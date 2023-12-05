using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerMediator _player;
    
    [Header("Dashes")] 
    [SerializeField] private RectTransform DashIconsParent;
    [SerializeField] private CanvasGroup dashImagePrefab;
    private List<CanvasGroup> dashIcons;
    
    [Header("XP")] 
    [SerializeField] private Image _xpBar;
    [SerializeField] private TextMeshProUGUI _xpText;
    [SerializeField] private TextMeshProUGUI _levelText;

    [Header("Upgrades")]
    [SerializeField] private float _upgradesToShow;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _upgradeContainer;
    [SerializeField] private GameObject _upgradeButtonPrefab;

    private List<GameObject> _upgradeButtons = new();

    private void Awake()
    {
        dashIcons = new();
        _player.OnDashesUpdated += UpdateDashUI;
        _player.OnXpUpdated += UpdateXpUI;
        _player.OnLevelUp += UpdateLevelUI;
        _player.OnUpgradeApplied += UpgradeComplete;
    }

    private void OnDestroy()
    {
        _player.OnDashesUpdated -= UpdateDashUI;
        _player.OnXpUpdated -= UpdateXpUI;
        _player.OnLevelUp -= UpdateLevelUI;
        _player.OnUpgradeApplied -= UpgradeComplete;
    }

    private void Start()
    {
        ConfigureDashCanvas();
    }

    private void ConfigureDashCanvas()
    {
        dashIcons.Clear();
        DashIconsParent.transform.DestroyAllChildren();
        
        int numDashes = _player.CurrentStats.dashAmount;
        for (int i = 0; i < numDashes; i++)
        {
            var dashIcon=Instantiate(dashImagePrefab, DashIconsParent);
            dashIcons.Add(dashIcon);
        }
    }

    private void UpdateDashUI(int dashAmount)
    {
        for (int i = 0; i < dashIcons.Count; i++)
        {
            dashIcons[i].alpha = i<dashAmount?1:0;
        }
    }

    private void UpdateXpUI( int xp, int xpNeeded)
    {
        //Debug.Log(xp+ "/" +xpNeeded);
        _xpBar.fillAmount = (float)xp / xpNeeded;
        _xpText.text = $"{xp} / {xpNeeded}";
    }

    private void UpdateLevelUI(int level)
    {
        _levelText.text = $"Level: {level}";
        UpgradeStat();
    }
    private void UpgradeStat()
    {
        ShowUpgradeUI(true);
       var allData = Resources.LoadAll<UpgradeData>("Upgrades");
        List<UpgradeData> currentDataList = new();

        for (int i = 0; i < _upgradesToShow; i++)
        {
            UpgradeData currentData;
            do
                currentData = allData[Random.Range(0, allData.Length)];
            while (currentDataList.Contains(currentData));
            
            GameObject upgradeButton = Instantiate(_upgradeButtonPrefab, _upgradeContainer.transform);
            _upgradeButtons.Add(upgradeButton);

            upgradeButton.GetComponent<UpgradeCard>().SetData(currentData);
            currentDataList.Add(currentData);
        }
    }

    private void UpgradeComplete(UpgradeData data)
    {
        if (data.upgrade == Upgrades.DashAmount)
        {
            ConfigureDashCanvas();
        }
        ShowUpgradeUI(false);
        foreach (var upgradeButton in _upgradeButtons)
        {
            Destroy(upgradeButton);
        }

        _upgradeButtons.Clear();
    }

    private void ShowUpgradeUI(bool value)
    {
        _upgradeMenu.SetActive(value);
        PlayerComponents.Instance.PlayerInputController.EnableInputs(!value);
        Time.timeScale = value ? 0 : 1;
    }
}