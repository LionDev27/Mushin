using System;
using Mushin.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Image _dashCooldownImage;
    private float _dashTimer;
    private float _dashRecoveryTimer;
    private int _currentDashAmount;
    private Vector2 _moveDir;
    private PlayerStats _stats;
    private Rigidbody2D _rb;

    public PlayerStats Stats { get => _stats; set => _stats = value; }

    public Vector2 MoveDir { get => _moveDir; set => _moveDir = value; }

    public void Configure(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetDashes();
    }

    private void Update()
    {
        if (_dashTimer <= 0)
        {
            _player.OnIsDashing(false);
        }
        else
        {
             _dashTimer -= Time.deltaTime;
        }

        if (_currentDashAmount < _stats.dashAmount)
        {
            _dashRecoveryTimer += Time.deltaTime;
            _dashCooldownImage.fillAmount = _dashRecoveryTimer / _stats.dashCooldown;
            if (_dashRecoveryTimer > _stats.dashCooldown)
            {
                _currentDashAmount++;
                _player.OnDashesUpdated?.Invoke(_currentDashAmount);
                if (_currentDashAmount < _stats.dashAmount)
                    ResetRecoveryTimer();
            }
        }
        else
        {
            _dashCooldownImage.fillAmount = 0f;
        }
    }

    public void ResetDashes()
    {
        _currentDashAmount = _stats.dashAmount;
        _player.OnDashesUpdated?.Invoke(_currentDashAmount);
    }

    public void TryDash(Vector2 defaultDir)
    {
        if (_dashTimer > 0 || _currentDashAmount <= 0) return;
        _player.OnIsDashing(true);
        //Si no se está moviendo, hará el dash a la dirección a la que apunta. Si se mueve, lo hará hacia la que se mueve.
        Vector2 dashDir;
        float extraForce = 1f;
        if (_moveDir != Vector2.zero)
            dashDir = _moveDir;
        else
        {
            dashDir = defaultDir; 
            extraForce = 1.5f;
        }
        //PlayerAttack.canAttack = false;
        _currentDashAmount--;
        _player.OnDashesUpdated?.Invoke(_currentDashAmount);
        ResetCooldown();
        ResetRecoveryTimer();
        
        _rb.AddForce(dashDir * _stats.dashForce * 100f * extraForce);
    }

    private void ResetCooldown()
    {
        _dashTimer = _stats.dashingTime;
    }

    private void ResetRecoveryTimer()
    {
        if (_dashRecoveryTimer > _stats.dashCooldown)
            _dashRecoveryTimer = 0;
    }
}
