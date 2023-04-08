using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacerPrototype : MonoBehaviour
{
    [SerializeField] private TowerPrototype _tower;
    [SerializeField] private Camera _camera;
    [Space]
    public SpawnerPrototype _spawner;
    [Space]
    public Material _badMaterial;
    public Material _goodMaterial;

    public bool _placing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) is false)
            return;

        if (_placing is false)
            return;

        var mouse = Input.mousePosition;

        if (!_camera)
            return;

        var ray = _camera.ScreenPointToRay(mouse);

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Ground")) is false)
            return;

        _tower._transform.position = hit.point;

        if (_spawner.IsBlockingPath(_tower._obstacle))
        {
            // Debug.Log("BLOCKING");
            _tower._meshRenderer.material = _badMaterial;
        }
        else
        {
            // Debug.Log("SAFE");
            _tower._meshRenderer.material = _goodMaterial;
        }
    }
}