using UnityEngine;

public class BossDeathEffect : MonoBehaviour
{
    [SerializeField ]GameObject DeathEffect;

    private void Start()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }
}
