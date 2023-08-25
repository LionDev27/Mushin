using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLevel : PlayerComponents
{
    public PlayerStats playerStats { get; private set; }
    private float _currentXP;
    
   [SerializeField] private PlayerStatsSO _playerStatsData;

    protected override void Awake()
    {
        base.Awake();
        playerStats = _playerStatsData.stats;
    }

    private void AddXP(float xp)
    {
        _currentXP += xp;
        Debug.Log(_currentXP);
    }

    private void OnEnable()
    {
        XpOrb.OnXpOrbCollected += AddXP;
    }

    private void OnDisable()
    {
        XpOrb.OnXpOrbCollected -= AddXP;
    }
}

public enum Stats
{
    Health,
    MoveSpeed,
    DashCooldown,
    AttackDamage,
    AttackRange,
    AttackCooldown,
}