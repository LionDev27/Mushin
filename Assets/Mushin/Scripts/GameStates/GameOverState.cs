using Mushin.Scripts.Events;
using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class GameOverState : IGameState
    {
        private readonly int _enemiesKilled;
        private readonly float _minutes;
        private readonly float _seconds;

        public GameOverState(int enemiesKilled, float minutes, float seconds)
        {
            _enemiesKilled = enemiesKilled;
            _minutes = minutes;
            _seconds = seconds;
        }

        public void Start()
        {
            Debug.Log("game over start");
            ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new GameOverEventData(_enemiesKilled, _minutes, _seconds));
        }

        public void Stop()
        {
            Debug.Log("game over stop");
        }

        public void Update()
        {
        }
    }
}