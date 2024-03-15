using Mushin.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    [HideInInspector] public bool canMove = true;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private float _moveSpeed;

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public Vector2 MoveDir
    {
        get => _moveDir;
        set => _moveDir = value;
    }

    public void Configure(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    { 
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        _rb.velocity = _moveSpeed * 10f * Time.fixedDeltaTime * _moveDir;
    }

    public void EnableMovement(bool value)
    {
        canMove = value;
    }

    public void Reset()
    {
        EnableMovement(true);
    }
}