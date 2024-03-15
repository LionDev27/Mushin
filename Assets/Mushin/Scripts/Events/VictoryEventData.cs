namespace Mushin.Scripts.Events
{
    public class VictoryEventData : EventData
    {
        public readonly int EnemiesKilled;
        public readonly float MinutesRemaining;
        public readonly float SecondsRemaining;

        public VictoryEventData(int enemiesKilled, float minutesRemaining, float secondsRemaining) : base(EventIds.VICTORY)
        {
            EnemiesKilled = enemiesKilled;
            MinutesRemaining = minutesRemaining;
            SecondsRemaining = secondsRemaining;
        }
    }
}