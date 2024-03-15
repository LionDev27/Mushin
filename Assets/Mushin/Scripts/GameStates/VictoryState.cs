using Mushin.Scripts.Events;
using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class VictoryState : IGameState
    {
        private readonly int _enemiesKilled;
        private readonly float _minutes;
        private readonly float _seconds;

        public VictoryState(int enemiesKilled, float minutes, float seconds)
        {
            _enemiesKilled = enemiesKilled;
            _minutes = minutes;
            _seconds = seconds;
        }

        public void Start()
        {
            Debug.Log("victory start");

            ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new VictoryEventData(_enemiesKilled, _minutes, _seconds));
        }

        public void Stop()
        {
            Debug.Log("victory stop");
        }

        public void Update()
        {
        }
    }
}