using System.Collections;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [SerializeField] private GameObject _sprite;
    [SerializeField] private float _extraInvulnerabilityTime;
    private PlayerComponents _playerComponents;
    private float _invulnerabilityTime;
    private float _timer;
    private bool _isInvulnarable;
    
    private void Awake()
    {
        _playerComponents = GetComponent<PlayerComponents>();
    }

    private void Start()
    {
        _invulnerabilityTime = _playerComponents.PlayerLevel.Stats.invulnerabilitySeconds;
        SetHealth(_playerComponents.PlayerLevel.Stats.health);
        for (int i = 0; i < _currentHealth; i++)
            HeartsContainer.OnAddHeart?.Invoke();
    }

    private void Update()
    {
        if (!CanTakeDamage())
            _timer -= Time.deltaTime;
    }

    public override void TakeDamage(float damage)
    {
        if (!CanTakeDamage()||_isInvulnarable) return;
        _timer = _invulnerabilityTime + _extraInvulnerabilityTime;
        base.TakeDamage(damage);
        HeartsContainer.OnEnableHeart?.Invoke(false);
        StartCoroutine(HitAnimation());
    }

    public void AddLife()
    {
        _maxHealth++;
        _currentHealth++;
        HeartsContainer.OnAddHeart?.Invoke();
    }

    public override void Heal(float amount)
    {
        base.Heal(amount);
        HeartsContainer.OnEnableHeart?.Invoke(true);
    }

    private bool CanTakeDamage()
    {
        return _timer <= 0;
    }

    private bool InvulnerabilityEnded()
    {
        return _timer <= _extraInvulnerabilityTime;
    }
    public void MakePlayerInvulnerableForSeconds(float seconds)
    {
        StartCoroutine(InvulnerableCoroutine(seconds));
    }
    private IEnumerator InvulnerableCoroutine(float seconds)
    {
        _isInvulnarable = true;
        yield return new WaitForSeconds(seconds);
        _isInvulnarable = false;
    }
    protected override void Die()
    {
        base.Die();
        GameController.OnPlayerDead?.Invoke();
    }

    private IEnumerator HitAnimation()
    {
        while (!InvulnerabilityEnded())
        {
            _sprite.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            _sprite.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
