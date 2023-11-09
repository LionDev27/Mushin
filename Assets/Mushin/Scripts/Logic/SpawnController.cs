using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float NextEnemySpawnerMinute => _spawnersData[_index].minuteToStartSpawning;
    [HideInInspector]
    public int enemiesKilled;
    
    private List<EnemySpawn> _spawnersData = new();
    private List<EnemySpawner> _activeSpawners = new();
    private int _maxEnemies;
    private int _currentEnemies;
    private float _timeBetweenSpawn;
    private float _xLimit, _yLimit;
    private int _index;
    private bool _spawnersEnabled;

    public static SpawnController Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Update()
    {
        if (_currentEnemies >= _maxEnemies && _spawnersEnabled)
            EnableSpawners(false);
        else if (_currentEnemies <= _maxEnemies - _activeSpawners.Count && !_spawnersEnabled)
            EnableSpawners(true);
    }

    public void Init(GameData data)
    {
        _maxEnemies = data.maxEnemies;
        _timeBetweenSpawn = data.timeBetweenSpawn;
        _xLimit = data.xLimit;
        _yLimit = data.yLimit;
        _spawnersData = data.spawns;
        _index = 0;
        _spawnersEnabled = true;
    }

    public void AddSpawner()
    {
        var currentTag = _spawnersData[_index].enemyTag;
        EnemySpawner newSpawner = new GameObject($"{currentTag}Spawner").AddComponent<EnemySpawner>();
        newSpawner.transform.parent = transform;
        newSpawner.Init(this, currentTag, _timeBetweenSpawn);
        _activeSpawners.Add(newSpawner);
        newSpawner.EnableSpawn(true);
        if (CanAddSpawner())
            _index++;
    }

    public void AddEnemy()
    {
        _currentEnemies++;
        if (_currentEnemies >= _maxEnemies)
            EnableSpawners(false);
    }

    public void RemoveEnemy()
    {
        _currentEnemies--;
    }

    public Vector2 RandomSpawnPos()
    {
        var value = Random.Range(0, 2);
        float xValue, yValue;
        if (value == 0)
        {
            xValue = RandomSign(_xLimit);
            yValue = Random.Range(-_yLimit, _yLimit);
        }
        else
        {
            xValue = Random.Range(-_xLimit, _xLimit);
            yValue = RandomSign(_yLimit);
        }
        return new Vector2(xValue, yValue);
    }

    public bool CanAddSpawner()
    {
        return _index < _spawnersData.Count;
    }

    private float RandomSign(float limit)
    {
        var sign = Random.Range(0, 2);
        return sign == 0 ? limit : -limit;
    }

    private void EnableSpawners(bool value)
    {
        Debug.Log("Enabling spawners");
        _spawnersEnabled = value;
        foreach (var spawner in _activeSpawners)
            spawner.EnableSpawn(value);
    }
}