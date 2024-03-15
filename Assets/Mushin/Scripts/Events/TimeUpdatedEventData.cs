namespace Mushin.Scripts.Events
{
    public class TimeUpdatedEventData : EventData
    {
        public readonly float Minutes;
        public readonly float Seconds;

        public TimeUpdatedEventData(float minutes, float seconds) : base(EventIds.TIMEUPDATED)
        {
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}