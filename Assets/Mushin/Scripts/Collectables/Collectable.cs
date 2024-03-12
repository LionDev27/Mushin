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
    private float _timer;

    public void Configure(Player player)
    {
        this.player = player;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_hasTarget) return;
        _timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!_hasTarget) return;
        Vector2 targetDir = _target.position - transform.position;
        targetDir.Normalize();
        _rb.velocity = targetDir * (_moveSpeed * Time.fixedDeltaTime * 100 * _timer);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _timer = 1f;
        _hasTarget = true;
    }

    public void RemoveTarget()
    {
        if (!_hasTarget) return;
        _timer = 0f;
        _hasTarget = false;
        _target = null;
        _rb.velocity = Vector2.zero;
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
