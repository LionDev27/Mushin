using System.Collections;
using Mushin.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageable : Damageable, IPoolable
{
    [SerializeField] private Canvas _healthCanvas;
    [SerializeField] private Image _healthImage;
    [SerializeField] private bool _applyKnockback = true;
    [SerializeField] private float _knockbackStrength;
    private EnemyAgent _agent;
    private Rigidbody2D _rigidbody2D;
    private string _poolTag;

    private void Awake()
    {
        _agent = GetComponent<EnemyAgent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _healthCanvas.gameObject.SetActive(false);
        _rigidbody2D.Sleep();
    }

    private void OnDisable()
    {
        _healthCanvas.gameObject.SetActive(false);
        _rigidbody2D.Sleep();
    }

    public override void TakeDamage(float damage)
    {
        if (_applyKnockback)
        {
            var dir = -_agent.TargetDir();
            _agent.EnableNavigation(false);
            _rigidbody2D.WakeUp();
            _rigidbody2D.AddForce(dir * _knockbackStrength, ForceMode2D.Impulse);
            StartCoroutine(KnockbackTimer());
        }
        base.TakeDamage(damage);
        if (!_healthCanvas.gameObject.activeInHierarchy)
            _healthCanvas.gameObject.SetActive(true);
        _healthImage.fillAmount = _currentHealth / _maxHealth;
    }

    protected override void Die()
    {
        base.Die();
        _agent.EnableNavigation(false);
        SpawnController.Instance.enemiesKilled++;
        SpawnXp();
        SpawnLife();
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }

    private void SpawnXp()
    {
        var xpAmount = _agent.stats.xpAmount;
        switch (xpAmount)
        {
            case 1:
                ObjectPooler.Instance.SpawnFromPool(_agent.stats.xpOrbTag, RandomNearPosition());
                break;
            case > 1:
            {
                for (int i = 0; i < xpAmount; i++)
                    ObjectPooler.Instance.SpawnFromPool(_agent.stats.xpOrbTag, RandomNearPosition());
                break;
            }
        }
    }

    private void SpawnLife()
    {
        int probability = Random.Range(0, 101);
        if (probability <= _agent.stats.dropHealthProbability)
            ObjectPooler.Instance.SpawnFromPool(_agent.stats.healingTag, RandomNearPosition());
    }

    private Vector2 RandomNearPosition()
    {
        Vector2 currentPos = transform.position;
        Vector2 pos = new Vector2(
            Random.Range(currentPos.x - 0.25f, currentPos.x + 0.25f),
            Random.Range(currentPos.y - 0.25f, currentPos.y + 0.25f)
        );
        return pos;
    }

    private IEnumerator KnockbackTimer()
    {
        yield return new WaitForSeconds(0.1f);
        _rigidbody2D.Sleep();
        _agent.EnableNavigation(true);
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    
}