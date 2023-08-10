using UnityEngine;

public class EnemyDamageable : Damageable
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Taking damage");
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}