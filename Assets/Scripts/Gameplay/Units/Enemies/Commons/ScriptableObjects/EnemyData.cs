using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.Units;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.EnemiesCommons
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = EnemiesConsts.ENEMIES_ASSET_MENU_NAME_PREFIX + "EnemyData")]
    public class EnemyData : UnitData
    {
        #region Variables

        [Foldout("NavMesh")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _turningSpeed = 120f;
        [Space]
        [SerializeField] private ObstacleAvoidanceType _obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        [SerializeField] private bool _autoRepath = false;

        #endregion

        #region Properties

        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public float TurningSpeed => _turningSpeed;
        public ObstacleAvoidanceType ObstacleAvoidanceType => _obstacleAvoidanceType;
        public bool AutoRepath => _autoRepath;

        #endregion
    }
}