using UnityEngine;

public class RangedAlert : EnemyState
{
    private EnemyRangedAgent _agent;
    
    public override void Setup(EnemyAgent agent)
    {
        _agent = agent as EnemyRangedAgent;
    }

    public override void OnStateEnter()
    {
        _agent.EnableNavigation(true);
        _agent.SetDestination(_agent.target.position);
    }

    public override void Execute()
    {
        if (_agent.TargetDistance() <= _agent.distanceToAttackPlayer)
        {
            Debug.Log("A distancia de ataque: " + _agent.TargetDistance());
            _agent.ChangeState(_agent.states[1]);
        }
        else if (_agent.navMeshAgent.destination != _agent.target.position)
            _agent.navMeshAgent.SetDestination(_agent.target.position);
    }
}