namespace Mushin.Scripts.Events
{
    public class GameOverEventData : EventData
    {
        public readonly int EnemiesKilled;
        public readonly float MinutesRemaining;
        public readonly float SecondsRemaining;

        public GameOverEventData(int enemiesKilled, float minutesRemaining, float secondsRemaining) : base(EventIds.GAMEOVER)
        {
            EnemiesKilled = enemiesKilled;
            MinutesRemaining = minutesRemaining;
            SecondsRemaining = secondsRemaining;
        }
    }
}