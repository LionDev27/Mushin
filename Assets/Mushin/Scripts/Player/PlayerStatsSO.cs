using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Data/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public PlayerStats stats;
}

[System.Serializable]
public struct PlayerStats
{
    public int health;
    public int dashAmount;
    public float dashCooldown;
    public float moveSpeed;
    public float attackDamage;
    public int skillDamagePercentage;
    public int criticalChancePercentage;
    public int criticalMultiplierPercentage;
}