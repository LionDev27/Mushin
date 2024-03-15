using UnityEngine;
using UnityEngine.UI;

namespace Mushin.Scripts.UI
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _backToMenuButton;
        private IGameMenu _gameMenu;
        private void Awake()
        {
            _resumeButton.onClick.AddListener(OnResume);
            _settingsButton.onClick.AddListener(OnSettings);
            _restartButton.onClick.AddListener(OnRestart);
            _backToMenuButton.onClick.AddListener(OnBackToMenu);
        }


        public void Configure(IGameMenu gameMenu)
        {
            _gameMenu = gameMenu;
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        private void OnResume()
        {
            _gameMenu.OnResumeButtonPressed();
        }
        private void OnSettings()
        {
            _gameMenu.OnSettingsButtonPressed();
        }
        private void OnRestart()
        {
            _gameMenu.OnRestartButtonPressed();
        }
        private void OnBackToMenu()
        {
            _gameMenu.OnBackToMenuButtonPressed();
        }


    }
}