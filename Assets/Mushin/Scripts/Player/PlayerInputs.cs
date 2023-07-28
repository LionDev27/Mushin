using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : PlayerComponents
{
    public Vector2 MoveDirection { get; private set; }
    public Vector2 AimDir { get; private set; }
    public Vector2 AimUnitaryDir { get; private set; }
    public Action OnDashPerformed;

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

        Vector2 aimUnitaryDirection = new Vector2();
        aimUnitaryDirection.x = Mathf.Sign(aimDirection.x);
        aimUnitaryDirection.y = Mathf.Sign(aimDirection.y);
        aimUnitaryDirection.Normalize();
        AimUnitaryDir = aimUnitaryDirection;
    }
    
    private void OnAimDirection(InputValue value)
    {
        if (CurrentInput() != InputType.Gamepad) return;
        AimDir = value.Get<Vector2>();
    }

    private void OnAimUnitaryDirection(InputValue value)
    {
        if (CurrentInput() != InputType.Gamepad) return;
        AimUnitaryDir = value.Get<Vector2>();
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