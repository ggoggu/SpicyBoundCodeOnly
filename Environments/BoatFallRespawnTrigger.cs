using Unity.VisualScripting;
using UnityEngine;

public class BoatFallRespawnTrigger : MonoBehaviour
{
    [SerializeField] Transform boatRespawnLocation;
    [SerializeField] private float verticalOffset = 40f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IDamageable target))
                target.TakeDamage(1);
            
            Vector3 respawnPosition = boatRespawnLocation.position + new Vector3(0, verticalOffset, 0);
            
            other.transform.position = respawnPosition;
        }
    }
}
