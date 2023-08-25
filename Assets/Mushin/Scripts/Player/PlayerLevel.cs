using UnityEngine;

public class PlayerLevel : PlayerComponents
{
    public PlayerStatsSO _playerStatsData;
    
    [Tooltip("Experiencia que necesitará para el primer nivel.")]
    [SerializeField] private int _startingXPNeeded;
    [Tooltip("Cuanta experiencia necesaria para subir de nivel se añade.")]
    [SerializeField] private int _xpAdditivePerLevel;
    
    private PlayerUI _playerUI;
    private float _currentXP;
    private float _currentXPNeeded;
    private int _currentLevel;

    private void Start()
    {
        _playerUI = PlayerUI.Instance;
        _currentXP = 0;
        _currentXPNeeded = _startingXPNeeded;
        _currentLevel = 1;
    }

    private void AddXp(int xp)
    {
        _currentXP += xp;
        if (_currentXP >= _currentXPNeeded)
            LevelUp();
        _playerUI.UpdateLevelUI(_currentLevel, _currentXP, _currentXPNeeded);
    }

    private void LevelUp()
    {
        _currentLevel++;
        _currentXP = 0;
        _currentXPNeeded += _xpAdditivePerLevel;
        _playerUI.UpgradeStat();
    }

    private void OnEnable()
    {
        XpOrb.OnXpOrbCollected += AddXp;
    }

    private void OnDisable()
    {
        XpOrb.OnXpOrbCollected -= AddXp;
    }
}

public enum Stats
{
    Health,
    MoveSpeed,
    DashAmount,
    DashCooldown,
    AttackDamage,
    AttackRange,
    AttackReach,
    AttackSpeed,
}