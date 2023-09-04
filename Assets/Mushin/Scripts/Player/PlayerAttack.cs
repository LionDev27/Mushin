using UnityEngine;

public class PlayerAttack : PlayerComponents
{
    private AttackBase _currentAttack;
    private float _timer;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputController.OnAttackPerformed += Attack;
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
        _currentAttack._currentDamage = PlayerLevel.Stats.attackDamage;
        if (IsCritical())
        {
            _currentAttack._currentDamage *= PlayerLevel.Stats.criticalDamageMultiplier;
            Debug.Log("Critical Hit!");
        }
        _currentAttack.Attack();
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
        _currentAttack._playerInput = PlayerInputController;
    }
}