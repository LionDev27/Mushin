using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] private Transform _firstPosition, _lastPosition;
    private GameObject _button;

    public GameObject Spawn(GameObject prefab)
    {
        _button = Instantiate(prefab, transform);
        _button.transform.position = _firstPosition.position;
        StartAnimation();
        return _button;
    }

    private void StartAnimation()
    {
        _button.transform.DOMove(_lastPosition.position, 0.25f).SetEase(Ease.InOutBounce).SetUpdate(true).Play()
            .OnComplete(() => _button.GetComponent<Button>().enabled = true);
    }

    public void EndAnimation()
    {
        _button.transform.DOMove(_firstPosition.position, 0.25f).SetEase(Ease.InOutBounce).SetUpdate(true).Play();
    }
}