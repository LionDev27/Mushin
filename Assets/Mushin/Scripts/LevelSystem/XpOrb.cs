using System;
using UnityEngine;

public class XpOrb : MonoBehaviour, IPoolable
{
    [SerializeField] private int _xpToAdd;
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody2D _rb;
    private Transform _target;
    private bool _hasTarget;
    private string _poolTag;
    
    public static Action<int> OnXpOrbCollected;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_hasTarget)
        {
            Vector2 targetDir = _target.position - transform.position;
            targetDir.Normalize();
            _rb.velocity = targetDir * _moveSpeed * Time.fixedDeltaTime * 100;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _hasTarget = true;
    }
    
    public void Collect()
    {
        OnXpOrbCollected?.Invoke(_xpToAdd);
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }
}