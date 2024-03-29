﻿using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mushin.Scripts.Commands
{
    public class ScreenFade : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private bool _isFinished;

        private void Awake()
        {
            _image.enabled = false;
        }

        public async Task FadeIn()
        {
            _isFinished = false;
            _image.SetAlpha(0);
            _image.enabled = true;
            _image.DOFade(1, .5f).Play().OnComplete(() =>
            {
                _image.SetAlpha(1);
                _isFinished=true;
            });
            while (!_isFinished)
            {
                await Task.Yield();
            }
        }

        public async Task FadeOut()
        {
            _isFinished = false;
            _image.SetAlpha(1);
            _image.enabled = true;
            _image.DOFade(0, .5f).Play().OnComplete(() =>
            {
                _image.enabled = false;
                _isFinished = true;
            });
            while (!_isFinished)
            {
                await Task.Yield();
            }
        }
    }
}