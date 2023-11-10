using System;

public class PlayerUpgrades : PlayerComponents
{
    public static Action<UpgradeData> OnUpgrade;

    private void UpgradeStat(UpgradeData data)
    {
        switch (data.upgrade)
        {
            case Upgrades.Health:
                PlayerDamageable.AddLife();
                break;
            case Upgrades.MoveSpeed:
                PlayerLevel.Stats.moveSpeed += data.value;
                break;
            case Upgrades.DashAmount:
                PlayerLevel.Stats.dashAmount += (int)data.value;
                PlayerDash.ResetDashes();
                break;
            case Upgrades.DashCooldown:
                PlayerLevel.Stats.dashCooldown += data.value;
                break;
            case Upgrades.AttackDamage:
                PlayerLevel.Stats.attackDamage += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackRange:
                PlayerLevel.Stats.attackRange += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackReach:
                PlayerLevel.Stats.attackReach += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke(0);
                break;
            case Upgrades.AttackPierce:
                PlayerAttack.OnAttackUpgraded?.Invoke((int)data.value);
                break;
            case Upgrades.AttackSpeed:
                PlayerLevel.Stats.attackSpeed += data.value;
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