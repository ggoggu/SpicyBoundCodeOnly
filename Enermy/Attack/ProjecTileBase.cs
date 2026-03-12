using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class ProjectileBase : MonoBehaviour
{
    public Action OnAttack;

    [SerializeField] protected GameObject effect;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float rotationPreFix = 180.0f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationPreFix);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {

            if (collision.gameObject.tag == "Player")
            {
                if (OnAttack != null)
                {
                    OnAttack.Invoke();
                }

                if (effect != null)
                {
                    Instantiate(effect, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            
            if(collision.tag == "Player" )
            {
                if(OnAttack != null)
                {
                    OnAttack.Invoke();
                }

                if (effect != null)
                {
                    Instantiate(effect, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
