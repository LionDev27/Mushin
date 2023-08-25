using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out XpOrb orb))
        {
            orb.SetTarget(transform.parent);
        }
    }
}
