using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnermySFM))]
[RequireComponent(typeof(EnermyStat))]
[RequireComponent(typeof(DamageFlash))]
public class EnermyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private EnermyData enermydata;


    protected EnermyStat stat;
    protected EnermySFM sfm;
    protected Animator animator;
    protected DamageFlash damageFlash;

    public Action OnDeath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Die()
    {
        // throw new NotImplementedException();
    }

    public virtual bool TakeDamage(int amount)
    {
        stat.TakeDamage(amount);
        

        return true;
    }

    public virtual void DamageFlash()
    {
        damageFlash.Flash();
    }

    protected virtual void Start()
    {
        stat = GetComponent<EnermyStat>();
        sfm = GetComponent<EnermySFM>();
        damageFlash = GetComponentInChildren<DamageFlash>();
        animator = GetComponentInChildren<Animator>();
        stat.SetupStat(enermydata);

        stat.onDamage += DamageFlash;
        stat.onDeath += Die;

        sfm.SetUp(GameManager.Instance.GetPlayer().transform);
        sfm.SetSpeed(stat.GetSpeed());
    }
}
