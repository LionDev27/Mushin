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
}
