using System.Threading.Tasks;

namespace Mushin.Scripts.Commands
{
    public interface ICommand
    {
        Task Execute();
    }
}