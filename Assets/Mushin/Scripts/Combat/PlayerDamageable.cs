using System.Collections;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [SerializeField] private GameObject _sprite;
    private PlayerComponents _playerComponents;
    private float _invulnerabilityTime;
    private float _timer;

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
        if (!CanTakeDamage()) return;
        base.TakeDamage(damage);
        _timer = _invulnerabilityTime;
        HeartsContainer.OnEnableHeart?.Invoke(false);
        StartCoroutine(HitAnimation());
    }

    private bool CanTakeDamage()
    {
        return _timer <= 0;
    }

    protected override void Die()
    {
        GameController.OnPlayerDead?.Invoke();
    }

    private IEnumerator HitAnimation()
    {
        while (!CanTakeDamage())
        {
            _sprite.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            _sprite.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
