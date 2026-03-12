using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    void Start()
    {
        float animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength);
    }
}
