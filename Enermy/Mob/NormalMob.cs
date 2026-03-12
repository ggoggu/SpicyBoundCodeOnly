using UnityEngine;

public class NormalMob : EnermyBase
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Vector3 deathEffectScale = new Vector3(10.0f,10.0f,1.0f);

    protected override void Start()
    {
        base.Start();
        stat.onDeath += Death;
    }

    private  void Death()
    {
        GameObject deatheffect = Instantiate(deathEffect, transform.position, transform.rotation);
        deathEffect.transform.localScale = deathEffectScale;
        Destroy(gameObject);
    }
}
