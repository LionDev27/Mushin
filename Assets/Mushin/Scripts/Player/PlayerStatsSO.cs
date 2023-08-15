using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Data/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public PlayerStats stats;
}

[System.Serializable]
public struct PlayerStats
{
    public int health;
    [Tooltip("Tiempo en segundos que el jugador es invencible a ataques. Se activa después de ser atacado.")]
    public float invulnerabilitySeconds;
    public int dashAmount;
    public float dashCooldown;
    public float dashForce;
    public float moveSpeed;
    public float attackDamage;
    [Tooltip("Tiempo en segundos que tarda entre ataque y ataque")]
    public float attackSpeed;
    public GameObject attackPrefab;
    public float skillDamage;
    public float skillCooldown;
    [Tooltip("Multiplicador de daño que se aplica al daño base.")]
    public float criticalDamageMultiplier;
    [Range(0, 100)]
    public int criticalChancePercentage;
    public float collectionRange;
    [Tooltip("Multiplicador que se aplica a la cantidad de experiencia que da un orbe.")]
    public float xpModifier;
}