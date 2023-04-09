using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.TowersCommons;
using TowerDefence.Units;
using UnityEngine;

namespace TowerDefence.TowersCommons
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = TowersConsts.TOWERS_ASSET_MENU_NAME_PREFIX + "TowerData")]
    public class TowerData : UnitData
    {
        #region Variables

        [Foldout("Setup")]
        [SerializeField] private float _range = 5f;
        [SerializeField] private float _cooldown = .5f;
        [SerializeField] private float _damage = 20f;
        [SerializeField] private TargetType _targetType;

        #endregion

        #region Properties

        public float Range => _range;
        public float Cooldown => _cooldown;
        public float Damage => _damage;
        public TargetType TargetType => _targetType;

        #endregion
    }
}