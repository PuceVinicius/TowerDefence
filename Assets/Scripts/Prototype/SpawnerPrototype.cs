using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Profiling;

public class SpawnerPrototype : MonoBehaviour
{
    public bool _spawn = true;
    public float _spawnDelay = 0.01f;
    public int _maxUnits = 10000;
    [Space]
    public NavMeshSurface _surface;
    public NavMeshData _beforeCheck;
    public NavMeshData _afterCheck;
    public UnitPrototype _unitPrototype;
    public NavMeshObstacle _obstacle;
    [Space]
    public int cast;
    public float maxDist;
    public int path;
    [Space]
    public Transform _target;
    public Transform _parent;
    public Transform _spawnPoint;
    public bool _finished;
    public bool blocks;

    private HashSet<UnitPrototype> _activeUnits = new HashSet<UnitPrototype>();

    private float _currentDelay;
    private NavMeshPath _path;
    private NavMeshData _dataCopy;
    private int counter;

    private void Awake()
    {
        _path = new NavMeshPath();
        _surface.UpdateNavMesh();
    }

    private void Update()
    {
        if (_spawn is false)
            return;

        if (_activeUnits.Count > _maxUnits)
            return;

        _currentDelay += Time.deltaTime;

        if (_currentDelay < _spawnDelay)
            return;

        _currentDelay -= _spawnDelay;

        var unit = Instantiate(_unitPrototype, _spawnPoint.position, Quaternion.identity, _parent);
        _activeUnits.Add(unit);
        unit.name = $"Unit ({counter++})";
        unit.SetTarget(_target);
        unit.SetSpawner(this);
    }

    [ContextMenu("Test Carving")]
    public void TestCarving()
    {
        StartCoroutine(CarveCoroutine());
    }

    private IEnumerator CarveCoroutine()
    {
        // _surface.navMeshData = _data;
        // _surface.AddData();
        // while (true)
        // {
        //     yield return null;
        // }
        yield break;
    }

    public bool IsBlockingPath(NavMeshObstacle navMeshObstacle)
    {
        // navMeshObstacle.carving = true;
        // navMeshObstacle.carveOnlyStationary = false;
        //
        // _surface.UpdateNavMesh();
        //
        // foreach (var unit in _activeUnits)
        // {
        //     if (unit._agent.isOnNavMesh is false)
        //         continue;
        //
        //     if (unit._agent.CalculatePath(unit._target, _path) && _path.status is NavMeshPathStatus.PathComplete)
        //         continue;
        //
        //     navMeshObstacle.carving = false;
        //     return true;
        // }

        // navMeshObstacle.carving = false;

        StartCoroutine(CheckBlockingPathCoroutine(navMeshObstacle));

        return false;
    }

    private IEnumerator CheckBlockingPathCoroutine(NavMeshObstacle navMeshObstacle)
    {
        _finished = false;

        navMeshObstacle.carving = true;
        navMeshObstacle.carveOnlyStationary = false;

        _surface.UpdateNavMesh(_surface.navMeshData);

        var source = navMeshObstacle.transform.position + new Vector3(0, 5, 0);
        var target = navMeshObstacle.transform.position + new Vector3(0, -5, 0);

        // Debug.Log("Started" + " " + Time.time);

        Profiler.BeginSample("Before Copy");

        _beforeCheck = Instantiate(_surface.navMeshData);

        Profiler.EndSample();

        NavMeshHit hit;

        while (NavMesh.SamplePosition(navMeshObstacle.transform.position, out hit, maxDist, -1))
        {
            NavMesh.SamplePosition(navMeshObstacle.transform.position, out hit, maxDist, -1);

            // if (hit.hit is false || hit.mask == 1 << NavMesh.GetAreaFromName("Not Walkable"))
            //     break;

            yield return null;
        }

        // yield return new WaitUntil(() => NavMesh.SamplePosition(navMeshObstacle.transform.position, out hit, maxDist, 1 << NavMesh.GetAreaFromName("Jump")));

        Profiler.BeginSample("After Copy");

        _afterCheck = Instantiate(_surface.navMeshData);

        Profiler.EndSample();

        blocks = false;
        foreach (var unit in _activeUnits)
        {
            if (unit._agent.isOnNavMesh is false)
                continue;

            if (NavMesh.CalculatePath(unit._agent.transform.position, unit._target, 1 << NavMesh.GetAreaFromName("Walkable"), _path) && _path.status is NavMeshPathStatus.PathComplete)
                continue;

            blocks = true;
            break;
        }

        Debug.Log("It blocks the path? " + blocks + " " /*+ Time.time*/);

        _finished = true;
        navMeshObstacle.carving = false;

        _surface.RemoveData();
        _surface.navMeshData = _beforeCheck;
        _surface.AddData();
    }

    public void UnitDeath(UnitPrototype unit)
    {
        if (_activeUnits.Contains(unit))
            _activeUnits.Remove(unit);
    }
}