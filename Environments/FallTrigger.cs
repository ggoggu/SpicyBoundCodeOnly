using Unity.VisualScripting;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IDamageable target))
                target.TakeDamage(3);
        }
    }
}
