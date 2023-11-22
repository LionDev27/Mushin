using System.Collections;
using System.Linq;
using UnityEngine;

public class SamuraiAttack : AttackBase
{
    private SpriteRenderer _renderer;
    private float _halfRange => _range / 2f;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
    }

    public override void Setup(PlayerStats stats, int pierce)
    {
        base.Setup(stats, pierce);
        transform.localScale = new Vector3(_range, _reach, 1f);
    }

    public override void UpdateDir(Vector2 dir)
    {
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * dir;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        transform.rotation = targetRotation;

        if (dir.x != 0)
            dir.x += (_halfRange * Mathf.Sign(dir.x));
        else
            dir.y += (_halfRange * Mathf.Sign(dir.y));
        transform.localPosition = dir;
    }

    public override void Attack(bool isCritical)
    {
        //Si el renderer está activado quiere decir que la animacion no ha terminado.
        if (_renderer.enabled)
        {
            StopCoroutine(AttackAnimation());
            _renderer.enabled = false;
            transform.localPosition = Vector2.zero;
        }

        StartCoroutine(AttackAnimation());
        var enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(_range, _reach), 0,
            LayerMask.GetMask("Enemy"));
        if (enemies.Length <= 0) return;
        var orderedEnemies = GetOrderedEnemies(enemies);
        for (int i = 0; i < _pierce; i++)
        {
            Damage(orderedEnemies[i].GetComponent<EnemyDamageable>(), isCritical);
            if (orderedEnemies.Length == i + 1)
                break;
        }
    }

    private Collider2D[] GetOrderedEnemies(Collider2D[] currentEnemies)
    {
        return currentEnemies.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();
    }

    private IEnumerator AttackAnimation()
    {
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _renderer.enabled = false;
        transform.localPosition = Vector2.zero;
    }
}