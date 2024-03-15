using System.Collections;
using Mushin.Scripts.Commands;
using Mushin.Scripts.Events;
using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    public bool IsInvulnerable{ get => _isInvulnerable; set => _isInvulnerable = value; }
    
    private Player _player;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private float _extraInvulnerabilityTime;
    [SerializeField] private Collider2D _triggerCollider;
    private float _invulnerabilityTime;
    private float _timer;
    private bool _isInvulnerable;

    public void Configure(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        //Reset();
    }

    public override void SetHealth(float amount)
    {
        base.SetHealth(amount);
        HeartsContainer.OnSetHearts((int)amount);
    }

    private void Update()
    {
        if (CanTakeDamage()) return;
        _timer -= Time.deltaTime;
        if (CanTakeDamage())
            EndInvulnerability();
    }

    private void EndInvulnerability()
    {
        _triggerCollider.enabled = true;
    }

    public override void TakeDamage(float damage)
    {
        if (!CanTakeDamage()||_isInvulnerable) return;
        _player.OnTakeDamage(damage);
        _triggerCollider.enabled = false;
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
        if (_currentHealth >= _maxHealth)
        {
            _player.OnMaxHealth();
        }
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
        _isInvulnerable = true;
        yield return new WaitForSeconds(seconds);
        _isInvulnerable = false;
    }
    protected override void Die()
    {
        base.Die();
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new PlayerKilledEventData(transform.position));
    }

    private IEnumerator HitAnimation()
    {
        new CameraShakeCommand(4).Execute();
        while (!InvulnerabilityEnded())
        {
            _sprite.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _sprite.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Reset()
    {
        _isInvulnerable = false;
        _invulnerabilityTime = _player.CurrentStats.invulnerabilitySeconds;
        StopCoroutine(HitAnimation());
        _sprite.SetActive(true);
        SetHealth(_player.CurrentStats.health);
        
    }
}
