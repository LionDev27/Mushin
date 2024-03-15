namespace Mushin.Scripts.GameStates
{
    public interface IGameState
    {
        public void Start();
        public void Stop();
        void Update();
    }
}