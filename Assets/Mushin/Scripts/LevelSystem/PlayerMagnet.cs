using System;
using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Collector _collector;
    private bool _playerCanHeal;

    public bool PlayerCanHeal
    {
        get => _playerCanHeal;
        set
        {
            _playerCanHeal = value;
            _collector.playerCanHeal = value;
        }
    }
    public void Configure(Player player)
    {
        _player = player;
    }
    private void Awake()
    {
        if (!_collector) _collector = GetComponentInChildren<Collector>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Collectable collectable))
        {
            if (collectable is HealCollectable && !_playerCanHeal) return;
            collectable.SetTarget(transform.parent);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Collectable collectable))
        {
            if (collectable is HealCollectable && !_playerCanHeal && collectable.HasTarget)
                collectable.RemoveTarget();
        }
    }
}
