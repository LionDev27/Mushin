using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : PlayerComponents
{
    [HideInInspector] public bool canMove = true;

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        Rigidbody.velocity = PlayerLevel.Stats.moveSpeed * 10f * Time.deltaTime * PlayerInputController.MoveDirection;
    }

    public void EnableMovement(bool value)
    {
        canMove = value;
    }
}