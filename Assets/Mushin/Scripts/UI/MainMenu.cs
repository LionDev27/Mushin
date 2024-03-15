using MelenitasDev.SoundsGood;
using Mushin.Scripts.Commands;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    private Music _menuMusic;
    private void Awake()
    {
        _startGameButton.onClick.AddListener(OnStartButtonPressed);
        _optionsButton.onClick.AddListener(OnOptionsButtonPressed);
        _exitButton.onClick.AddListener(OnExitButtonPressed);
        
        EventSystem.current.SetSelectedGameObject(_startGameButton.gameObject);

        _menuMusic = new Music(Track.MainMenuMusic);
        _menuMusic.SetVolume(.5f).SetLoop(true).SetFadeOut(1).SetOutput(Output.Music).Play(1f);
    }

    private void OnStartButtonPressed()
    {
        _menuMusic.Stop(1);
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new LoadGameSceneCommand());
    }

    private void OnOptionsButtonPressed()
    {
    }

    private void OnExitButtonPressed()
    {
        Application.Quit();
    }
}