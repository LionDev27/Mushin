using Mushin.Scripts.Player;
using UnityEngine;

public class Collectable : MonoBehaviour, IPoolable
{
    public bool HasTarget => _hasTarget;
    
    [SerializeField] protected float _moveSpeed = 7f;
    
    protected Player player;

    private Rigidbody2D _rb;
    private Transform _target;
    private bool _hasTarget;
    private string _poolTag;

    public void Configure(Player player)
    {
        this.player = player;
    }
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
            _rb.velocity = targetDir * (_moveSpeed * Time.fixedDeltaTime * 100);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _hasTarget = true;
    }

    public void RemoveTarget()
    {
        if (!_hasTarget) return;
        _hasTarget = false;
        _target = null;
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    public virtual void Collect()
    {
        RemoveTarget();
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }
}