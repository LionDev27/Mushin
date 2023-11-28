using System;
using UnityEngine;

public class XpOrbCollectable : Collectable
{
    [SerializeField] private int _xpToAdd;
    
    public override void Collect()
    {
        base.Collect();
        player.OnXPOrbCollected(_xpToAdd); 
    }
}