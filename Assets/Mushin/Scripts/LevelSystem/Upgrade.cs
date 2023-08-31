using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Upgrade : MonoBehaviour
{
    private UpgradeData _data;

    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _title, _description;

    private void Start()
    {
        _sprite.sprite = _data.sprite;
        _title.text = _data.title;
        _description.text = _data.description;
    }

    public void SetData(UpgradeData data)
    {
        _data = data;
    }

    public UpgradeData GetData()
    {
        return _data;
    }

    public void UpgradeStat()
    {
        PlayerUpgrades.OnUpgrade?.Invoke(_data);
        PlayerUI.Instance.UpgradeComplete();
    }
}