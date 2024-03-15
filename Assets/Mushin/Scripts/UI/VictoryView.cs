using Mushin.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mushin.Scripts.UI
{
    public class VictoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _timeSurvived, _enemiesKilled;
        private IGameMenu _gameMenu;

        public void Configure(GameMenuMediator gameMenu)
        {
            _gameMenu = gameMenu;
        }

        private void Awake()
        {
            _menuButton.onClick.AddListener(GoToMainMenu);
        }

        public void Show(int enemiesKilled, float minutesSurvived, float secondsSurvived)
        {
            UpdateFinalStats(enemiesKilled, minutesSurvived, secondsSurvived);
            _panel.SetActive(true);
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }

        private void UpdateFinalStats(int enemiesKilled, float minutesSurvived, float secondsSurvived)
        {
            _enemiesKilled.text = enemiesKilled.ToString();
            _timeSurvived.text = Statics.FormatTime(minutesSurvived, secondsSurvived);
        }

        private void GoToMainMenu()
        {
            _gameMenu.OnBackToMenuButtonPressed();
        }

        private void RestartGame()
        {
            _gameMenu.OnRestartButtonPressed();
        }
    }
}