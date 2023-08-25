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
        _invulnerabilityTime = _playerComponents.PlayerLevel._playerStatsData.invulnerabilitySeconds;
        SetHealth(_playerComponents.PlayerLevel._playerStatsData.health);
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
        StartCoroutine(HitAnimation());
    }

    private bool CanTakeDamage()
    {
        return _timer <= 0;
    }

    protected override void Die()
    {
        Debug.Log("Muelto");
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
