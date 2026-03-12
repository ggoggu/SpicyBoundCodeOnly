using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2RainSpawner : ObjectPool
{
    public GameObject boss;

    public Transform leftMax; public Transform rightMax;
    public float rainSpawnTimeMin = 0.5f;
    public float rainSpawnTimeMax = 1.5f;

    float currentTime = 0;
    float maxTime = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        base.Start();
        

        boss.GetComponent<IHpChange>().OnHalfHp += Accelate;

        maxTime = Random.Range(rainSpawnTimeMin, rainSpawnTimeMax);

       // RainStart();
    }

    private void Update()
    {
        CheckRain();
    }

    void CheckRain()
    {
        if (currentTime < 0)
        {
            maxTime = Random.Range(rainSpawnTimeMin, rainSpawnTimeMax);
            currentTime = maxTime;
            DropRain();
        }

        currentTime -= Time.deltaTime;
    }

    private void RainStart()
    {
        DropRain();
        float time = Random.Range(rainSpawnTimeMin, rainSpawnTimeMax);

        Invoke("RainStart", time);
    }

    private void Accelate()
    {
        rainSpawnTimeMin *= 0.5f;
        rainSpawnTimeMax *= 0.5f;
    }

    private void DropRain()
    {
        float x = Random.Range(leftMax.position.x, rightMax.position.x);
        Vector3 pos = new Vector3(x, leftMax.position.y, 0);


        PooledObject rain = GetPooledObejct();

        rain.transform.position = pos;
    }

}

