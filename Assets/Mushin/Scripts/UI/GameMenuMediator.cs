using Mushin.Scripts.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuMediator : MonoBehaviour, IGameMenu
{
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private VictoryView _victoryView;
    private CommandQueue _commandQueue;
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
        _commandQueue=ServiceLocator.Instance.GetService<CommandQueue>();
        HideAllMenus();
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
        HideAllMenus();
        _commandQueue.AddCommand(new ResumeGameCommand());
        _commandQueue.AddCommand(new EndGameplayCommand(false));
        _commandQueue.AddCommand(new StartGameplayCommand());
    }

    public void OnBackToMenuButtonPressed()
    {
        _commandQueue.AddCommand(new LoadSceneCommand("MainMenu"));
        _commandQueue.AddCommand(new ResumeGameCommand());
    }
    
}