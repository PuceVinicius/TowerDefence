using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using TowerDefence.EnemiesCommons;
using TowerDefence.Units;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.EnemiesCommons
{
    public abstract class Enemy : Unit
    {
        #region Variables

        [Header("References")]
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Header("Datas")]
        [SerializeField] private EnemyData _data;

        [Header("Broadcasters")]
        [SerializeField] private EnemyEventChannel _onEnemyDisabledEvent;

        [Header("Debug")]
        [SerializeField, ReadOnly] private Vector3 _target;

        #endregion

        #region Properties

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Vector3 Target => _target;

        #endregion

        protected virtual void OnDisable()
        {
            EventUtils.BroadcastEvent(_onEnemyDisabledEvent, this);
        }

        public void Init(Vector3 target)
        {
            Initialization(target);
        }

        protected virtual void Initialization(Vector3 target)
        {
            InitNavMesh(target);
        }

        private void InitNavMesh(Vector3 target)
        {
            _navMeshAgent.speed = _data.Speed;
            _navMeshAgent.acceleration = _data.Acceleration;
            _navMeshAgent.angularSpeed = _data.TurningSpeed;
            _navMeshAgent.obstacleAvoidanceType = _data.ObstacleAvoidanceType;
            _navMeshAgent.autoRepath = _data.AutoRepath;

            _navMeshAgent.SetDestination(target);
            _target = target;
        }
    }
}