using System.Threading.Tasks;
using Mushin.Scripts.Camera;

namespace Mushin.Scripts.Commands
{
    public class CameraShakeCommand : ICommand
    {
        private readonly float _intensity;

        public CameraShakeCommand(float intensity)
        {
            _intensity = intensity;
        }
        public Task Execute()
        {
            ServiceLocator.Instance.GetService<CameraShake>().Shake(_intensity);
            return Task.CompletedTask;
        }
    }
}