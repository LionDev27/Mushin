using System;
using Mushin.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    public static Action<int> OnAttackUpgraded;
    [SerializeField] private Vector2 _offset;
    private AttackBase _currentAttack;
    private float _timer;
    private PlayerStats _stats;
    private Vector2 _dir;

    public bool IsPlayerDashing { get; set; }
    public bool CanAttack=>_currentAttack && _timer <= 0 && !IsPlayerDashing;
    public PlayerStats Stats { get => _stats; set => _stats = value; }

    public void Configure(Player player)
    {
        _player = player;
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
        if (!CanAttack)
            RunTimer();
        else
        {
            ChangeAttackDir();
            _currentAttack.UpdateDir(_dir);
        }
    }

    public void Attack()
    {
        if (!CanAttack) return;
        SetTimer();
        
        _currentAttack.Attack(IsCritical());
    }

    private void ChangeAttackDir()
    {
        Vector2 offset = new Vector2(_offset.x * Mathf.Sign(_dir.x), _offset.y * Mathf.Sign(_dir.y));
        if (_dir.x != 0)
            _dir.x += offset.x;
        else
            _dir.y += offset.y;
    }

    private bool IsCritical()
    {
        float randomValue = Random.Range(0f, 100f);
        return randomValue <= _stats.criticalChancePercentage;
    }

    private void RunTimer()
    {
        _timer -= Time.deltaTime;
    }

    private void SetTimer()
    {
        _timer = _stats.AttackCooldown();
    }

    private void InstantiateAttack()
    {
        var attackPrefab = _stats.attackPrefab;
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
        _currentAttack.Setup(_stats, pierce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector2.one + _offset);
    }
}