using UnityEngine;
using System;

public class HeartCoin : MonoBehaviour
{
    public Action OnPlayerTouched;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerTouched?.Invoke();
            
            if (other.TryGetComponent(out IDamageable target))
                target.TakeDamage(-1);
            
            Destroy(gameObject);
        }
    }
}
