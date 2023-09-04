using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsContainer : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;

    private List<Image> _hearts = new();
    private GridLayoutGroup _gridLayout;
    private int LastHeartIndex => _hearts.Count - 1;
    private int _enabledHearts;

    public static Action OnAddHeart;
    public static Action<bool> OnEnableHeart;
    public static Action OnRemoveHeart;

    private void OnEnable()
    {
        OnAddHeart += AddHeart;
        OnEnableHeart += EnableHeart;
        OnRemoveHeart += RemoveHeart;
    }

    private void OnDisable()
    {
        OnAddHeart -= AddHeart;
        OnEnableHeart -= EnableHeart;
        OnRemoveHeart -= RemoveHeart;
    }

    private void AddHeart()
    {
        var heart = Instantiate(_heartPrefab, transform).GetComponent<Image>();
        _hearts.Add(heart);
        EnableHeart(true);
    }

    private void EnableHeart(bool value)
    {
        _enabledHearts += value ? 1 : -1;
        UpdateColors();
    }

    private void RemoveHeart()
    {
        if (_hearts[LastHeartIndex].color == Color.white)
            EnableHeart(false);
        Destroy(_hearts[LastHeartIndex].gameObject);
        _hearts.RemoveAt(LastHeartIndex);
    }

    private void SetHeartColor(Color color)
    {
        var index = _enabledHearts != 0 ? _enabledHearts - 1 : 0;
        _hearts[index].color = color;
    }

    private void UpdateColors()
    {
        for (var i = 0; i < _hearts.Count; i++)
        {
            for (var j = 0; j < _enabledHearts; j++)
                _hearts[i].color = i > j ? Color.black : Color.white;
        }
    }
}
