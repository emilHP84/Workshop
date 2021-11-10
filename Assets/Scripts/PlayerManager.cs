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

        if (!Physics.Raycast(ray, out var hit)) return;
        
        _player.transform.LookAt(hit.point + new Vector3(0, transform.position.y, 0));
        
        if (Input.GetMouseButtonDown(0))
        {
            _player.destination = hit.point;
        }

    }
}
