using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using UnityEngine;

namespace TowerDefence.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _gameObject;
    }
}