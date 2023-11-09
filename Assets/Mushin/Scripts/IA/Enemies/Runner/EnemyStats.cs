using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Data/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int health;
    public float speed;
    public float attackDamage;
    public string xpOrbTag;
    public int xpAmount;
    [Range(0, 100)]
    public int dropHealthProbability;
}
