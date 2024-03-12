using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class GameOverState : IGameState
    {
        public void Start()
        {
            Debug.Log("game over start");
            //Eevent
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