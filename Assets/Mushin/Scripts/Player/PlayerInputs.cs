using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : PlayerComponents
{
    public Vector2 MoveDirection { get; private set; }
    public Vector2 AimDir { get; private set; }
    public Vector2 AimUnitaryDir { get; private set; }
    public Action OnDashPerformed;
    public Action OnAttackPerformed;
    public bool IsAiming { get; private set; }

    private PlayerInput _playerInput;
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }

    private void OnDash(InputValue value)
    {
        OnDashPerformed();
    }

    private void OnAimPosition(InputValue value)
    {
        if (CurrentInput() != InputType.Keyboard) return;
        var cursorPos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        Vector2 aimDirection = cursorPos - transform.position;
        aimDirection.Normalize();
        AimDir = aimDirection;

        Vector2 aimUnitaryDirection = Mathf.Abs(aimDirection.x) > Mathf.Abs(aimDirection.y)
            ? new Vector2(Mathf.Sign(aimDirection.x), 0)
            : new Vector2(0, Mathf.Sign(aimDirection.y));
        aimUnitaryDirection.Normalize();
        AimUnitaryDir = aimUnitaryDirection;
    }

    private void OnAimDirection(InputValue value)
    {
        if (CurrentInput() != InputType.Gamepad || value.Get<Vector2>() == Vector2.zero) return;
        AimDir = value.Get<Vector2>();
    }

    private void OnAimUnitaryDirection(InputValue value)
    {
        if (CurrentInput() != InputType.Gamepad) return;
        if (value.Get<Vector2>() == Vector2.zero)
        {
            IsAiming = false;
            return;
        }
        IsAiming = true;
        Vector2 unitaryDir = value.Get<Vector2>();
        AimUnitaryDir = Mathf.Abs(unitaryDir.x) > Mathf.Abs(unitaryDir.y)
            ? new Vector2(Mathf.Sign(unitaryDir.x), 0)
            : new Vector2(0, Mathf.Sign(unitaryDir.y));
    }

    private void OnAttack()
    {
        OnAttackPerformed();
    }

    public void EnableInputs(bool value)
    {
        _playerInput.enabled = value;
    }

    public Vector2 MoveUnitaryDir()
    {
        return Mathf.Abs(MoveDirection.x) > Mathf.Abs(MoveDirection.y)
            ? new Vector2(Mathf.Sign(MoveDirection.x), 0)
            : new Vector2(0, Mathf.Sign(MoveDirection.y));
    }

    public InputType CurrentInput()
    {
        return _playerInput.currentControlScheme == "Gamepad" ? InputType.Gamepad : InputType.Keyboard;
    }

    public bool IsMoving()
    {
        return MoveDirection != Vector2.zero;
    }
}

public enum InputType
{
    Keyboard,
    Gamepad
}