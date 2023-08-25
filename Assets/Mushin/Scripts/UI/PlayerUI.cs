using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Dashes")]
    [SerializeField] private TextMeshProUGUI _dashesCountText;

    [Header("XP")]
    [SerializeField] private Image _xpUIVisuals;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _xpText;

    [Header("Upgrades")]
    [SerializeField] private float _upgradesToShow;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _upgradeContainer;
    [SerializeField] private GameObject _upgradeButtonPrefab;
    
    public static PlayerUI Instance;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void UpdateDashUI(int dashAmount)
    {
        _dashesCountText.text = $"Dashes: {dashAmount}";
    }

    public void UpdateLevelUI(int level, float xp, float xpNeeded)
    {
        _xpUIVisuals.fillAmount = xp / xpNeeded;
        _xpText.text = $"{xp} / {xpNeeded}";
        _levelText.text = $"Level: {level}";
    }

    public void UpgradeStat()
    {
        ShowUpgradeUI(true);
        for (int i = 0; i < _upgradesToShow; i++)
        {
            Instantiate(_upgradeButtonPrefab, _upgradeContainer.transform);
        }
    }
    
    public void ShowUpgradeUI(bool value)
    {
        _upgradeMenu.SetActive(value);
        Time.timeScale = value ? 0 : 1;
    }
}
