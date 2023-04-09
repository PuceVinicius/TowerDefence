using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.EnemiesManagers
{
    public class EnemiesDestination : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Unit"))
                return;

            other.gameObject.SetActive(false);
        }
    }
}