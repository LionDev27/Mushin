using System;
using System.Collections;
using Mushin.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLevel : MonoBehaviour
{
    private Player _player;

    [HideInInspector] [Tooltip("Experiencia que necesitará para el primer nivel.")] [SerializeField]
    private int _startingXPNeeded;

    [Tooltip("Cuanta experiencia necesaria para subir de nivel se añade.")] [SerializeField]
    private int _xpAdditivePerLevel;

    [SerializeField] private float _repelMaxDistance;
    [SerializeField] private float _levelUpWait;
    [SerializeField] private ParticleSystem _burstParticles;
    [SerializeField] private ParticleSystem _raysParticles;

    private int _currentXP;
    private int _currentXPNeeded;
    private int _currentLevel;

    public void Configure(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _currentXP = 0;
        _currentXPNeeded = _startingXPNeeded;
        _currentLevel = 1;
    }

    public void AddXp(int xp)
    {
        _currentXP += xp;
        if (_currentXP >= _currentXPNeeded)
            StartCoroutine(LevelUp());

        _player.OnXpUpdated?.Invoke(_currentXP, _currentXPNeeded);
    }

    [ContextMenu("Level Up")]
    private void DebugLevelUp()
    {
        StartCoroutine(LevelUp());
    }

    private IEnumerator LevelUp()
    {
        _currentLevel++;
        _currentXP = 0;
        _currentXPNeeded += _xpAdditivePerLevel;
        _player.OnStartLevelingUp(false);
        RepelEnemies();
        var particlesMain = _burstParticles.main;
        particlesMain.startSpeed = _repelMaxDistance;
        _burstParticles.Play();
        yield return new WaitForSeconds(_levelUpWait / 2f);
        _raysParticles.Play();
        yield return new WaitForSeconds(_levelUpWait / 2f);
        _player.OnLevelUp?.Invoke(_currentLevel);
    }

    private void RepelEnemies()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, _repelMaxDistance, LayerMask.GetMask("Enemy"));
        if (enemies.Length <= 0) return;
        foreach (var enemy in enemies)
        {
            var enemyPos = enemy.transform.position;
            Vector2 dir = enemyPos - transform.position;
            dir.Normalize();
            var finalPos = new Vector2(transform.position.x + (dir.x * _repelMaxDistance), transform.position.y + (dir.y * _repelMaxDistance));
            enemy.GetComponent<EnemyDamageable>().Repel(_levelUpWait, finalPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _repelMaxDistance);
    }
}