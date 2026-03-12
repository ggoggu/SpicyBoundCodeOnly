using System;
using System.Collections;
using UnityEngine;

public class Boss1EnvController : MonoBehaviour, IEnvTrigger
{
    [SerializeField] private Animator DestoryAnimaion;

    [SerializeField] private GameObject[] DestoryInFlyingFze;

    [SerializeField] private float animationTime = 3;
    [SerializeField] private float destoryTime = 3;
    [SerializeField] private GameObject[] Clouds;
    private SpriteRenderer[] DestoryInFlyingFzeSpriteRenders;
    [SerializeField] private Material destoryMaterial;

    public bool trigger = false;

    public Action afterTrigger { get; set; }
    public Action onTrigger { get; set; }

    private void Start()
    {
        foreach (GameObject cloud in Clouds)
        {
            cloud.SetActive(false);
        }


        if (trigger)
        {
            Trigger();
        }

    }

    public void Trigger()
    {
        onTrigger?.Invoke();

        if(DestoryAnimaion != null)
        {
            const string destoryParameter = "Destory";
            DestoryAnimaion.SetTrigger(destoryParameter);
        }

        foreach (GameObject cloud in Clouds)
        {
            cloud.SetActive(true);
        }

        Invoke("StartFadeIn", animationTime);
    }

    public void StartFadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

    private void DestoryEnv()
    {
        foreach (GameObject env in DestoryInFlyingFze)
        {
            env.SetActive(false);
        }
    }


    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;
        float fadedTime = destoryTime;

        string matvalue = "_Split_Value";

        while (elapsedTime < fadedTime)
        {
            


            destoryMaterial.SetFloat(matvalue, Mathf.Lerp(1.4f, 0f, elapsedTime / fadedTime));


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        destoryMaterial.SetFloat(matvalue, 1.4f);
        DestoryEnv();

        afterTrigger?.Invoke();

        Destroy(this);
    }
}
