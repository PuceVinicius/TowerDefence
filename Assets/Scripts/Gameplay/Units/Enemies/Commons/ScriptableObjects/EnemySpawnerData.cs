using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.Units;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.EnemiesCommons
{
    [CreateAssetMenu(fileName = "New EnemySpawnerData", menuName = EnemiesConsts.ENEMIES_ASSET_MENU_NAME_PREFIX + "EnemySpawnerData")]
    public class EnemySpawnerData : ScriptableObject
    {
        #region Variables

        [Foldout("Setup")]
        [SerializeField] private bool _disable = false;
        [SerializeField] private float _spawnDelay = 0.5f;
        [SerializeField] private int _maxSpawned = 1000;
        [SerializeField] private List<Enemy> _enemiesToSpawn;

        #endregion

        #region Properties

        public bool Disable => _disable;
        public float SpawnDelay => _spawnDelay;
        public int MaxSpawned => _maxSpawned;
        public List<Enemy> EnemiesToSpawn => _enemiesToSpawn;

        #endregion
    }
}