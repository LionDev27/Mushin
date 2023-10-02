using UnityEngine;

[System.Serializable]
public abstract class AttackBase : MonoBehaviour
{
    protected float _damage;
    protected float _criticalMultiplier;
    protected float _range;
    protected float _reach;

    public abstract void Attack(Vector2 dir, bool isCritical);

    public virtual void Setup(float damage, float criticalMultiplier, float range, float reach)
    {
        SetValue(ref _damage, damage);
        SetValue(ref _criticalMultiplier, criticalMultiplier);
        SetValue(ref _range, range);
        SetValue(ref _reach, reach);
    }

    private void SetValue(ref float oldValue, float newValue)
    {
        if (newValue != 0 && newValue != oldValue)
            oldValue = newValue;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Damageable damageable))
            Damage(damageable);
    }

    protected virtual void Damage(Damageable damageable)
    {
        damageable.TakeDamage(_damage);
    }
}