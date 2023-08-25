using System;
using UnityEngine;

public class EnemyDamageable : Damageable
{
    private EnemyAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<EnemyAgent>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Taking damage");
    }

    protected override void Die()
    {
        _agent.EnableNavigation(false);
        Destroy(gameObject);
    }
}