using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game")]
public class GameData : ScriptableObject
{
    [Tooltip("Tiempo de partida en minutos.")]
    public float timeInMinutes;
    public List<EnemySpawn> spawns;
    public GameObject spawnParticlesPrefab;
    [Tooltip("Tiempo que tarda un enemigo en spawnear.")]
    public float timeBetweenSpawn;
    [Tooltip("Maximo de enemigos que puede haber en pantalla.")]
    public int maxEnemies;
    [Tooltip("Límites del nivel")]
    public Vector2 levelLimit;
    [Tooltip("Obstáculos del nivel que evitará el spawner de enemigos.")]
    public LayerMask obstaclesLayer;
}