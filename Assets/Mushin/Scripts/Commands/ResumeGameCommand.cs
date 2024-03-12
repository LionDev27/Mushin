using System.Threading.Tasks;
using UnityEngine;

namespace Mushin.Scripts.Commands
{
    public class ResumeGameCommand:ICommand
    {
        public Task Execute()
        {
            Time.timeScale = 1;
            return Task.CompletedTask;
        }
    }
}