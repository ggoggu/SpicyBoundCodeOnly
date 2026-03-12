using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public float flashTime = 0.15f;
    public Coroutine flashCoroutine;

    private SpriteRenderer[] spriteRenderers;
    


    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();


        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].material.SetFloat("_FlashAmount", 0);
        }
    }

    public void Flash()
    {
        flashCoroutine = StartCoroutine(ChangeFlashAmount());
    }

    private IEnumerator ChangeFlashAmount()
    {
        float cuurntFlashAmount = 0f;
        
        float elapsedTime = 0f;

        Debug.Log("Flash Start");

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;
            cuurntFlashAmount = Mathf.Lerp(0f, 1f, elapsedTime / flashTime);

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].material.SetFloat("_FlashAmount", cuurntFlashAmount);

            }
            yield return null;
        }

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].material.SetFloat("_FlashAmount", 0);

        }


    }


}
