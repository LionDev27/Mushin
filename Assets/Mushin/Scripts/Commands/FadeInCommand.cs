using System.Threading.Tasks;

namespace Mushin.Scripts.Commands
{
    public class FadeInCommand : ICommand
    {
        public async Task Execute()
        {
            await ServiceLocator.Instance.GetService<ScreenFade>().FadeIn();
        }
    }
}