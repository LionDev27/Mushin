using System.Collections;
using UnityEngine;

public class SamuraiAttack : AttackBase
{
    [SerializeField] private float _range;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    public override void Attack()
    {
        //Si el collider está activado quiere decir que la animacion no ha terminado.
        if (_collider.enabled)
        {
            StopCoroutine(AttackAnimation());
            _collider.enabled = false;
            transform.localPosition = Vector2.zero;
        }
        //TODO: Comprobar si, es con mando, usar dirección de movimiento si es distinta de cero.
        Vector2 newPos = transform.localPosition;
        newPos += _playerInput.AimUnitaryDir * _range * 1.2f;
        transform.localPosition = newPos;
        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = false;
        transform.localPosition = Vector2.zero;
    }
}