using System.Threading.Tasks;
using Mushin.Scripts.GameStates;

namespace Mushin.Scripts.Commands
{
    public class StartGameplayCommand : ICommand
    {
        public Task Execute()
        {
            // await new FadeInCommand().Execute();
            var gameStateController = ServiceLocator.Instance.GetService<GameStateController>();
            gameStateController.SwitchState(new PlayingState(gameStateController.GameData));
            //await new FadeOutCommand().Execute();
            return Task.CompletedTask;
        }
    }
}