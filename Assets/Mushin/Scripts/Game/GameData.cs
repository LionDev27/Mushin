using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game")]
public class GameData : ScriptableObject
{
    [Tooltip("Tiempo de partida en minutos.")]
    public float timeInMinutes;
    public List<EnemySpawn> spawns;
    [Tooltip("Tiempo que tarda un enemigo en spawnear.")]
    public float timeBetweenSpawn;
    [Tooltip("Maximo de enemigos que puede haber en pantalla.")]
    public int maxEnemies;
    [Tooltip("Limite en X y en Y del mapa.")]
    public float xLimit, yLimit;
}