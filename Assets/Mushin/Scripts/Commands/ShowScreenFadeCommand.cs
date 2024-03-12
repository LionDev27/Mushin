using System.Threading.Tasks;

namespace Mushin.Scripts.Commands
{
    public class ShowScreenFadeCommand : ICommand
    {
        public async Task Execute()
        {
            await ServiceLocator.Instance.GetService<ScreenFade>().Show();
        }
    }
}