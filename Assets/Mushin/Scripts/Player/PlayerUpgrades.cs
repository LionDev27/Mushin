﻿using System;
using Mushin.Scripts.Player;
using UnityEngine;
public enum Upgrades
{
    Health,
    MoveSpeed,
    DashAmount,
    DashCooldown,
    AttackDamage,
    AttackRange,
    AttackReach,
    AttackSpeed,
    AttackPierce,
    XpAbsortionRange,
    EnemyHealthProbability
}
public class PlayerUpgrades : MonoBehaviour
{
    private Player _player;

    public static Action<UpgradeData> OnUpgrade;
    public void Configure(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        OnUpgrade += UpgradeStat;
    }

    private void OnDisable()
    {
        OnUpgrade -= UpgradeStat;
    }
    private void UpgradeStat(UpgradeData data)
    {
        var playerCurrentStats = _player.CurrentStats;
        switch (data.upgrade)
        {
            case Upgrades.Health:
                _player.OnShouldAddLife();
                break;
            case Upgrades.MoveSpeed:
                playerCurrentStats.moveSpeed += data.value;
                break;
            case Upgrades.DashAmount:
                playerCurrentStats.dashAmount += (int)data.value;
                _player.OnDashUpgrade();
                break;
            case Upgrades.DashCooldown:
                playerCurrentStats.dashCooldown += data.value;
                break;
            case Upgrades.AttackDamage:
                playerCurrentStats.attackDamage += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackRange:
                playerCurrentStats.attackRange += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackReach:
                playerCurrentStats.attackReach += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackPierce:
                PlayerAttack.OnAttackUpgraded?.Invoke((int)data.value);
                break;
            case Upgrades.AttackSpeed:
                playerCurrentStats.attackSpeed += data.value;
                break;
        }
        _player.CurrentStats = playerCurrentStats;
        _player.OnStatsUpdated();
        _player.OnUpgradeApplied?.Invoke(data);
    }


    public void Reset()
    {
        
    }
}