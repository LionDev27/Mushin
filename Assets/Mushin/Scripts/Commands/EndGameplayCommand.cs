using System.Threading.Tasks;
using Mushin.Scripts.GameStates;

namespace Mushin.Scripts.Commands
{
    public class EndGameplayCommand : ICommand
    {
        private readonly bool _playerSurvived;

        public EndGameplayCommand(bool playerSurvived)
        {
            _playerSurvived = playerSurvived;
        }
        public Task Execute()
        {
            //ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
            ServiceLocator.Instance.GetService<GameStateController>().SwitchState(_playerSurvived ? GameStatesIds.VICTORY : GameStatesIds.GAMEOVER);

            return Task.CompletedTask;
        }
    }
}