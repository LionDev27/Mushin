using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMediator _player;
    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _mainCanvas, _endCanvas, _pauseCanvas;
    [SerializeField] private TextMeshProUGUI _endTitle, _timeSurvived, _enemiesKilled;
    
    private SpawnController _spawnController;
    private float _timer;
    private bool _runTimer;
    private float _minutes;

    private void OnEnable()
    {
        _player.OnPlayerDead += () => EndGame(false);
    }

    private void OnDisable()
    {
        _player.OnPlayerDead -= () => EndGame(false);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        RunTimer();
        CheckSpawners();
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void StartGame()
    {
        AddSpawnController();
        ResetTimer();
        SetupCanvas();
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

    public void TogglePause()
    {
        _pauseCanvas.SetActive(!_pauseCanvas.activeInHierarchy);
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
    }

    private void AddSpawnController()
    {
        _spawnController = new GameObject("SpawnController").AddComponent<SpawnController>();
        _spawnController.transform.parent = transform;
        _spawnController.Init(_gameData);
    }
    
    private void SetupCanvas()
    {
        _mainCanvas.SetActive(true);
        _endCanvas.SetActive(false);
        _pauseCanvas.SetActive(false);
    }

    private void CheckSpawners()
    {
        if (!_spawnController.CanAddSpawner()) return;
        if (_minutes >= _spawnController.NextEnemySpawnerMinute)
            _spawnController.AddSpawner();
    }

    #region Timers

    private void ResetTimer()
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
        _timerText.text = $"{minutes:00}:{seconds:00}";
        if (minutes >= _gameData.timeInMinutes)
            EndGame(true);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_gameData.levelLimit.x * 2, _gameData.levelLimit.y * 2, 1f));
    }

    #endregion
}