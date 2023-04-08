using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.EnemiesCommons;
using TowerDefence.Units;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public abstract class Enemy : Unit
    {
        [Foldout("Datas")]
        [SerializeField] private EnemyData _enemyData;
    }
}