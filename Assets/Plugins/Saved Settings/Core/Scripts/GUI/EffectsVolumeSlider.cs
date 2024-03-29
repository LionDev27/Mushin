﻿using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    /// <summary>
    /// Syncs the slider value to the sound effects volume.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class EffectsVolumeSlider : BaseUILoadSetting
    {
        void Start()
        {
            Slider slider = GetComponent<Slider>();
            //LoadValue();
            slider.value = 1;
            slider.onValueChanged.AddListener(delegate { SettingsHelper.SoundEffectVolume = slider.value; });
        }

        public override void LoadValue()
        {
            GetComponent<Slider>().value = SettingsHelper.SoundEffectVolume;
        }
    }
}
