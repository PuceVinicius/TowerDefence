using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.FuncChannels;
using Boilerplate.Utilities;
using TowerDefence.EnemiesCommons;
using TowerDefence.TowersCommons;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.TowersManagers
{
    public class TowersPlacer : MonoBehaviour
    {
        #region Variables

        [Foldout("Data")]
        [SerializeField] private TowersPlacerData _data;

        [Foldout("References")]
        [SerializeField] private TowerPreview _preview;
        [SerializeField] private NavMeshSurface _surface;

        [Foldout("Callers")]
        [SerializeField] private BoolVoidFuncChannel _checkPathingAvailableFunc;
        [SerializeField] private CameraVoidFuncChannel _getCameraFunc;

        [Foldout("Broadcaster")]
        [SerializeField] private VoidEventChannel _setGameplayActionMapEvent;

        [Foldout("Listeners")]
        [SerializeField] private VoidEventChannel _inputStartPlaceTowerEvent;
        [SerializeField] private VoidEventChannel _inputEndPlaceTowerEvent;
        [SerializeField] private TowerTypeEventChannel _onTowerHUDClickEvent;

        [Foldout("Debug")]
        [SerializeField, ReadOnly] private NavMeshData _original;
        [SerializeField, ReadOnly] private NavMeshData _updated;
        [SerializeField, ReadOnly] private NavMeshHit _navMeshHit;
        [SerializeField, ReadOnly] private bool _isPathValid;
        [SerializeField, ReadOnly] private bool _isPreviewValid;
        [SerializeField, ReadOnly] private Camera _camera;
        [SerializeField, ReadOnly] private RaycastHit _raycastHit;
        [SerializeField, ReadOnly] private float _currentPathTimer;
        [SerializeField, ReadOnly] private bool _allowPreview = true;
        [SerializeField, ReadOnly] private TowerType _currentTowerType;

        private Coroutine _previewCoroutine;
        private Coroutine _checkBlockingCoroutine;

        #endregion

        #region Messages

        private void OnEnable()
        {
            EventUtils.AddEventListener(_inputStartPlaceTowerEvent, InputStartPlaceTower);
            EventUtils.AddEventListener(_inputEndPlaceTowerEvent, InputEndPlaceTower);
            EventUtils.AddEventListener(_onTowerHUDClickEvent, OnTowerHUDClick);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_inputStartPlaceTowerEvent, InputStartPlaceTower);
            EventUtils.RemoveEventListener(_inputEndPlaceTowerEvent, InputEndPlaceTower);
            EventUtils.RemoveEventListener(_onTowerHUDClickEvent, OnTowerHUDClick);
            StopAllCoroutines();
        }

        private void Start()
        {
            _surface.UpdateNavMesh();
            EventUtils.BroadcastEvent(_setGameplayActionMapEvent);
        }

        #endregion

        #region Methods

        private void OnTowerHUDClick(TowerType towerType)
        {
            if (towerType == _currentTowerType)
            {
                EndTowerPreview();
                return;
            }

            _currentTowerType = towerType;
            StartTowerPreview();
        }

        private void StartTowerPreview()
        {
            if (_previewCoroutine is { })
                StopCoroutine(_previewCoroutine);

            _previewCoroutine = StartCoroutine(TowerPreviewCoroutine());
        }

        private IEnumerator TowerPreviewCoroutine()
        {
            if (!_camera is { })
                _camera = FuncUtils.CallDelegate(_getCameraFunc);

            while (true)
            {
                yield return new WaitUntil(() => _allowPreview);

                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _raycastHit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Ground")))
                {
                    if (NavMesh.SamplePosition(_raycastHit.point, out _navMeshHit, _data.PreviewDistance, _data.SampleAreaMask))
                        ValidPreview(_raycastHit);
                    else
                        InvalidPreview(_raycastHit);
                }
                else
                    NoHitPreview();

                yield return null;
            }
        }

        private void ValidPreview(RaycastHit hit) => ShowPreview(hit, _data.ValidMaterial, true);

        private void InvalidPreview(RaycastHit hit) => ShowPreview(hit, _data.InvalidMaterial, false);

        private void ShowPreview(RaycastHit hit, Material material, bool value)
        {
            if (_preview.GameObject.activeInHierarchy is false)
                _preview.GameObject.SetActive(true);

            _isPreviewValid = value;
            _preview.Transform.position = hit.point;
            _preview.UpdateMaterial(material);
        }

        private void NoHitPreview()
        {
            if (_preview.GameObject.activeInHierarchy)
                _preview.GameObject.SetActive(false);
        }

        private void EndTowerPreview()
        {
            if (_previewCoroutine is { })
                StopCoroutine(_previewCoroutine);

            if (_preview.GameObject.activeInHierarchy)
                _preview.GameObject.SetActive(false);
            _isPreviewValid = false;
            _currentTowerType = TowerType.None;
        }

        private void InputStartPlaceTower()
        {
            if (_isPreviewValid is false)
                return;

            if (_checkBlockingCoroutine is { })
                StopCoroutine(_checkBlockingCoroutine);
            _checkBlockingCoroutine = StartCoroutine(CheckBlockingPathCoroutine(_preview.Transform.position));
        }

        private void InputEndPlaceTower()
        {
            if (_checkBlockingCoroutine is { })
                StopCoroutine(_checkBlockingCoroutine);
            _isPathValid = false;
            _allowPreview = true;
        }

        private IEnumerator CheckBlockingPathCoroutine(Vector3 obstacle)
        {
            _original = Instantiate(_surface.navMeshData);
            _surface.UpdateNavMesh(_surface.navMeshData);
            _currentPathTimer = Time.time;
            _allowPreview = false;

            yield return new WaitWhile(() => NavMesh.SamplePosition(obstacle, out _navMeshHit, _data.SampleDistance, _data.SampleAreaMask));

            _updated = Instantiate(_surface.navMeshData);
            _isPathValid = FuncUtils.CallDelegate(_checkPathingAvailableFunc);
            ApplyNavMeshSurface(_original);

            if (_isPathValid is false)
                yield break;

            yield return new WaitWhile(() => Time.time - _currentPathTimer < _data.DelayToPlace);

            Instantiate(GetCurrentTower(), _preview.Transform.position, Quaternion.identity);
            ApplyNavMeshSurface(_updated);
            _allowPreview = true;
        }

        private Tower GetCurrentTower()
        {
            switch (_currentTowerType)
            {
                case TowerType.Area:
                    return _data.AreaTower;
                case TowerType.Burst:
                    return _data.BurstTower;
                case TowerType.Fast:
                    return _data.FastTower;
            }
            return null;
        }

        private void ApplyNavMeshSurface(NavMeshData navMeshData)
        {
            _surface.RemoveData();
            _surface.navMeshData = navMeshData;
            _surface.AddData();
        }

        #endregion
    }
}