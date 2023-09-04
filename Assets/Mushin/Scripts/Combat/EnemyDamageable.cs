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
        SpawnController.Instance.enemiesKilled++;
        var xpAmount = _agent.stats.xpAmount;
        switch (xpAmount)
        {
            case 1:
                ObjectPooler.Instance.SpawnFromPool(_agent.stats.xpOrbTag, transform.position);
                break;
            case > 1:
            {
                for (int i = 0; i < xpAmount; i++)
                {
                    Vector2 currentPos = transform.position;
                    Vector2 pos = new Vector2(
                        Random.Range(currentPos.x - 0.25f, currentPos.x + 0.25f),
                        Random.Range(currentPos.y - 0.25f, currentPos.y + 0.25f)
                    );
                    ObjectPooler.Instance.SpawnFromPool(_agent.stats.xpOrbTag, pos);
                }
                break;
            }
        }
        Destroy(gameObject);
    }
}