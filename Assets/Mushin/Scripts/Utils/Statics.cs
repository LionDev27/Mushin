using UnityEngine;

namespace Mushin.Scripts.Utils
{
    public class Statics
    {
        public static string FormatTime(float minutes, float seconds)
        {
            var wholeMinutes = Mathf.FloorToInt(minutes);
            var wholeSeconds = Mathf.FloorToInt(seconds);
            return $"{wholeMinutes:00}:{wholeSeconds:00}";
        }
    }
}