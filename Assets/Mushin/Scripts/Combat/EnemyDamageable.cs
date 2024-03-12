using System.Collections;
using DG.Tweening;
using Mushin.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageable : Damageable, IPoolable
{
    [SerializeField] private Canvas _healthCanvas;
    [SerializeField] private Image _healthImage;
    [SerializeField] private bool _applyKnockback = true;
    [SerializeField] private float _knockbackStrength;
    [SerializeField] private SpriteRenderer _sprite;
    
    [Header("Flash")] [SerializeField] private float _flashTime;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");
    
    private MaterialPropertyBlock materialPropertyBlock;
    private EnemyAgent _agent;
    private Rigidbody2D _rigidbody2D;
    private string _poolTag;

    private void Awake()
    {
        _agent = GetComponent<EnemyAgent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        materialPropertyBlock= new MaterialPropertyBlock();
        materialPropertyBlock.SetTexture(MainTex,_sprite.sprite.texture);
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
            ApplyKnockback(_knockbackStrength);
        StartCoroutine(HitAnimation());
        base.TakeDamage(damage);
        if (!_healthCanvas.gameObject.activeInHierarchy)
            _healthCanvas.gameObject.SetActive(true);
        _healthImage.fillAmount = _currentHealth / _maxHealth;
    }

    protected override void Die()
    {
        base.Die();
        materialPropertyBlock.SetFloat(FlashAmount, 0);
        _sprite.SetPropertyBlock(materialPropertyBlock);
        _agent.EnableNavigation(false);
        SpawnController.Instance.enemiesKilled++;//TODO: Lanzar un evento
        SpawnXp();
        SpawnLife();
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }

    private void ApplyKnockback(float strength)
    {
        var dir = -_agent.TargetDir();
        EnableRigidbody(true);
        _rigidbody2D.AddForce(dir * strength, ForceMode2D.Impulse);
        StartCoroutine(KnockbackTimer());
    }

    public void Repel(float time, Vector2 finalPos)
    {
        EnableRigidbody(true);
        transform.DOMove(finalPos, time).SetEase(Ease.OutSine).SetUpdate(true).Play().OnComplete(() => EnableRigidbody(false));
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

    private void EnableRigidbody(bool value)
    {
        if (value)
            _rigidbody2D.WakeUp();
        else
            _rigidbody2D.Sleep();
        _agent.EnableNavigation(!value);
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
        EnableRigidbody(false);
    }

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    private IEnumerator HitAnimation()
    {
        float elapsedTime = 0;
        while (elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;
            var currentFlashAmount = Mathf.Lerp(1, 0, (elapsedTime / _flashTime));
            materialPropertyBlock.SetFloat(FlashAmount, currentFlashAmount);
            _sprite.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }
        
    }
}