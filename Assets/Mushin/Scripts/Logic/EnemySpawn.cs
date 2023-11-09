using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    [Tooltip("Tag usado en el Object Pooler.")]
    public string enemyTag;
    [Tooltip("Tiempo de la partida en el que empieza a spawnear.")]
    public float minuteToStartSpawning;
}