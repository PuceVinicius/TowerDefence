using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitPrototype : MonoBehaviour
{
    public NavMeshAgent _agent;
    public SpawnerPrototype _spawner;
    public Vector3 _target;

    public void SetTarget(Transform target)
    {
        _target = target.position;
        _agent.SetDestination(_target);
    }

    public void SetSpawner(SpawnerPrototype spawner)
    {
        _spawner = spawner;
    }

    private void OnDisable()
    {
        if (_spawner)
            _spawner.UnitDeath(this);
    }
}