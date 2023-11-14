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

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private int _index;
    private bool _spawnersEnabled;

    public static SpawnController Instance;
    private Camera _camera;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    private void Start()
    {
        _camera = Camera.main;
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
        _minX = data.minX;
        _maxX = data.maxX;
        _minY = data.minY;
        _maxY = data.maxY;
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
        int spawnSide = Random.Range(0, 4);

        switch (spawnSide)
        {
            case 0: // Izquierda del viewport
                return _camera.ViewportToWorldPoint(new Vector3(_minX, Random.Range(_minY, _maxY)));

            case 1: // Arriba del viewport
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(_minX, _maxX), _maxY));

            case 2: // Derecha del viewport
                return _camera.ViewportToWorldPoint(new Vector3(_maxX, Random.Range(_minY, _maxY)));

            case 3: // Debajo del viewport
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(_minX, _maxX), _minY));

            default:
                return Vector2.zero; // Valor predeterminado, aunque no debería llegar aquí.
        }
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