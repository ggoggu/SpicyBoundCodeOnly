using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint initSize;
    [SerializeField] private PooledObject prefab;

    private Stack<PooledObject> pool;


    protected void Start()
    {
        pool = new Stack<PooledObject>();

        for(int i = 0; i < initSize; i++)
        {

            PooledObject newObj = Instantiate(prefab);
            newObj.Pool = this;
            newObj.gameObject.SetActive(false);
            pool.Push(newObj);
        }

    }

    public PooledObject GetPooledObejct()
    {
        if(pool.Count == 0)
        {
            PooledObject newObj = Instantiate(prefab, transform);
            newObj.Pool = this;
            return newObj;
        }
        else
        {
            PooledObject obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }



    public virtual void ReturnPool(PooledObject pooledObject)
    {
        pool.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
