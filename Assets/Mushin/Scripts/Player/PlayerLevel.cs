using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private Player _player;

    [HideInInspector] [Tooltip("Experiencia que necesitará para el primer nivel.")] [SerializeField]
    private int _startingXPNeeded;

    [Tooltip("Cuanta experiencia necesaria para subir de nivel se añade.")] [SerializeField]
    private int _xpAdditivePerLevel;


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
        {
            LevelUp();
        }

        _player.OnXpUpdated?.Invoke(_currentXP, _currentXPNeeded);
    }

    [ContextMenu("Level Up")]
    private void LevelUp()
    {
        _currentLevel++;
        _currentXP = 0;
        _currentXPNeeded += _xpAdditivePerLevel;
        _player.OnLevelUp?.Invoke(_currentLevel);
    }
}