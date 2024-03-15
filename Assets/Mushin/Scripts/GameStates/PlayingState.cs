using Mushin.Scripts.Events;
using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class PlayingState : IGameState, IEventObserver
    {
        private readonly GameData _gameData;

        private EventQueue _eventQueue;
        private SpawnController _spawnController;

        private int _enemiesKilled;
        private float _minutes;
        private float _seconds;
        private bool _runTimer;
        private float _timeInSeconds;
        private Player.Player _player;

        public PlayingState(GameData gameData)
        {
            _gameData = gameData;
        }
        
        public void Start()
        {
            Debug.Log("playing start");
            _player = ServiceLocator.Instance.GetService<Player.Player>();
            _player.ResetPlayer();
            //ResetSpawner
            _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
            ResetTimer();
            _spawnController = ServiceLocator.Instance.GetService<SpawnController>();
            _eventQueue.Subscribe(EventIds.ENEMYKILLED, this);
            _eventQueue.Subscribe(EventIds.PLAYERKILLED, this);
        }

        public void Stop()
        {
            Debug.Log("playing stop");
            StopTimer();
        }

        public void Update()
        {
            RunTimer();
            CheckSpawners();
            CheckVictory();
        }

        private void CheckVictory()
        {
            if (_minutes <= 0 && _seconds <= 0)
            {
                ServiceLocator.Instance.GetService<GameStateController>().SwitchState(new VictoryState(_enemiesKilled, _minutes, _seconds));
            }
        }
        private void CheckSpawners()
        {
            if (!_spawnController.CanAddSpawner()) return;
            if (_minutes <= _spawnController.NextEnemySpawnerMinute)
            {
                _spawnController.AddSpawner();
            }
        }

        public void ProcessEvents(EventData eventData)
        {
            if (eventData.EventId == EventIds.ENEMYKILLED)
            {
                _enemiesKilled++;
            }
            else if (eventData.EventId == EventIds.PLAYERKILLED)
            {
                ServiceLocator.Instance.GetService<GameStateController>().SwitchState(new GameOverState(_enemiesKilled, _minutes, _seconds));
            }
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.ENEMYKILLED, this);
            ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.PLAYERKILLED, this);
        }

        #region Timer

        private void ResetTimer()
        {
            _timeInSeconds = _gameData.timeInMinutes * 60;
            _minutes = _timeInSeconds / 60f;
            _seconds = _timeInSeconds % 60f;
            _runTimer = true;
            _eventQueue.EnqueueEvent(new TimeUpdatedEventData(_minutes, _seconds));
        }

        private void StopTimer()
        {
            _runTimer = false;
        }

        private void RunTimer()
        {
            if (!_runTimer) return;

            _timeInSeconds -= Time.deltaTime;

            _minutes = _timeInSeconds / 60f;
            _seconds = _timeInSeconds % 60f;

            _eventQueue.EnqueueEvent(new TimeUpdatedEventData(_minutes, _seconds));
        }

        #endregion
    }
}