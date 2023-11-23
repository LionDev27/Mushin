using System;
using UnityEngine;

public class Recollectable : MonoBehaviour, IPoolable
{
    public bool HasTarget => _hasTarget;
    
    [SerializeField] protected float _moveSpeed = 7f;

    private Rigidbody2D _rb;
    private Transform _target;
    private bool _hasTarget;
    private string _poolTag;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_hasTarget) return;
        _timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!_hasTarget) return;
        Vector2 targetDir = _target.position - transform.position;
        targetDir.Normalize();
        _rb.velocity = targetDir * _moveSpeed * Time.fixedDeltaTime * 100 * _timer;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _timer = 1f;
        _hasTarget = true;
    }

    public void RemoveTarget()
    {
        if (!_hasTarget) return;
        _timer = 0f;
        _hasTarget = false;
        _target = null;
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    public virtual void Collect()
    {
        RemoveTarget();
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }
}