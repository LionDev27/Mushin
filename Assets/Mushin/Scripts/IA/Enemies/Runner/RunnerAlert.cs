using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAlert : EnemyState
{
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
