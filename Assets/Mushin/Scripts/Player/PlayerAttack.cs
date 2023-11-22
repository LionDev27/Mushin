using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : PlayerComponents
{
    public static Action<int> OnAttackUpgraded;
    [SerializeField] private Vector2 _offset;
    private AttackBase _currentAttack;
    private float _timer;
    private Vector2 _dir;

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
        else
        {
            ChangeAttackDir();
            _currentAttack.UpdateDir(_dir);
        }
    }

    public void Attack()
    {
        if (!CanAttack()) return;
        SetTimer();
        _currentAttack.Attack(IsCritical());
    }

    private void ChangeAttackDir()
    {
        _dir = PlayerInputController.CurrentInput() == InputType.Gamepad && PlayerInputController.IsMoving() &&
                      !PlayerInputController.IsAiming
            ? PlayerInputController.MoveUnitaryDir()
            : PlayerInputController.AimUnitaryDir;

        Vector2 offset = new Vector2(_offset.x * Mathf.Sign(_dir.x), _offset.y * Mathf.Sign(_dir.y));
        if (_dir.x != 0)
            _dir.x += offset.x;
        else
            _dir.y += offset.y;
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
        UpdateStats(1);
    }

    private void UpdateStats(int pierce = 0)
    {
        _currentAttack.Setup(PlayerLevel.Stats, pierce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector2.one + _offset);
    }
}