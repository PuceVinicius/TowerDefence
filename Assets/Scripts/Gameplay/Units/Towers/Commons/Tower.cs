using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.Utilities;
using TowerDefence.TowersCommons;
using TowerDefence.Units;
using UnityEngine;

namespace TowerDefence.TowersCommons
{
    public abstract class Tower : Unit
    {
        #region Variables

        [Header("Datas")]
        [SerializeField] private TowerData _data;

        [Header("Debug")]
        [SerializeField] private float _currentCooldown;
        [SerializeField] private RaycastHit[] _raycastHits = new RaycastHit[20];
        private Coroutine _damageCoroutine;

        #endregion

        #region Messages

        private void Start()
        {
            // I'm commenting this because its WIP and I don't have enough time to finish right now
            // _damageCoroutine = StartCoroutine(DamageCoroutine());
        }

        #endregion

        #region Methods

        private IEnumerator DamageCoroutine()
        {
            while (true)
            {
                _currentCooldown = 0;
                while (_currentCooldown < _data.Cooldown)
                {
                    _currentCooldown += Time.deltaTime;
                    yield return null;
                }

                _currentCooldown -= _data.Cooldown;

                DamageTargets(GetTargets());
            }
        }

        private List<Unit> GetTargets()
        {
            switch (_data.TargetType)
            {
                case TargetType.Closer:
                    return GetClosestTarget();
                case TargetType.Area:
                    return GetAreaTargets();
                case TargetType.Tougher:
                    return GetToughestTarget();
            }
            return null;
        }

        private List<Unit> GetClosestTarget()
        {
            var collisions = Physics.SphereCastNonAlloc(
                Transform.position, _data.Range,
                Vector3.down, _raycastHits, _data.Range,
                ~(1 << LayerMask.NameToLayer("Unit")), QueryTriggerInteraction.Collide
            );

            if (collisions <= 0)
                return null;

            float closestSqrDistance = float.PositiveInfinity;
            Transform closestUnit = _raycastHits[0].transform;

            for (int i = 0; i < collisions; i++)
            {
                var dist = Vector3.SqrMagnitude(_raycastHits[i].transform.position - Transform.position);
                if (dist >= closestSqrDistance)
                    continue;

                closestSqrDistance = dist;
                closestUnit = _raycastHits[i].transform;
            }

            return new List<Unit> { closestUnit.gameObject.GetComponent<Unit>() };
        }

        private List<Unit> GetAreaTargets()
        {

            return null;
        }

        private List<Unit> GetToughestTarget()
        {

            return null;
        }

        private void DamageTargets(List<Unit> targets)
        {
            if (targets is { } is false)
                return;
        }

        #endregion
    }
}