using System;
using System.Collections;
using Unity.Behavior;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum Boss1State
{
    Start,
    HalfHp,
    QuaterHp
}

[RequireComponent(typeof(HorizonMovement), typeof(WeaponBase))]
public class Boss1 : EnermyBase
{
    [SerializeField] private GameObject jumpattackCollider;
    [SerializeField] LoadingManager loadingManager;
    [SerializeField] private GameObject deathEffect;

    [Header("FlyingState")]
    [SerializeField] private BehaviorGraph flyingBehaviorGraph;
    [SerializeField] private Transform flyingPostion1;
    [SerializeField] private Transform flyingPostion2;
    public System.Action OnHalfHp;
    public System.Action OnAfterQuaterHp;

    [Header("HeadControl And FireBallAttack")]
//    [SerializeField] private GameObject fireballPrefeb;
    [SerializeField] private Transform fireballSwpanPoint;
    [SerializeField] private Transform headTrnasform;
    [SerializeField] private Transform headTarget;

    private Boss1State currentState;
    private IWeaponInterface fireBallAttack;
    private IHeadRotate headRotate;

    private Boss1Anim anim;
    private HorizonMovement movement;
    private Rigidbody2D rb;
    
    protected override void Start()
    {
        base.Start();
        jumpattackCollider.SetActive(false);

        InitStat();

        movement = GetComponent<HorizonMovement>();
        anim = GetComponentInChildren<Boss1Anim>();
        rb = GetComponentInChildren<Rigidbody2D>();

        headTarget = GameManager.Instance.GetPlayer().transform;
        currentState = Boss1State.Start;
        InitAnim();
        InitWeapon();
        InitHeadRoation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.TakeDamage(10);
        }

        
    }

    public void TriggerFireball()
    {
        StartCoroutine(RandomFireBall());
    }

    public void TriggerJumpAttack()
    {
        string trigger = "jumpAttack";
        animator.SetTrigger(trigger);
    }

    private void MoveStart()
    {
        float speed = stat.GetSpeed();
        speed = 30.0f;
        movement.SetMoveSpeed(speed);

        Debug.Log("Boss1MoveStart");
    }

    private void MoveEnd()
    {
        //stop = true;
        movement.SetMoveSpeed(0);

        Debug.Log("Boss1MoveEnd");
    }

    private void SmashCollider()
    {
        jumpattackCollider.SetActive(true);
    }

    private void SmashColliderEnd()
    {
        jumpattackCollider.SetActive(false);
    }

    private void CheckHalfHp()
    {
        if (currentState == Boss1State.Start)
        {

            float halfHp = stat.GetMaxHp() / 2;
            float currentHp = stat.GetCurrentHp();

            if (halfHp > currentHp)
            {
                Debug.Log("Boss1 Change State to HalfHp");
                ChangeHalfHpFaze();
            }

        }
    }

    private void CheckQuaterHp()
    {
        if (currentState == Boss1State.HalfHp)
        {
            float quaterHp = stat.GetMaxHp() / 4;
            float currentHp = stat.GetCurrentHp();

            if (quaterHp > currentHp)
            {
                Debug.Log("Boss1 Change State to QuaterHp");
                ChnageQuaterFaze();
            }
        }
    }

    private void ChangeHalfHpFaze()
    {
        if (currentState != Boss1State.HalfHp)
        {
            currentState = Boss1State.HalfHp;

            movement.bIsHorizon = false;
            rb.gravityScale = 0;
            rb.WakeUp();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            movement.ChangePostionRestract(flyingPostion2, flyingPostion1);

            CapsuleCollider2D col = gameObject.GetComponent<CapsuleCollider2D>();

            if (col == null)
            {
                col = gameObject.AddComponent<CapsuleCollider2D>();
            }

            Vector2 offset = new Vector2(4.25973f, 6.29805f);
            Vector2 size = new Vector2(17.76128f, 32.3813f);

            col.offset = offset;
            col.size = size;


            string flyingtrigger = "flying";
            animator.SetTrigger(flyingtrigger);

            

            const float movetime = 5.0f;

            fireBallAttack.isUeable = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            MovetoPoint(flyingPostion1, movetime);
            Invoke("FixPosition", movetime);
            Invoke("ActiveFireBall", movetime);
            Invoke("ActiveCollider", movetime);

            if (flyingBehaviorGraph != null)
            {
                sfm.ChangeBehaviorGraph(flyingBehaviorGraph);
            }

            if(OnHalfHp != null)
            {
                OnHalfHp();
            }

            OnHalfHp?.Invoke();
        }
    }

    private void ChnageQuaterFaze()
    {
        if (currentState != Boss1State.QuaterHp)
        {
            currentState = Boss1State.QuaterHp;


            fireBallAttack.isUeable = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            const float moveTime = 5.0f;

            MovetoPoint(flyingPostion2, 5);
            FilpHeadRoation();

            Invoke("MoveCloud", moveTime);
            Invoke("FixPosition", moveTime);
            Invoke("ActiveFireBall", moveTime);
            Invoke("ActiveCollider", moveTime);

        }
    }

    private void ActiveFireBall()
    {
        fireBallAttack.isUeable = true;
    }

    private void ActiveCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void QuaterFazeStartMove()
    {
        movement.LockChangeSpeed();
        movement.SetMoveSpeed(25.0f);
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;

        movement.MoveToTargetDirection(flyingPostion2.position);
        Invoke("EndQuaterFazeStartMove", 2.0f);

        
    }

    private void EndQuaterFazeStartMove()
    {
        movement.UnLockChangeSpeed();

        TurnArround();
    }

    private void TurnArround()
    {
        const string turnaround = "turnaround";
        animator.SetTrigger(turnaround);
    }

    private void EndTurnArrund()
    {
        string flyingtrigger = "flying";
        animator.SetTrigger(flyingtrigger);
    }

    private void MoveCloud()
    {
        
        OnAfterQuaterHp?.Invoke();
        
    }


    private void MovetoPoint(Transform postion ,float time)
    {
        
        headRotate.isRotate = false;

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;

        

        movement.LockChangeSpeed();
        movement.SetMoveSpeed(25.0f);
        movement.MoveToTargetUntilArrive(postion);
        //movement.FreeMoveToTarget(postion.position);
        

        
    }

    private void FixPosition()
    {
        headRotate.isRotate = true;
        movement.UnLockChangeSpeed();
        movement.SetMoveSpeed(0);
        movement.FreeMoveToTargetDirection(GameManager.Instance.GetPlayer().transform.position);
        
        


        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.freezeRotation = true;

        movement.bIsRestrict = true;
    }

    private void BossDeath()
    {
        GameManager.Instance.ClearStage(0);

        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = new Vector3(40, 40, 40);
        }

        gameObject.SetActive(false);
        const float delayTime = 2.0f;

        Invoke("LoadWinScene", delayTime);

    }

    private  void LoadWinScene()
    {
        loadingManager.LoadScene(6);
        SceneManager.LoadScene("WinScreen");
    }

    private void ShotFireBall()
    {
        fireBallAttack.TryAttack();
    }

    private void InitWeapon()
    {

        const float fireballdamage = 1.0f;
        const float weaonCooldownTime = 0.1f;

        fireBallAttack = GetComponent<WeaponBase>();
        fireBallAttack.Init(headTarget, fireballdamage, weaonCooldownTime);
        
    }

    private  void InitHeadRoation()
    {

        headRotate = GetComponent<HeadRotate>();
        const float headRotatePrefixAngle = 180.0f;
        headRotate.Init(headTrnasform, headTarget, headRotatePrefixAngle);
    }

    private void InitStat()
    {
        stat.onDamage += CheckHalfHp;
        stat.onDamage += CheckQuaterHp;
        stat.onDeath += BossDeath;
    }

    private void InitAnim()
    {
        if (anim != null)
        {
            anim.OnSmash += SmashCollider;
            anim.EndSmash += SmashColliderEnd;
            anim.OnMove += MoveStart;
            anim.EndMove += MoveEnd;
            anim.OnEndTurnArrund += EndTurnArrund;
        }
        else
        {
            Debug.LogError("Boss1Anim component not found on Boss1.");
        }
    }

    private void FilpHeadRoation()
    {
        headRotate.bIsFlip = true;
        fireballSwpanPoint.Rotate(new Vector3(180, 180, 0));
    }

    private IEnumerator RandomFireBall()
    {
        if (currentState != Boss1State.Start)
        {
            int fireBallCount = UnityEngine.Random.Range(1, 4);

            for (int i = 0; i < fireBallCount; i++)
            {
                ShotFireBall();
                yield return new WaitForSeconds(0.2f);
            }

        }
    }
}
