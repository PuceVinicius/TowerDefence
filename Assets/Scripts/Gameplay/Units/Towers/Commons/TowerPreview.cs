using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using TowerDefence.TowersCommons;
using UnityEngine;

namespace TowerDefence.TowersCommons
{
    public class TowerPreview : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Transform _Transform;
        [SerializeField] private MeshRenderer _meshRenderer;

        #endregion

        #region Properties

        public GameObject GameObject => _gameObject;
        public Transform Transform => _Transform;

        #endregion

        #region Methods

        public void UpdateMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        #endregion
    }
}