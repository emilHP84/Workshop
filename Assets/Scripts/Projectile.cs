using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _distance;
    private bool _isPlayer;

    private Rigidbody _rigidbody;

    private Vector3 _startPos;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _startPos = transform.position;
    }

    private void Update()
    {
        CheckBulletOutOfRange();
    }

    private void CheckBulletOutOfRange()
    {
        if(Vector3.Distance(_startPos, transform.position) >= _distance) Destroy(gameObject);
    }

    public void Initialize(float speed, int distance, bool isPlayer)
    {
        _distance = distance;
        _isPlayer = isPlayer;
        
        _rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (_isPlayer)
        {
            case true when other.gameObject.CompareTag("Enemy"):
            case false when other.gameObject.CompareTag("Player"):
                other.gameObject.GetComponent<Entity>().TakeDamage(1);
                break;
        }
        
        Destroy(gameObject);
    }
}
