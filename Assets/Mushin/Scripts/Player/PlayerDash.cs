using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : PlayerComponents
{
    [SerializeField] private TextMeshProUGUI _dashesCountText;
    [SerializeField] private Image _dashCooldownImage;
    [SerializeField] private float _dashingTime;
    [SerializeField] private float _dashForce;
    private float _dashTimer;
    private float _dashRecoveryTimer;
    private int _currentDashAmount;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputController.OnDashPerformed += Dash;
    }

    private void Start()
    {
        _currentDashAmount = PlayerLevel.playerStats.dashAmount;
    }

    private void Update()
    {
        if (_dashTimer <= 0)
        {
            PlayerMovement.EnableMovement(true);
            //PlayerAttack.canAttack = true;
        }
        else
            _dashTimer -= Time.deltaTime;

        if (_currentDashAmount < PlayerLevel.playerStats.dashAmount)
        {
            _dashRecoveryTimer += Time.deltaTime;
            _dashCooldownImage.fillAmount = _dashRecoveryTimer / PlayerLevel.playerStats.dashCooldown;
            if (_dashRecoveryTimer > PlayerLevel.playerStats.dashCooldown)
            {
                _currentDashAmount++;
                if (_currentDashAmount < PlayerLevel.playerStats.dashAmount)
                    ResetRecoveryTimer();
            }
        }
        else
            _dashCooldownImage.fillAmount = 0f;

        _dashesCountText.text = $"Dashes: {_currentDashAmount}";
    }
    
    public bool IsDashing()
    {
        return _dashTimer > 0;
    }
    
    private void Dash()
    {
        if (IsDashing() || _currentDashAmount <= 0) return;
        PlayerMovement.EnableMovement(false);
        //PlayerAttack.canAttack = false;
        _currentDashAmount--;
            
        ResetCooldown();
        ResetRecoveryTimer();
        Vector2 moveDir = PlayerInputController.MoveDirection;
        //Si no se está moviendo, hará el dash a la dirección a la que apunta. Si se mueve, lo hará hacia la que se mueve.
        Vector2 dashDir = new Vector2();
        float extraForce = 1f;
        if (moveDir != Vector2.zero)
            dashDir = moveDir;
        else
        {
            dashDir = PlayerInputController.AimDir;
            extraForce = 1.5f;
        }
        Rigidbody.AddForce(dashDir * _dashForce * 100f * extraForce);
    }

    private void ResetCooldown()
    {
        _dashTimer = _dashingTime;
    }

    private void ResetRecoveryTimer()
    {
        if (_dashRecoveryTimer > PlayerLevel.playerStats.dashCooldown)
            _dashRecoveryTimer = 0;
    }
}
