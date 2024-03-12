using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mushin.Scripts.GameStates
{
    public class GameStateController : MonoBehaviour
    {
        private IGameState _currentState;
        private Dictionary<GameStatesIds, IGameState> _idToGameState;

        private void Awake()
        {
            _idToGameState = new Dictionary<GameStatesIds, IGameState>
            {
                { GameStatesIds.INITIAL, new InitialState() },
                { GameStatesIds.PLAYING, new PlayingState() },
                { GameStatesIds.GAMEOVER, new GameOverState() },
                { GameStatesIds.VICTORY, new VictoryState() },
            };
        }

        private void Start()
        {
            _currentState = GetState(GameStatesIds.INITIAL);
        }

        private void Update()
        {
            _currentState.Update();
        }

        private void Reset()
        {
            SwitchState(GameStatesIds.INITIAL);
        }

        public void SwitchState(GameStatesIds newGameStateIds)
        {
            _currentState.Stop();
            _currentState = GetState(newGameStateIds);
            _currentState.Start();
        }

        private IGameState GetState(GameStatesIds gameStateIds)
        {
            return _idToGameState[gameStateIds];
        }

        private void OnDestroy()
        {
            Reset();
        }
    }
}