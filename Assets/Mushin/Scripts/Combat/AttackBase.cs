using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AttackBase : MonoBehaviour
{
    protected float _damage;
    protected float _criticalMultiplier;
    protected float _range;
    protected float _reach;
    protected int _penetration;
    private List<Collider2D> _colliders = new();

    public abstract void Attack(Vector2 dir, bool isCritical);

    public virtual void Setup(PlayerStats stats, int penetration)
    {
        SetValue(ref _damage, stats.attackDamage);
        SetValue(ref _criticalMultiplier, stats.criticalDamageMultiplier);
        SetValue(ref _range, stats.attackRange);
        SetValue(ref _reach, stats.attackReach);
        if (penetration != 0)
            _penetration += penetration;
        Debug.Log(_penetration);
    }

    private void SetValue(ref float oldValue, float newValue)
    {
        if (newValue != 0 && newValue != oldValue)
            oldValue = newValue;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Damageable damageable))
        {
            if (_colliders.Count >= _penetration) return;
            _colliders.Add(col);
            Damage(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.TryGetComponent(out EnemyDamageable damageable)) return;
        if (_colliders.Contains(col))
            _colliders.Remove(col);
    }

    protected virtual void Damage(Damageable damageable)
    {
        damageable.TakeDamage(_damage);
    }
}