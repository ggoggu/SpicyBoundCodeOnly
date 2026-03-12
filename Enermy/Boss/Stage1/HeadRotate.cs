using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadRotate : MonoBehaviour, IHeadRotate
{
    public Transform headTransform; // The transform of the head to rotate
    public Transform target;
    public float prefixAngle = 180f; // Optional prefix angle to adjust the rotation
    public float flipedprefixAngle = 0f; // Optional prefix angle when flipped
    public bool isRotate { get; set; } // Flag to indicate if the head is currently rotating
    
    private Quaternion targetRotaion;
    float currentTime = 0f; // Current time for rotation
    float recognizeTime = 0.5f; // Time to wait before rotating to the target

    public bool bIsFlip { get; set; }

    private void LateUpdate()
    {
        if(isRotate)
        {
            RotateToTarget();
        }

        /*
        if (currentTime < 0)
        {
            
            currentTime = recognizeTime; // Reset the timer after rotation
        }

        currentTime -= Time.deltaTime;
        */
    }

    public void Init(Transform headTrnsform, Transform target, float prefixAngle = 90.0f)
    {
        if (headTrnsform == null || target == null)
        {
            Debug.LogError("HeadTransform or Target is not assigned in HeadRotate script.");
            return;
        }
        this.headTransform = headTrnsform;
        this.target = target;
        this.prefixAngle = prefixAngle;

    }



    private void RotateToTarget()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned in HeadRotate script.");
            return;
        }
        Vector3 direction = target.position - headTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + prefixAngle;
        // Check if the target is on the left side
        //bool isFacingLeft;
        // Rotate the head towards the target
        

        const float rotationSpeed = 0.1f;

        Quaternion currentRoation = headTransform.rotation;
        if(bIsFlip == true)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + flipedprefixAngle;
            // Check if the target is on the left side
            headTransform.localScale = new Vector3(-1, headTransform.localScale.y, headTransform.localScale.z);
        }
        else
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + prefixAngle;
            // Check if the target is on the left side
            
        }

        targetRotaion = Quaternion.Euler(currentRoation.x, currentRoation.y, angle);





        headTransform.rotation = targetRotaion;
        //headTransform.transform.rotation = Quaternion.Slerp(currentRoation, targetRotaion, rotationSpeed);
    }
}
