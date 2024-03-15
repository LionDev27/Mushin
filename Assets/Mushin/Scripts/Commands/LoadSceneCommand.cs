using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Mushin.Scripts.Commands
{
    public class LoadSceneCommand : ICommand
    {
        private readonly string _sceneName;

        public LoadSceneCommand(string sceneName)
        {
            _sceneName = sceneName;
        }
        public async Task Execute()
        {
            await new FadeInCommand().Execute();
            var loadingScreen = ServiceLocator.Instance.GetService<LoadingScreen>();
            loadingScreen.Show();
            await LoadScene(_sceneName);
            //await Task.Delay(500);
            loadingScreen.Hide();
            await new FadeOutCommand().Execute();
        }

        private async Task LoadScene(string sceneName)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }
            await Task.Yield();
        }
    }
}