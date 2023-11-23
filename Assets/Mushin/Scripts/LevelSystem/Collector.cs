using UnityEngine;

public class Collector : MonoBehaviour
{
    public bool playerCanHeal;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Collectable collectable))
        {
            if (collectable is HealCollectable && !playerCanHeal) return;
            collectable.Collect();
        }
    }
}