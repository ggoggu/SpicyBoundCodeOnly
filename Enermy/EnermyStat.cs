using System;
using UnityEngine;

public class EnermyStat : MonoBehaviour
{
    public Action onDeath;
    public Action onDamage;

    private float maxHp;
    private float currentHp;
    private float damage;
    private float speed;
    private bool isDead = false;

    public void SetupStat(EnermyData data)
    {
        maxHp = data.maxHp;
        currentHp = maxHp;
        damage = data.damage;
        speed = data.speed;
    }

    public float TakeDamage(int damage)
    {
        currentHp -= damage;
        Mathf.Clamp(currentHp, 0, maxHp);

        onDamage?.Invoke();


        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("EnermyDead");
            onDeath?.Invoke();
        }

        Debug.Log("EnermyDamaged");

        return damage;
    }


    public float GetMaxHp()
    {
        return maxHp;
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }

    public float GetSpeed()
    {
        return speed;
    }

}
