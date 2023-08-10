using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    protected abstract void Die();
}
