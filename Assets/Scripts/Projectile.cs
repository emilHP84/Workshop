using System;
using System.Collections;
using System.Collections.Generic;
using Scenes.Jordan.Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private int _distance;
    private bool _isPlayer;

    private Rigidbody _rigidbody;

    private Vector3 _startPos;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Log(_speed);
        

        _startPos = transform.position;
    }

    private void Update()
    {
        _rigidbody.velocity = transform.right * _speed;
        CheckBulletOutOfRange();
    }

    private void CheckBulletOutOfRange()
    {
        if(Vector3.Distance(_startPos, transform.position) >= _distance) Destroy(gameObject);
    }

    public void Initialize(float speed, int distance, bool isPlayer)
    {
        _speed = speed;
        _distance = distance;
        _isPlayer = isPlayer;
    }

    private void OnCollisionEnter(Collision other)
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
