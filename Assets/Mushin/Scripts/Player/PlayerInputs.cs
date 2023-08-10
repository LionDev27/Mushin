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
        Vector2 aimDirection =  cursorPos- transform.position;
        aimDirection.Normalize();
        AimDir = aimDirection;

        Vector2 aimUnitaryDirection = Mathf.Abs(aimDirection.x) > Mathf.Abs(aimDirection.y) ?
            new Vector2(Mathf.Sign(aimDirection.x), 0) :
            new Vector2(0, Mathf.Sign(aimDirection.y));
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
        if (CurrentInput() != InputType.Gamepad || value.Get<Vector2>() == Vector2.zero) return;
        Vector2 unitaryDir = value.Get<Vector2>();
        AimUnitaryDir = Mathf.Abs(unitaryDir.x) > Mathf.Abs(unitaryDir.y) ?
            new Vector2(Mathf.Sign(unitaryDir.x), 0) :
            new Vector2(0, Mathf.Sign(unitaryDir.y));
    }

    private void OnAttack()
    {
        OnAttackPerformed();
    }

    public InputType CurrentInput()
    {
        return _playerInput.currentControlScheme == "Gamepad" ? InputType.Gamepad : InputType.Keyboard;
    }
}

public enum InputType
{
    Keyboard,
    Gamepad
}