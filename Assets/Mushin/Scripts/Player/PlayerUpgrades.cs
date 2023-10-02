using System;

public class PlayerUpgrades : PlayerComponents
{
    public static Action<UpgradeData> OnUpgrade;

    private void UpgradeStat(UpgradeData data)
    {
        switch (data.stat)
        {
            case Stats.Health:
                PlayerLevel.Stats.health += (int)data.value;
                HeartsContainer.OnAddHeart?.Invoke();
                break;
            case Stats.MoveSpeed:
                PlayerLevel.Stats.moveSpeed += data.value;
                break;
            case Stats.DashAmount:
                PlayerLevel.Stats.dashAmount += (int)data.value;
                PlayerDash.ResetDashes();
                break;
            case Stats.DashCooldown:
                PlayerLevel.Stats.dashCooldown += data.value;
                break;
            case Stats.AttackDamage:
                PlayerLevel.Stats.attackDamage += data.value;
                PlayerAttack.OnAttackUpgraded?.Invoke();
                break;
            case Stats.AttackRange:
                PlayerAttack.OnAttackUpgraded?.Invoke();
                break;
            case Stats.AttackReach:
                PlayerAttack.OnAttackUpgraded?.Invoke();
                break;
            case Stats.AttackSpeed:
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