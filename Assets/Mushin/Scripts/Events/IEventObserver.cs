namespace Mushin.Scripts.Events
{
    public interface IEventObserver
    {
        public void ProcessEvents(EventData eventData);
    }
}