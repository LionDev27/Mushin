using System;
using System.Collections.Generic;
using Mushin.Scripts.Events;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAgent : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    public List<EnemyState> states;
    
    public EnemyStats stats;
    private EnemyDamageable _damageable;
    private EnemyState _currentState;
    private PlayerDamageable _playerDamageable;
    private TargetDirFlipper _spriteFlipper;
    private float _attackCooldown = 0.5f;
    private float _timer;

    protected virtual void Awake()
    {
        if (states.Count <= 0) return;
        foreach (var state in states)
        {
            state.Setup(this);
        }
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        _damageable = GetComponent<EnemyDamageable>();
        target = FindObjectOfType<PlayerComponents>().transform;
        _spriteFlipper = GetComponentInChildren<TargetDirFlipper>();
    }

    private void OnEnable()
    {
        _damageable.SetHealth(stats.health);
        ChangeState(states[0]);
    }
    
    protected virtual void Start()
    {
        _spriteFlipper.target = target;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = stats.speed / 3;
    }

    protected virtual void Update()
    {
        if (_currentState)
            _currentState.Execute();
        if (_timer > 0)
            _timer -= Time.deltaTime;
        FlipSprite();
    }

    public virtual void ChangeState(EnemyState newState)
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

    public float TargetDistance()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    public Vector2 TargetDir()
    {
        Vector2 dir = target.position - transform.position;
        dir.Normalize();
        return dir;
    }

    private void FlipSprite()
    {
        if (_spriteFlipper.CanFlip())
            _spriteFlipper.Flip();
    }

    private void ResetAttackCooldown()
    {
        _timer = _attackCooldown;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Attack(col);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Attack(col);
    }

    private void Attack(Collider2D col)
    {
        if (_timer <= 0 && col.TryGetComponent(out PlayerDamageable damageable) && col is BoxCollider2D)
        {
            damageable.TakeDamage(stats.attackDamage);
            ResetAttackCooldown();
        }
    }
}
