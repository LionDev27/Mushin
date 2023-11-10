using System;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private PlayerDamageable _damageable;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Recollectable recollectable))
        {
            if (recollectable is HealRecollectable && !_damageable.CanHeal()) return;
            recollectable.SetTarget(transform.parent);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Recollectable recollectable))
        {
            if (recollectable is HealRecollectable && !_damageable.CanHeal() && recollectable.HasTarget)
                recollectable.RemoveTarget();
        }
    }
}
