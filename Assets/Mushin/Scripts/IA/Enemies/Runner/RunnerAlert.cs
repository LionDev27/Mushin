public class RunnerAlert : EnemyState
{
    private EnemyAgent _agent;

    public override void Setup(EnemyAgent agent)
    {
        _agent = agent;
    }

    public override void OnStateEnter()
    {
        _agent.SetDestination(_agent.target.position);
    }

    public override void Execute()
    {
        if (_agent.navMeshAgent.destination != _agent.target.position)
            _agent.navMeshAgent.SetDestination(_agent.target.position);
    }
}
