using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MelenitasDev.SoundsGood;
using Mushin.Scripts.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mushin.Scripts.UI
{
    public class SplashCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text _pressAnyKeyText;
        private bool _hasStarted;
        private TweenerCore<Color, Color, ColorOptions> _blinkDotween;
        private Music _backMusic;
        private void Awake()
        {
            _blinkDotween = _pressAnyKeyText.DOFade(0, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            _backMusic = new Music(Track.TitleBackMusic);
            _backMusic.SetVolume(.5f).SetLoop(true).SetFadeOut(1).SetOutput(Output.Music).Play(2f);
        }

        private void OnAny(InputValue value)
        {
            if (_hasStarted) return;
            GoToMainMenu();
        }

        private async void GoToMainMenu()
        {
            _hasStarted = true;
            _pressAnyKeyText.enabled = false;
            
            _backMusic.Stop(1);
            
            await new LoadSceneCommand("MainMenu").Execute();
        }

        private void Update()
        {
            _blinkDotween.Play();
        }
    }
}