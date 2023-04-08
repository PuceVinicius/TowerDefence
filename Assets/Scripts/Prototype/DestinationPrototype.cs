using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPrototype : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Unit"))
            return;

        other.gameObject.SetActive(false);
    }
}