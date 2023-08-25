using UnityEngine;

public class RangedAttack : EnemyState
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _cooldown;
    
    private EnemyRangedAgent _agent;
    private float timer;
    
    public override void Setup(EnemyAgent agent)
    {
        _agent = agent as EnemyRangedAgent;
    }

    public override void OnStateEnter()
    {
        _agent.EnableNavigation(false);
        Attack();
    }

    public override void Execute()
    {
        if (TimerEnded())
        {
            if (_agent.TargetDistance() > _agent.distanceToAttackPlayer)
                _agent.ChangeState(_agent.states[0]);
            else
                Attack();
        }
        else
            timer -= Time.deltaTime;
    }

    private bool TimerEnded()
    {
        return timer <= 0f;
    }
    
    private void Attack()
    {
        timer = _cooldown;
        GameObject tempBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        tempBullet.GetComponent<BasicBullet>().SetBullet(
            _agent.TargetDir(), _bulletSpeed,
            "Player", _agent.stats.attackDamage);
    }
}