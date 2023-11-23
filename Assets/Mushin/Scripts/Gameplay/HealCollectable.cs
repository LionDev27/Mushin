using Mushin.Scripts.Player;
using UnityEngine;

public class HealCollectable : Collectable
{
    [SerializeField] private int _healValue;
    
    public override void Collect()
    {
        base.Collect();
        player.OnHealOrbCollected(_healValue);
    }
}