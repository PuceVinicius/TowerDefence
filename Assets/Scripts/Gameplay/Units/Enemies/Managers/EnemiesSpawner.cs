using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.FuncChannels;
using Boilerplate.Utilities;
using TowerDefence.EnemiesCommons;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace TowerDefence.EnemiesManagers
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private Transform _enemiesTarget;

        [Foldout("Datas")]
        [SerializeField] private EnemySpawnerData _data;

        [Foldout("Delegates")]
        [SerializeField] private BoolVoidFuncChannel _isPathingAvailableFunc;

        [Foldout("Listeners")]
        [SerializeField] private EnemyEventChannel _onEnemyDisabledEvent;

        [Foldout("Debug")]
        [SerializeField, ReadOnly] private HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();
        [SerializeField, ReadOnly] private float _currentDelay;

        private void OnEnable()
        {
            GlobalUpdate.UpdateEvent += OnGlobalUpdate;
            EventUtils.AddEventListener(_onEnemyDisabledEvent, OnEnemyDisabled);
            FuncUtils.SetDelegate(_isPathingAvailableFunc, CheckPathingAvailable);
        }

        private void OnDisable()
        {
            GlobalUpdate.UpdateEvent -= OnGlobalUpdate;
            EventUtils.RemoveEventListener(_onEnemyDisabledEvent, OnEnemyDisabled);
            FuncUtils.ResetDelegate(_isPathingAvailableFunc);
        }

        private void OnGlobalUpdate()
        {
            UpdateSpawn();
        }

        private void UpdateSpawn()
        {
            if (_data.Disable)
                return;

            _currentDelay += Time.deltaTime;

            if (_currentDelay < _data.SpawnDelay)
                return;

            _currentDelay -= _data.SpawnDelay;

            var index = UnityEngine.Random.Range(0, _data.EnemiesToSpawn.Count);
            var newEnemy = Instantiate(_data.EnemiesToSpawn[index], _enemiesParent);
            _activeEnemies.Add(newEnemy);
            newEnemy.Init(_enemiesTarget.position);
        }

        private bool CheckPathingAvailable()
        {
            var path = new NavMeshPath();

            foreach (var enemy in _activeEnemies)
            {
                if (enemy.NavMeshAgent.isOnNavMesh is false)
                    continue;

                if (NavMesh.CalculatePath(enemy.NavMeshAgent.transform.position, enemy.Target, 1 << NavMesh.GetAreaFromName("Walkable"), path)
                    && path.status is NavMeshPathStatus.PathComplete)
                    continue;

                return false;
            }

            return true;
        }

        private void OnEnemyDisabled(Enemy enemy)
        {
            if (_activeEnemies.Contains(enemy))
                _activeEnemies.Remove(enemy);
        }
    }
}