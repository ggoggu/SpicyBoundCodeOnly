using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerAction : MonoBehaviour
{
    public Action<Collider2D> EnterTrigger;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnterTrigger != null)
        {
            EnterTrigger(collision);
        }
    }
}
