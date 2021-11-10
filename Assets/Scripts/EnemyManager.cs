using System;
using Scenes.Jordan.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Entity
{
    [SerializeField] protected float distanceDetection = 10f;
    [SerializeField] protected float distanceToShoot = 6f;
    
    private State _currentState = State.ChaseEgg;

    private PlayerManager _player;

    [SerializeField] private Transform egg;

    private NavMeshAgent _enemy;

    private float _eggDistance;
    private float _playerDistance;
    
    private enum State
    {
        ChaseEgg,
        ChasePlayer,
        Combat,
        Dead
    }

    private void Awake()
    {
        _player = FindObjectOfType<PlayerManager>();
        _enemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckDistances();
        
        switch (_currentState)
        {
            case (State.ChaseEgg):
                ChaseEgg();
                break;
            case (State.ChasePlayer):
                ChasePlayer();
                break;
            case (State.Combat):
                Combat();
                break;
            case (State.Dead):
                Dead();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChaseEgg()
    {
        _enemy.destination = egg.transform.position;

        _currentState = IsPlayerDetected(_playerDistance, _eggDistance);

        if (IsDead) _currentState = State.Dead;
    }
    
    private void ChasePlayer()
    {
        _enemy.destination = _player.transform.position;
        
        PlayerStatus();
        
        if (IsDead) _currentState = State.Dead;
    }
    
    private void Combat()
    {
        _enemy.destination = transform.position;
        
        _currentState = IsPlayerInRange(_playerDistance);
        
        if (IsDead) _currentState = State.Dead;
    }
    
    private void Dead()
    {
        Destroy(gameObject);
    }

    private void CheckDistances()
    {
        var playerPosition = _player.transform.position;
        var enemyPosition = transform.position;
        var eggPosition = egg.position;

        _eggDistance = Vector3.Distance(enemyPosition, eggPosition);
        _playerDistance = Vector3.Distance(enemyPosition, playerPosition);
    }
    
    private void PlayerStatus()
    {
        _currentState = IsPlayerInRange(_playerDistance);
        _currentState = IsPlayerDetected(_playerDistance, _eggDistance);
    }
    
    private State IsPlayerDetected(float playerDistance, float eggDistance) => 
        playerDistance < distanceDetection && playerDistance < eggDistance ? State.ChasePlayer : State.ChaseEgg;

    private State IsPlayerInRange(float playerDistance) => 
        playerDistance < distanceToShoot ? State.Combat : State.ChasePlayer;
}
