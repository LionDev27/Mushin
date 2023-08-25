using UnityEngine;

[System.Serializable]
public abstract class EnemyState : MonoBehaviour
{
    public abstract void Setup(EnemyAgent agent);
    public abstract void OnStateEnter();
    public abstract void Execute();
}
