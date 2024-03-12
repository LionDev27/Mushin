using System.Threading.Tasks;

namespace Mushin.Scripts.Commands
{
    public class HideScreenFadeCommand:ICommand{
        public async Task Execute()
        {
            await ServiceLocator.Instance.GetService<ScreenFade>().Hide();
        }
    }
}