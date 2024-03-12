using System.Threading.Tasks;

namespace Mushin.Scripts.Commands
{
    public class LoadGameSceneCommand : ICommand
    {
        public async Task Execute()
        {
            await new LoadSceneCommand("GameScene").Execute();
            await new StartGameplayCommand().Execute();
        }
    }
}