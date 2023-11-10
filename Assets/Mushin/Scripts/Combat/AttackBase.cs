using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AttackBase : MonoBehaviour
{
    protected float _damage;
    protected float _criticalMultiplier;
    protected float _range;
    protected float _reach;
    protected int _pierce;
    protected float _attackCooldown;
    private int _colliders;
    private bool _canDamage = true;

    public abstract void Attack(Vector2 dir, bool isCritical);

    public virtual void Setup(PlayerStats stats, int pierce)
    {
        SetValue(ref _damage, stats.attackDamage);
        SetValue(ref _criticalMultiplier, stats.criticalDamageMultiplier);
        SetValue(ref _range, stats.attackRange);
        SetValue(ref _reach, stats.attackReach);
        SetValue(ref _attackCooldown, stats.AttackCooldown());
        if (pierce != 0)
        {
            _pierce += pierce;
            Debug.Log($"Penetration: {_pierce}");
        }
    }

    private void SetValue(ref float oldValue, float newValue)
    {
        if (newValue != 0 && newValue != oldValue)
            oldValue = newValue;
    }
    
    protected virtual void Damage(Damageable damageable)
    {
        damageable.TakeDamage(_damage);
    }

    private void EnableDamage()
    {
        _canDamage = true;
        _colliders = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Damageable damageable))
        {
            if (!_canDamage) return;
            _colliders++;
            if (_colliders >= _pierce)
            {
                _canDamage = false;
                Invoke(nameof(EnableDamage), _attackCooldown);
            }
            Debug.Log($"Adding. Colliders: {_colliders}");
            Damage(damageable);
        }
    }
}