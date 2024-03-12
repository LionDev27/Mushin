using Mushin.Scripts.Utils;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void UpdateTime(float minutes, float seconds)
    {
        _timerText.text = Statics.FormatTime(minutes, seconds);
    }

    public void ResetTime(float timeInMinutes)
    {
        UpdateTime(timeInMinutes,0);
    }
}
