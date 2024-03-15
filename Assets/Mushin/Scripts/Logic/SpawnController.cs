using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    public float NextEnemySpawnerMinute => _spawnersData[_index].minuteToStartSpawning;

    private const string SPAWN_PARTICLES_TAG = "spawnParticles";

    private GameData _gameData;

    private List<EnemySpawn> _spawnersData;
    private List<string> _activeSpawners;
    private int _maxEnemies;
    private int _currentEnemies;
    private float _timeBetweenSpawn;
    private float _timer;

    private LayerMask _obstaclesLayer;
    private float _xLimit;
    private float _yLimit;
    private int _index;
    private bool _spawnersEnabled;

    private void Awake()
    {
        _spawnersData = new();
        _activeSpawners = new();
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
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
                StartCoroutine(Spawn());
        }
    }

    public void Init(GameData data)
    {
        _gameData = data;
        _maxEnemies = data.maxEnemies;
        _timeBetweenSpawn = data.timeBetweenSpawn;
        _spawnersData = data.spawns;
        _xLimit = data.levelLimit.x;
        _yLimit = data.levelLimit.y;
        _index = 0;
        _spawnersEnabled = true;
        _obstaclesLayer = data.obstaclesLayer;
    }

    public void AddSpawner()
    {
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

    private IEnumerator Spawn()
    {
        ResetTimer();
        var pos = RandomSpawnPos();
        ObjectPooler.Instance.SpawnFromPool(SPAWN_PARTICLES_TAG, pos);
        yield return new WaitForSeconds(1f);
        ObjectPooler.Instance.SpawnFromPool(GetRandomEnemyTag(), pos);
        AddEnemy();
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
        while (true)
        {
            var x = Random.Range(-_xLimit, _xLimit);
            var y = Random.Range(-_yLimit, _yLimit);
            if (Physics2D.OverlapCircle(new Vector2(x, y), 1f, _obstaclesLayer)) continue;
            return new Vector2(x, y);
        }
    }

    public bool CanAddSpawner()
    {
        return _index < _spawnersData.Count;
    }

    private void EnableSpawners(bool value)
    {
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