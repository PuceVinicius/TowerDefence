using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using UnityEngine;

namespace TowerDefence.Units
{
    public abstract class Unit : MonoBehaviour
    {
        #region Variables

        [Header("The attribute that i'm using for foldout doesn't work with inheritance, dont have the time to fix now :(")]
        [Header("References")]
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;

        #endregion

        #region Properties

        public Transform Transform => _transform;
        public GameObject GameObject => _gameObject;

        #endregion
    }
}