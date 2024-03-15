using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class GameStateController : MonoBehaviour
    {
        private IGameState _currentState;
        public GameData GameData { get; private set; }

        public void Init(GameData gameData)
        {
            GameData = gameData;
        }

        private void Start()
        {
            _currentState = new InitialState();
            _currentState.Start();
        }

        private void Update()
        {
            _currentState.Update();
        }

        private void Reset()
        {
            SwitchState(new InitialState());
        }

        public void SwitchState(IGameState newGameState)
        {
            _currentState.Stop();
            _currentState = newGameState;
            _currentState.Start();
        }

        private void OnDestroy()
        {
            Reset();
        }
    }
}