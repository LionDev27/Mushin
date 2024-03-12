using System;
using Mushin.Scripts.Commands;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMediator _player;
    [SerializeField] private GameData _gameData;
    [SerializeField] private TimerView _timerView;

    private SpawnController _spawnController;

    //private int _playerLevel;
    private int _enemiesKilled;
    public Action<int, float, float> OnGameOver;
    public Action<int, float, float> OnVictory;

    private bool _runTimer;
    private float _timer;
    private float _minutes;
    private float _seconds;

    private void Awake()
    {
        _player.OnPlayerDead += GameOver;
    }

    private void OnDestroy()
    {
        _player.OnPlayerDead -= GameOver;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        RunTimer();
        CheckSpawners();
        CheckVictory();
    }


    private void StartGame()
    {
        //meter esto en StartGameplayCommand
        //ResetPlayer
        AddSpawnController();
        ResetTimer();
    }

    private void CheckVictory()
    {
        if (_minutes <= 0 && _seconds <= 0)
        {
            Victory();
        }
    }

    private void GameOver()
    {
        StopTimer();
        _enemiesKilled = _spawnController.enemiesKilled;
        OnGameOver?.Invoke(_enemiesKilled, _minutes, _seconds);
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new EndGameplayCommand(false));
    }

    private void Victory()
    {
        StopTimer();
        _enemiesKilled = _spawnController.enemiesKilled;
        OnVictory?.Invoke(_enemiesKilled, _minutes, _seconds);
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new EndGameplayCommand(true));
    }

    private void AddSpawnController()
    {
        _spawnController = new GameObject("SpawnController").AddComponent<SpawnController>();
        _spawnController.transform.parent = transform;
        _spawnController.Init(_gameData);
    }


    private void CheckSpawners()
    {
        if (!_spawnController.CanAddSpawner()) return;
        if (_minutes <= _spawnController.NextEnemySpawnerMinute)
        {
            _spawnController.AddSpawner();
        }
    }

    #region Timers

    private void ResetTimer()
    {
        _timer = _gameData.timeInMinutes * 60;
        _runTimer = true;

        _timerView.ResetTime(_timer);
    }

    private void StopTimer()
    {
        _runTimer = false;
    }

    private void RunTimer()
    {
        if (!_runTimer) return;

        _timer -= Time.deltaTime;

        _minutes = _timer / 60f;
        _seconds = _timer % 60f;

        _timerView.UpdateTime(_minutes, _seconds);
    }

    #endregion
}