using System;
using UnityEngine;

public class XpOrbCollectable : Collectable
{
    [SerializeField] private int _xpToAdd;
    
    public static Action<int> OnXpOrbCollected;
    
    public override void Collect()
    {
        base.Collect();
        player.OnXPOrbCollected(_xpToAdd);
        // OnXpOrbCollected?.Invoke(_xpToAdd);
    }
}