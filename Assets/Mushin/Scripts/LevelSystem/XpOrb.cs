using System;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    [SerializeField] private int _xpToAdd;
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody2D _rb;
    private Transform target;
    private bool hasTarget;
    
    public static Action<int> OnXpOrbCollected;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDir = target.position - transform.position;
            targetDir.Normalize();
            _rb.velocity = targetDir * _moveSpeed * Time.fixedDeltaTime * 100;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        hasTarget = true;
    }
    
    public void Collect()
    {
        OnXpOrbCollected?.Invoke(_xpToAdd);
        ObjectPooler.Instance.ReturnToPool("xp", gameObject);
    }
}