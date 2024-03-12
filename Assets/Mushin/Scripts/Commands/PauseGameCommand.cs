using System.Threading.Tasks;
using UnityEngine;

namespace Mushin.Scripts.Commands
{
    public class PauseGameCommand:ICommand
    {
        public Task Execute()
        {
            Time.timeScale = 0;
            return Task.CompletedTask;
        }
    }
}