using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : PlayerComponents
{
    public static Action OnAttackUpgraded;
    private AttackBase _currentAttack;
    private float _timer;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputController.OnAttackPerformed += Attack;
    }

    private void OnEnable()
    {
        OnAttackUpgraded += UpdateStats;
    }

    private void OnDisable()
    {
        OnAttackUpgraded -= UpdateStats;
    }

    private void Start()
    {
        InstantiateAttack();
    }
    
    private void Update()
    {
        if (!CanAttack())
            RunTimer();
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        SetTimer();
        
        Vector2 dir = PlayerInputController.CurrentInput() == InputType.Gamepad && PlayerInputController.IsMoving() &&
                      !PlayerInputController.IsAiming
            ? PlayerInputController.MoveUnitaryDir()
            : PlayerInputController.AimUnitaryDir;
        
        _currentAttack.Attack(dir, IsCritical());
    }

    private bool IsCritical()
    {
        float randomValue = Random.Range(0f, 100f);
        return randomValue <= PlayerLevel.Stats.criticalChancePercentage;
    }
    
    private bool CanAttack()
    {
        return _currentAttack && _timer <= 0 && !PlayerDash.IsDashing();
    }

    private void RunTimer()
    {
        _timer -= Time.deltaTime;
    }

    private void SetTimer()
    {
        _timer = PlayerLevel.Stats.AttackCooldown();
    }

    private void InstantiateAttack()
    {
        var attackPrefab = PlayerLevel.Stats.attackPrefab;
        if (!attackPrefab)
        {
            Debug.LogWarning("Attack prefab not set on PlayerStats");
            return;
        }

        var currentAttack = Instantiate(attackPrefab, transform);
        _currentAttack = currentAttack.GetComponent<AttackBase>();
        UpdateStats();
    }

    private void UpdateStats()
    {
        PlayerStats stats = PlayerLevel.Stats;
        _currentAttack.Setup(stats.attackDamage, stats.criticalDamageMultiplier, stats.attackRange, stats.attackReach);
    }
}