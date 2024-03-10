using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    public float NextEnemySpawnerMinute => _spawnersData[_index].minuteToStartSpawning;
    [HideInInspector] public int enemiesKilled;

    private List<EnemySpawn> _spawnersData = new();
    private List<string> _activeSpawners = new();
    private int _maxEnemies;
    private int _currentEnemies;
    private float _timeBetweenSpawn;
    private float _timer;

    private float _xLimit;
    private float _yLimit;
    private int _index;
    private bool _spawnersEnabled;

    public static SpawnController Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        CheckMaxEnemies();
        if (_spawnersEnabled && _timer > 0f)
        {
            Debug.Log("Spawner timer: " + _timer);
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
                Spawn();
        }
    }
    
    public void Init(GameData data)
    {
        _maxEnemies = data.maxEnemies;
        _timeBetweenSpawn = data.timeBetweenSpawn;
        _spawnersData = data.spawns;
        _xLimit = data.levelLimit.x;
        _yLimit = data.levelLimit.y;
        _index = 0;
        _spawnersEnabled = true;
    }

    public void AddSpawner()
    {
        Debug.Log("Adding spawner");
        var currentTag = _spawnersData[_index].enemyTag;
        _activeSpawners.Add(currentTag);
        if (CanAddSpawner())
            _index++;
    }

    private void ResetTimer()
    {
        _timer = _timeBetweenSpawn;
    }
    
    private void CheckMaxEnemies()
    {
        if (_currentEnemies >= _maxEnemies && _spawnersEnabled)
            EnableSpawners(false);
        else if (_currentEnemies <= _maxEnemies - _activeSpawners.Count && !_spawnersEnabled)
            EnableSpawners(true);
    }

    private string GetRandomEnemyTag()
    {
        var randomIndex = Random.Range(0, _activeSpawners.Count);
        var randomTag = _activeSpawners[randomIndex];
        return randomTag;
    }

    private void Spawn()
    {
        Debug.Log("Spawning enemy");
        ObjectPooler.Instance.SpawnFromPool(GetRandomEnemyTag(), RandomSpawnPos());
        AddEnemy();
        ResetTimer();
    }

    private void AddEnemy()
    {
        _currentEnemies++;
        if (_currentEnemies >= _maxEnemies)
            EnableSpawners(false);
    }

    public void RemoveEnemy()
    {
        _currentEnemies--;
    }

    private Vector2 RandomSpawnPos()
    {
        var x = Random.Range(-_xLimit, _xLimit);
        var y = Random.Range(-_yLimit, _yLimit);
        return new Vector2(x, y);
    }
    
    public bool CanAddSpawner()
    {
        return _index < _spawnersData.Count;
    }

    private void EnableSpawners(bool value)
    {
        Debug.Log("Enabling spawners");
        _spawnersEnabled = value;
    }
    
    // OLD SPAWNERS BEHAVIOR
    // public Vector2 RandomSpawnPos()
    // {
    //     Vector2 newPosition;
    //     do
    //     {
    //         int spawnSide = Random.Range(0, 4);
    //         switch (spawnSide)
    //         {
    //             case 0: // Izquierda del viewport
    //                 newPosition = _camera.ViewportToWorldPoint(new Vector3(_minX, Random.Range(_minY, _maxY)));
    //                 break;
    //             case 1: // Arriba del viewport
    //                 newPosition = _camera.ViewportToWorldPoint(new Vector3(Random.Range(_minX, _maxX), _maxY));
    //                 break;
    //             case 2: // Derecha del viewport
    //                 newPosition = _camera.ViewportToWorldPoint(new Vector3(_maxX, Random.Range(_minY, _maxY)));
    //                 break;
    //             case 3: // Debajo del viewport
    //                 newPosition = _camera.ViewportToWorldPoint(new Vector3(Random.Range(_minX, _maxX), _minY));
    //                 break;
    //             default:
    //                 return Vector2.zero; // Valor predeterminado, aunque no debería llegar aquí.
    //         }
    //     } while (!IsPositionInsideMap(newPosition));
    //
    //     return newPosition;
    // }
    // private bool IsPositionInsideMap(Vector2 position)
    // {
    //     var x = position.x;
    //     var y = position.y;
    //     return x is < 57 and > -57 && y is < 32 and > -32;
    // }
}