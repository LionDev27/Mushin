using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    protected float _maxHealth;
    protected float _currentHealth;

    public void SetHealth(float amount)
    {
        _maxHealth = amount;
        _currentHealth = _maxHealth;
    }
    
    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        DieEffects();
    }

    protected virtual void DieEffects()
    {
        CameraShake.Instance.Shake(5f);
    }
}
