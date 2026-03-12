using Unity.VisualScripting;
using UnityEngine;

public class FallRespawnTrigger : MonoBehaviour
{
    [SerializeField] Transform respawnLocation;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IDamageable target))
                target.TakeDamage(1);
            
            other.transform.position = respawnLocation.transform.position;
        }
    }
}
