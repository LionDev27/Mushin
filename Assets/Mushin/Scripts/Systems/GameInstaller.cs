using Mushin.Scripts.GameStates;
using UnityEngine;

namespace Mushin.Scripts.Systems
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private GameController _gameController;
        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(_cameraShake);
            ServiceLocator.Instance.RegisterService(_gameStateController);
            ServiceLocator.Instance.RegisterService(_gameController);
        }
    

        private void OnDestroy()
        {
            ServiceLocator.Instance.UnregisterService<CameraShake>();
            ServiceLocator.Instance.UnregisterService<GameStateController>();
            ServiceLocator.Instance.UnregisterService<GameController>();
        }
    }
}