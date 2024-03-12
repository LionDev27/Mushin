using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class VictoryState : IGameState
    {
        public void Start()
        {
            Debug.Log("victory start");
            //Event
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