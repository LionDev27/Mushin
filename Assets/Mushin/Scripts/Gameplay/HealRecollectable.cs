using UnityEngine;

public class HealRecollectable : Recollectable
{
    [SerializeField] private int _healValue;

    public override void Collect()
    {
        base.Collect();
        PlayerComponents.Instance.PlayerDamageable.Heal(_healValue);
    }
}