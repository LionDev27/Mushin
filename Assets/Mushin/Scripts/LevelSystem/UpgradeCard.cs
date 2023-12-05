using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeCard : MonoBehaviour
{
    private UpgradeData _data;

    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _title, _description;

    public void SetData(UpgradeData data)
    {
        _data = data;
        _sprite.sprite = _data.sprite;
        _title.text = _data.title;
        _description.text = _data.description;
    }

    public UpgradeData GetData()
    {
        return _data;
    }

    public void UpgradeStat()
    {
        PlayAnimation();
        PlayerUpgrades.OnUpgrade?.Invoke(_data);
    }

    private void PlayAnimation()
    {
        transform.DOShakeScale(0.25f, 0.75f).SetUpdate(true).Play();
    }
}