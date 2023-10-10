using System.Collections;
using UnityEngine;

public class SamuraiAttack : AttackBase
{
    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    public override void Setup(PlayerStats stats, int penetration)
    {
        base.Setup(stats, penetration);
        transform.localScale = new Vector3(_range, _reach, 1f);
    }

    public override void Attack(Vector2 dir, bool isCritical)
    {
        //Si el collider está activado quiere decir que la animacion no ha terminado.
        if (_collider.enabled)
        {
            StopCoroutine(AttackAnimation());
            _collider.enabled = false;
            transform.localPosition = Vector2.zero;
        }

        Vector2 newPos = transform.localPosition;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * dir;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        transform.rotation = targetRotation;

        newPos += dir * _range;
        transform.localPosition = newPos;

        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = false;
        _renderer.enabled = false;
        transform.localPosition = Vector2.zero;
    }
}