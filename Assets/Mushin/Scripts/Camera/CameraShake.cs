using Cinemachine;
using UnityEngine;

namespace Mushin.Scripts.Camera
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;
        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void Shake(float intensity = 1f)
        {
            _impulseSource.GenerateImpulse(intensity);
        }
    }
}
