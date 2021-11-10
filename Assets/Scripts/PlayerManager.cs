using Scenes.Jordan.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : Entity
{
    private NavMeshAgent _player;
    private Camera _camera;

    private void Awake()
    {
        _player = GetComponent<NavMeshAgent>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
        {
            _player.destination = hit.point;
        }

    }
}
