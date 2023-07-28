using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLevel : PlayerComponents
{
    public PlayerStats playerStats { get; private set; }
    
   [SerializeField] private PlayerStatsSO _playerStatsData;

    protected override void Awake()
    {
        base.Awake();
        playerStats = _playerStatsData.stats;
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