using Mushin.Scripts.Commands;
using Mushin.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mushin.Scripts.UI
{
    public class GameMenuMediator : MonoBehaviour, IGameMenu, IEventObserver
    {
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private VictoryView _victoryView;

        private CommandQueue _commandQueue;
        private EventQueue _eventQueue;

        private bool _isPaused;

        private void Awake()
        {
            _isPaused = false;

            _pauseView.Configure(this);
            _gameOverView.Configure(this);
            _victoryView.Configure(this);
        }

        private void Start()
        {
            _commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();

            _eventQueue.Subscribe(EventIds.GAMEOVER, this);
            _eventQueue.Subscribe(EventIds.VICTORY, this);
            HideAllMenus();
        }

        private void OnDestroy()
        {
            _eventQueue.Unsubscribe(EventIds.GAMEOVER, this);
            _eventQueue.Unsubscribe(EventIds.VICTORY, this);
        }

        private void HideAllMenus()
        {
            _pauseView.Hide();
            _gameOverView.Hide();
            _victoryView.Hide();
        }

        private void OnEscape(InputValue value)
        {
            TogglePause();
        }

        private void TogglePause()
        {
            if (_isPaused)
            {
                OnResumeButtonPressed();
            }
            else
            {
                OnPauseButtonPressed();
            }

            _isPaused = !_isPaused;
        }

        public void OnPauseButtonPressed()
        {
            _pauseView.Show();
            _commandQueue.AddCommand(new PauseGameCommand());
        }

        public void OnResumeButtonPressed()
        {
            _commandQueue.AddCommand(new ResumeGameCommand());
            _pauseView.Hide();
        }

        public void OnSettingsButtonPressed()
        {
        }

        public void OnRestartButtonPressed()
        {
            //_commandQueue.AddCommand(new LoadGameSceneCommand());
            HideAllMenus();
            _commandQueue.AddCommand(new ResumeGameCommand());
            // _commandQueue.AddCommand(new EndGameplayCommand(false));
            _commandQueue.AddCommand(new StartGameplayCommand());
        }

        public void OnBackToMenuButtonPressed()
        {
            _commandQueue.AddCommand(new LoadSceneCommand("MainMenu"));
            _commandQueue.AddCommand(new ResumeGameCommand());
        }

        public void ProcessEvents(EventData eventData)
        {
            if (eventData.EventId == EventIds.GAMEOVER)
            {
                var gameOverData = (GameOverEventData)eventData;
                _gameOverView.Show(gameOverData.EnemiesKilled, gameOverData.MinutesRemaining, gameOverData.SecondsRemaining);
                return;
            }

            if (eventData.EventId == EventIds.VICTORY)
            {
                var victoryData = (VictoryEventData)eventData;
                _victoryView.Show(victoryData.EnemiesKilled, victoryData.MinutesRemaining, victoryData.SecondsRemaining);
                return;
            }
        }
    }
}