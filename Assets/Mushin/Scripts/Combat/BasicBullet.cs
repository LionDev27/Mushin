using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    private float _damage;
    private string _tag;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    public void SetBullet(Vector2 dir, float speed, string tagToDamage, float damage)
    {
        _rb.velocity = dir * speed * Time.fixedDeltaTime * 100;
        _tag = tagToDamage;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(_tag))
        {
            col.GetComponent<Damageable>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}