using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private SpawnController _controller;
    private string _enemyTag;
    private bool _canSpawn;
    private float _timeBetweenSpawn;
    private float _timer;

    private void Update()
    {
        if (_canSpawn && _timer > 0f)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
                Spawn();
        }
    }

    public void Init(SpawnController controller, string enemyTag, float timeBetweenSpawn)
    {
        _controller = controller;
        _enemyTag = enemyTag;
        _timeBetweenSpawn = timeBetweenSpawn;
        ResetTimer();
    }
    
    public void EnableSpawn(bool value)
    {
        _canSpawn = value;
    }

    private void Spawn()
    {
        ObjectPooler.Instance.SpawnFromPool(_enemyTag, _controller.RandomSpawnPos());
        ResetTimer();
    }

    private void ResetTimer()
    {
        _timer = _timeBetweenSpawn;
    }
}