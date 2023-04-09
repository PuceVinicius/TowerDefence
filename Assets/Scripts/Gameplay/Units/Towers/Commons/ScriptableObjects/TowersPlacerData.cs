using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.TowersCommons;
using TowerDefence.Units;
using UnityEngine;

namespace TowerDefence.TowersCommons
{
    [CreateAssetMenu(fileName = "New TowersPlacerData", menuName = TowersConsts.TOWERS_ASSET_MENU_NAME_PREFIX + "TowersPlacerData")]
    public class TowersPlacerData : ScriptableObject
    {
        #region Variables

        [Foldout("Setup")]
        [SerializeField] private Tower _fastTower;
        [SerializeField] private Tower _areaTower;
        [SerializeField] private Tower _burstTower;
        [Space]
        [SerializeField] private float _delayToPlace = 0.2f;
        [SerializeField] private float _sampleDistance = 1f;
        [SerializeField] private int _sampleAreaMask = -1;
        [Space]
        [SerializeField] private float _previewDistance = .05f;
        [Space]
        [SerializeField] private Material _validMaterial;
        [SerializeField] private Material _invalidMaterial;

        #endregion

        #region Properties

        public Tower FastTower => _fastTower;
        public Tower AreaTower => _areaTower;
        public Tower BurstTower => _burstTower;

        public float DelayToPlace => _delayToPlace;
        public float SampleDistance => _sampleDistance;
        public int SampleAreaMask => _sampleAreaMask;

        public float PreviewDistance => _previewDistance;

        public Material ValidMaterial => _validMaterial;
        public Material InvalidMaterial => _invalidMaterial;

        #endregion
    }
}