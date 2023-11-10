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

    public virtual void Heal(float amount)
    {
        if (_currentHealth >= _maxHealth) return;
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
    }

    public bool CanHeal()
    {
        Debug.Log(_currentHealth);
        return _currentHealth < _maxHealth;
    }

    protected virtual void DieEffects()
    {
        CameraShake.Instance.Shake(2f);
    }
}
