using UnityEngine;

public class Recollectable : MonoBehaviour, IPoolable
{
    [SerializeField] protected float _moveSpeed = 7f;

    private Rigidbody2D _rb;
    private Transform _target;
    private bool _hasTarget;
    private string _poolTag;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_hasTarget)
        {
            Vector2 targetDir = _target.position - transform.position;
            targetDir.Normalize();
            _rb.velocity = targetDir * _moveSpeed * Time.fixedDeltaTime * 100;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _hasTarget = true;
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    public virtual void Collect()
    {
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }
}