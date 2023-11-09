using System;
using UnityEngine;

public class XpOrb : Recollectable
{
    [SerializeField] private int _xpToAdd;
    
    public static Action<int> OnXpOrbCollected;
    
    public override void Collect()
    {
        OnXpOrbCollected?.Invoke(_xpToAdd);
        base.Collect();
    }
}