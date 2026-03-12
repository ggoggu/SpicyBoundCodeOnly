using Unity.Behavior;
using UnityEngine;

public class CheckTargetTrigger : MonoBehaviour
{
    public bool playerDetected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerDetected = true;
    }
}
