using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Upgrade : MonoBehaviour
{
    [HideInInspector]
    public UpgradeData data;

    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _title, _description;

    private void Awake()
    {
        UpgradeData[] allData = Resources.LoadAll<UpgradeData>("Upgrades");
        int randomDataIndex = Random.Range(0, allData.Length);
        data = allData[randomDataIndex];
    }

    private void Start()
    {
        _sprite.sprite = data.sprite;
        _title.text = data.title;
        _description.text = data.description;
    }

    public void UpgradeStat()
    {
        PlayerUpgrades.OnUpgrade?.Invoke(data);
        PlayerUI.Instance.ShowUpgradeUI(false);
    }
}
