using System;

public class PlayerUpgrades : PlayerComponents
{
    public static Action<UpgradeData> OnUpgrade;

    private void UpgradeStat(UpgradeData data)
    {
        switch (data.stat)
        {
            case Stats.Health:
                PlayerLevel._playerStatsData.health += (int)data.value;
                break;
            case Stats.MoveSpeed:
                PlayerLevel._playerStatsData.moveSpeed += data.value;
                break;
            case Stats.DashAmount:
                PlayerLevel._playerStatsData.dashAmount += (int)data.value;
                PlayerDash.ResetDashes();
                break;
            case Stats.DashCooldown:
                PlayerLevel._playerStatsData.dashCooldown += data.value;
                break;
            case Stats.AttackDamage:
                PlayerLevel._playerStatsData.attackDamage += data.value;
                break;
            case Stats.AttackRange:
                break;
            case Stats.AttackReach:
                break;
            case Stats.AttackSpeed:
                PlayerLevel._playerStatsData.attackSpeed += data.value;
                break;
        }
    }

    private void OnEnable()
    {
        OnUpgrade += UpgradeStat;
    }

    private void OnDisable()
    {
        OnUpgrade -= UpgradeStat;
    }
}