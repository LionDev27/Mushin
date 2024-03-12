using System.Threading.Tasks;
using DG.Tweening;

namespace Mushin.Scripts.Commands
{
    public class CameraShakeCommand : ICommand
    {
        private readonly float _duration;
        private readonly float _strength;

        private readonly float _intensity;

        public CameraShakeCommand(float duration, float strength)
        {
            _duration = duration;
            _strength = strength;
        }
        public CameraShakeCommand(float intensity)
        {
            _intensity = intensity;
        }
        public Task Execute()
        {
            //Camera.main.DOShakePosition(_duration,_strength);
            ServiceLocator.Instance.GetService<CameraShake>().Shake(_intensity);
            return Task.CompletedTask;
        }
    }
}