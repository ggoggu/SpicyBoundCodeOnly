using System;
using UnityEngine;

public class Boss1CloudMoveController : MonoBehaviour, IEnvTrigger
{
    [SerializeField] private GameObject[] Clouds;
    public float cloudSpeed = 2.0f;
    public Action afterTrigger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action onTrigger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    [SerializeField] GameObject Boss;

    private void Start()
    {
        EnermyStat stat = Boss.GetComponent<EnermyStat>();

        if (stat != null)
        {
            stat.onDeath += BossDeath;
        }
        else
        {
            Debug.LogWarning("EnermyStat component not found on Boss");
        }
    }


    public void Trigger()
    {
        foreach (GameObject cloud in Clouds)
        {
            CloudRemove cloudRemove = cloud.GetComponent<CloudRemove>();

            if (cloudRemove != null)
            {
                cloudRemove.Init(cloudSpeed);
            }
            else
            {
                Debug.LogWarning("CloudRemove component not found on " + cloud.name);
            }
        }
    }

    private void BossDeath()
    {
        foreach (GameObject cloud in Clouds)
        {
            CloudRemove cloudRemove = cloud.GetComponent<CloudRemove>();

            if (cloudRemove != null)
            {
                cloudRemove.Init(0);
            }
            else
            {
                Debug.LogWarning("CloudRemove component not found on " + cloud.name);
            }
        }
    }


}
