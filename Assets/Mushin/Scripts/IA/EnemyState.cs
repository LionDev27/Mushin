using System;
using UnityEngine;

[System.Serializable]
public abstract class EnemyState : MonoBehaviour
{
    protected EnemyAgent _agent;

    private void Start()
    {
        OnStateEnter();
    }

    public virtual void Setup(EnemyAgent agent)
    {
        _agent = agent;
    }
    public abstract void OnStateEnter();
    public abstract void Execute();
}
