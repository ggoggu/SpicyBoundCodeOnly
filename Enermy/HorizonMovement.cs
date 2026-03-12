using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class HorizonMovement : MonoBehaviour
{
    [Header("Postion Restrict")]
    public bool bIsRestrict = false;
    [SerializeField] Transform rightRestract = null;
    [SerializeField] Transform leftRestract = null;
    public bool bIsHorizon = true;

    [Header("Sprite Rotaion")]
    [SerializeField] Transform rotationTransform;
    [SerializeField] bool bIsSprite = true;
    [SerializeField] bool bIsFlipFornt = false;

    [Header("Ground Check")]
    [SerializeField] bool bIsCheckGround = true;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkRadius = 0.3f;
    [SerializeField] private LayerMask whatIsGround = 8;
    public bool isGrounded;

    [Header("Change Speed Lock")]
    private bool bIsChangeSpeedLock = false;
    private uint changeSpeedLock = 0;

    private SpriteRenderer spriteRenderer;
    private float front = 1.0f;
    private float moveDirection = 0f;
    private Vector2 freeMoveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private float moveSpeed = 0;


    public Action OnFlip;
    public bool bIsTarget = false;
    public Transform target;
    public float targetThreshold = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rotationTransform == null)
        {
            rotationTransform = transform;
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (bIsFlipFornt)
            front = -front;

    }

    public void ChangePostionRestract(Transform left, Transform right)
    {
        leftRestract = left;
        rightRestract = right;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetDirection()
    {
        return moveDirection;
    }

    public float GetFront()
    {
        return front;
    }

    private void FixedUpdate()
    {

        if (bIsTarget && target != null)
        {
            if (Mathf.Abs(target.position.x - transform.position.x) > targetThreshold)
            {
                if (bIsHorizon)
                {
                    MoveToTargetDirection(target.position);
                }
                else
                {
                    FreeMoveToTargetDirection(target.position);
                }
            }
            else
            {
                bIsTarget = false;
                moveDirection = 0f;
            }
        }

        if (bIsHorizon)
        {
            UpdateVelocity();
        }
        else
        {
            UpdateVelocityFree();
        }

        CheckRectract();

        if(bIsCheckGround)
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    public void MoveToTargetUntilArrive(Transform target)
    {
        // If you want change movement Make UnTarget
        this.target = target;
        bIsTarget = true;
    }

    public void MoveToTargetDirection(Vector2 target)
    {
        float targetPos = target.x;
        float agentPos = transform.position.x;

        ChangeDirection(targetPos - agentPos);
    }

    public void ChangeDirection(float direction)
    {
        if (direction > 0)
        {
            moveDirection = 1.0f;
        }
        else if (direction < 0)
        {
            moveDirection = -1.0f;
        }

        CheckFront();

    }

    public void FreeMoveToTargetDirection(Vector2 target)
    {
        rb.gravityScale = 0;
        bIsHorizon = false;

        Vector2 targetPos = target;
        Vector2 agentPos = transform.position;

        freeMoveDirection = targetPos - agentPos;
        freeMoveDirection.Normalize();

        ChangeDirection(freeMoveDirection.x);
    }

    public void LockChangeSpeed()
    {
        bIsChangeSpeedLock = true;
    }

    public void UnLockChangeSpeed()
    {
        bIsChangeSpeedLock = false;
        changeSpeedLock = 0;
    }

    public void EndLockChangeSpeed()
    {
        changeSpeedLock--;
    }

    public void SetMoveSpeed(float speed)
    {
        if (bIsChangeSpeedLock)
        {
            changeSpeedLock++;
        }

        if(changeSpeedLock > 1)
        {
            Debug.LogWarning("Change Speed Lock is On, can't change speed");
            return;
        }

        moveSpeed = speed;
    }

    public void UpdateAnimationSpeed(string name, ref Animator animator)
    {
       
        animator.SetFloat(name, Math.Abs(rb.linearVelocityX));
    }

    private void CheckFront()
    {
        if (front != moveDirection)
        {
            FilpSprite();
        }
    }

    private void FilpSprite()
    {
        if(bIsSprite)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }else
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, 180, 0));
            if(OnFlip != null)
            {
                OnFlip.Invoke();
            }
        }
        
        front = -front;


    }

   


    private void UpdateVelocity()
    {
        
        rb.linearVelocityX = moveDirection * moveSpeed;
            
    }

    private void UpdateVelocityFree()
    {

        rb.linearVelocity = freeMoveDirection * moveSpeed;

    }

    private void CheckRectract()
    {
        if(bIsRestrict)
        {
            Transform transform = gameObject.transform;
            if (rightRestract != null)
            {
                if (transform.position.x >= rightRestract.position.x)
                {
                    transform.position = new Vector3(rightRestract.position.x, transform.position.y, transform.position.z);
                }
            }

            if (leftRestract != null)
            {
                if (transform.position.x <= leftRestract.position.x)
                {
                    transform.position = new Vector3(leftRestract.position.x, transform.position.y, transform.position.z);
                }
            }
        }
        
    }


}
