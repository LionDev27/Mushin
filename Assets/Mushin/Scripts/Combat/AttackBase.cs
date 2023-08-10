using System;
using UnityEngine;

[System.Serializable]
public abstract class AttackBase : MonoBehaviour
{
    [HideInInspector]
    public PlayerInputs _playerInput;
    [HideInInspector]
    public float _currentDamage;

    public abstract void Attack();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(_currentDamage);
        }
    }
}