using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action OnPlayerDead;
    
    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _mainCanvas, _endCanvas;
    [SerializeField] private TextMeshProUGUI _endTitle, _timeSurvived, _enemiesKilled;
    
    private SpawnController _spawnController;
    private float _timer;
    private bool _runTimer;
    private float _minutes;

    private void OnEnable()
    {
        OnPlayerDead += () => EndGame(false);
    }

    private void OnDisable()
    {
        OnPlayerDead -= () => EndGame(false);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        RunTimer();
        CheckSpawners();
    }

    private void StartGame()
    {
        AddSpawnController();
        SetTimer();
    }

    private void EndGame(bool win)
    {
        _mainCanvas.SetActive(false);
        _endTitle.text = win ? "YOU WIN!" : "GAME OVER";
        _timeSurvived.text = _timerText.text;
        _enemiesKilled.text = _spawnController.enemiesKilled.ToString();
        _endCanvas.SetActive(true);
        Time.timeScale = 0f;
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
        if (_minutes >= _spawnController.NextEnemySpawnerMinute)
            _spawnController.AddSpawner();
    }

    #region Timers

    private void SetTimer()
    {
        _timer = 0f;
        _runTimer = true;
    }
    
    private void RunTimer()
    {
        if (!_runTimer) return;
        _timer += Time.deltaTime;
        _minutes = _timer / 60f;
        var minutes = Mathf.FloorToInt(_timer / 60f);
        var seconds = Mathf.FloorToInt(_timer - minutes * 60);
        _timerText.text = $"{minutes:0}:{seconds:00}";
        if (minutes >= _gameData.timeInMinutes)
            EndGame(true);
    }

    #endregion
}