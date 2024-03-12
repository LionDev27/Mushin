using System.Threading.Tasks;
using Mushin.Scripts.GameStates;
using UnityEngine;

namespace Mushin.Scripts.Commands
{
    public class StartGameplayCommand : ICommand
    {
        public async Task Execute()
        {
            await new ShowScreenFadeCommand().Execute();
            ServiceLocator.Instance.GetService<GameStateController>().SwitchState(GameStatesIds.PLAYING);
            await new HideScreenFadeCommand().Execute();
        }
    }
}