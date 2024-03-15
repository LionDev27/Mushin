using Mushin.Scripts.Camera;
using Mushin.Scripts.Events;
using Mushin.Scripts.GameStates;
using UnityEngine;

namespace Mushin.Scripts.Systems
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private SpawnController _spawnController;
        [SerializeField] private EventQueue _eventQueue;
        [SerializeField] private GameData _gameData;
        [SerializeField] private Player.Player _player;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(_player);
            ServiceLocator.Instance.RegisterService(_cameraShake);
            ServiceLocator.Instance.RegisterService(_gameStateController);
            ServiceLocator.Instance.RegisterService(_eventQueue);
            ServiceLocator.Instance.RegisterService(_spawnController);
            _gameStateController.Init(_gameData);
            _spawnController.Init(_gameData);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.UnregisterService<Player.Player>();
            ServiceLocator.Instance.UnregisterService<CameraShake>();
            ServiceLocator.Instance.UnregisterService<GameStateController>();
            ServiceLocator.Instance.UnregisterService<EventQueue>();
            ServiceLocator.Instance.UnregisterService<SpawnController>();
        }
    }
}