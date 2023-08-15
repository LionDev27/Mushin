using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAgent : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    [SerializeField] private List<EnemyState> _states;
    [SerializeField] private EnemyStats _stats;
    private EnemyDamageable _damageable;
    private EnemyState _currentState;
    private PlayerDamageable _playerDamageable;

    protected virtual void Awake()
    {
        if (_states.Count <= 0) return;
        foreach (var state in _states)
        {
            state.Setup(this);
        }
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        _damageable = GetComponent<EnemyDamageable>();
        target = FindObjectOfType<PlayerComponents>().transform;
    }

    protected virtual void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = _stats.speed / 3;
        
        _damageable.SetHealth(_stats.health);
        
        ChangeState(_states[0]);
    }

    protected virtual void Update()
    {
        if (_currentState)
            _currentState.Execute();
        if (_playerDamageable)
            _playerDamageable.TakeDamage(_stats.attackDamage);
    }

    protected virtual void ChangeState(EnemyState newState)
    {
        _currentState = newState;
        _currentState.OnStateEnter();
    }

    public void EnableNavigation(bool value)
    {
        navMeshAgent.velocity = value ? Vector3.one : Vector3.zero;
        navMeshAgent.isStopped = !value;
    }
    
    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.destination = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerDamageable damageable))
            _playerDamageable = damageable;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerDamageable = null;
    }
}
