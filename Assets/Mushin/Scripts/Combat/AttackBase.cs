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

    public abstract void Attack(bool isCritical);

    public virtual void Setup(PlayerStats stats, int pierce)
    {
        SetValue(ref _damage, stats.attackDamage);
        SetValue(ref _criticalMultiplier, stats.criticalDamageMultiplier);
        SetValue(ref _range, stats.attackRange);
        SetValue(ref _reach, stats.attackReach);
        SetValue(ref _attackCooldown, stats.AttackCooldown());
        if (pierce != 0)
            _pierce += pierce;
    }

    public virtual void UpdateDir(Vector2 dir){}

    private void SetValue(ref float oldValue, float newValue)
    {
        if (newValue != 0 && newValue != oldValue)
            oldValue = newValue;
    }
    
    protected virtual void Damage(Damageable damageable, bool critical)
    {
        damageable.TakeDamage(critical ? _damage * _criticalMultiplier : _damage);
    }
}