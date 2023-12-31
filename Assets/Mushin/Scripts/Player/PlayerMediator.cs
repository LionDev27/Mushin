using System;
using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerMediator : Player
{
    [SerializeField] private PlayerStatsSO _playerStatsData;
    
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private PlayerDamageable _playerDamageable;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private PlayerUpgrades _playerUpgrades;
    [SerializeField] private PlayerMagnet _playerMagnet;
    [SerializeField] private PlayerVisuals _playerVisuals;

    private void Awake()
    {
        CurrentStats = _playerStatsData.Stats;
        
        _playerInputs.Configure(this);
        _playerMovement.Configure(this);
        _playerAttack.Configure(this);
        _playerDash.Configure(this);
        _playerDamageable.Configure(this);
        _playerLevel.Configure(this);
        _playerUpgrades.Configure(this);
        _playerMagnet.Configure(this);
        _playerVisuals.Configure(this);
        
        OnStatsUpdated();
    }


    public override void OnMoveInput(Vector2 value)
    {
        _playerMovement.MoveDir = value;
        _playerDash.MoveDir = value;
    }

    public override void OnAttackInput(Vector2 dir)
    {
        _playerAttack.Attack(dir);
    }

    public override void OnDashInput()
    {
        _playerDash.TryDash(_playerInputs.AimDir);
    }

    public override void OnStatsUpdated()
    {
        _playerMovement.MoveSpeed = CurrentStats.moveSpeed;
        _playerAttack.Stats = CurrentStats;
        _playerDash.Stats = CurrentStats;
        _playerDamageable.IsInvulnerable = false;
        Time.timeScale = 1;
    }

    public override void OnIsDashing(bool dashing)
    {
        _playerAttack.IsPlayerDashing = dashing;
        _playerMovement.EnableMovement(!dashing);
        if (dashing)
        {
            _playerVisuals.SpawnDashShadows();
            _playerDamageable.MakePlayerInvulnerableForSeconds(CurrentStats.dashingTime);
        }
    }

    public override void OnHealOrbCollected(float amount)
    {
        _playerDamageable.Heal(amount);
    }
    public override void OnXPOrbCollected(int amount)
    {
        _playerLevel.AddXp(amount);
    }
    public override void OnTakeDamage(float damage)
    {
        _playerMagnet.PlayerCanHeal=true;
    }

    public override void OnMaxHealth()
    {
        _playerMagnet.PlayerCanHeal=false;
    }

    public override void OnShouldAddLife()
    {
        _playerDamageable.AddLife();
    }

    public override void OnDashUpgrade()
    {
        _playerDash.ResetDashes();
    }

    public override void OnStartLevelingUp()
    {
        _playerInputs.EnableInputs(false);
        _playerDamageable.IsInvulnerable = true;
        Time.timeScale = 0;
    }
}