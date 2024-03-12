using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class PlayingState : IGameState
    {
        public void Start()
        {
            Debug.Log("playing start");
            
        }

        public void Stop()
        {
            Debug.Log("playing stop");
        }

        public void Update()
        {
            
        }
    }
}