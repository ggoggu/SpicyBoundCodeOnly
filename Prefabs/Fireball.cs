using UnityEngine;

namespace Prefabs
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] float fireballSpeed = 30f;
        [SerializeField] int fireballDamage = 10;
        [SerializeField] GameObject shootingEffectPrefab;
    
        private Rigidbody2D rb; 
    
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            SetFireballVelocity();
        }

        private void SetFireballVelocity()
        {
            rb.linearVelocity = transform.right * fireballSpeed;
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.TryGetComponent(out IDamageable target))
                    target.TakeDamage(fireballDamage);
                    
                Debug.Log(other.gameObject.name);
                
                Instantiate(shootingEffectPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
