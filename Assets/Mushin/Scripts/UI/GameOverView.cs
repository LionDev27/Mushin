using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameFacade _gameFacade;
    [SerializeField] private SceneLoader _sceneLoader;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _menuButton.onClick.AddListener(GoToMainMenu);
        Hide();
    }

    private void GoToMainMenu()
    {
        Hide();
        _sceneLoader.LoadScene("MainMenu");
    }

    private void RestartGame()
    {
        Hide();
        _gameFacade.StartFight();
    }

    private void Show()
    {
        _gameFacade.StopFight();
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}