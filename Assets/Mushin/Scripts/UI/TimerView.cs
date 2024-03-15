using Mushin.Scripts.Events;
using Mushin.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Mushin.Scripts.UI
{
    public class TimerView : MonoBehaviour, IEventObserver
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private void Start()
        {
            ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.TIMEUPDATED, this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.TIMEUPDATED, this);
        }

        private void UpdateTime(float minutes, float seconds)
        {
            _timerText.text = Statics.FormatTime(minutes, seconds);
        }

        public void ProcessEvents(EventData eventData)
        {
            if (eventData.EventId == EventIds.TIMEUPDATED)
            {
                var timeData = (TimeUpdatedEventData)eventData;
                UpdateTime(timeData.Minutes, timeData.Seconds);
                return;
            }
        }
    }
}