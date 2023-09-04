using System.Collections.Generic;
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
    }

    protected virtual void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = stats.speed / 3;
        
        _damageable.SetHealth(stats.health);
        
        ChangeState(states[0]);
    }

    protected virtual void Update()
    {
        if (_currentState)
            _currentState.Execute();
        if (_playerDamageable)
            _playerDamageable.TakeDamage(stats.attackDamage);
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

    private void OnDisable()
    {
        if (SpawnController.Instance != null)
            SpawnController.Instance.RemoveEnemy();
    }

    private void OnEnable()
    {
        if (SpawnController.Instance != null)
            SpawnController.Instance.AddEnemy();
    }
}
