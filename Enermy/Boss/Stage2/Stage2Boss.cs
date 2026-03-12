using NUnit.Framework;
using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

enum StageBoss2Faze
{
    Start,
    HalfHp
}

public class Stage2Boss : EnermyBase, IBossAppear, IHpChange
{
    public System.Action OnHalfHp { get; set; }

    [SerializeField] private GameObject[] BossHandAttackPrefeb; const int handsize = 2;
    [SerializeField] private GameObject FallingWaterAttack;

    [SerializeField] LoadingManager loadingManager;

    [SerializeField] private BehaviorGraph angerBehaviorGraph;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Transform deathEffectTransform;

    StageBoss2Faze stageBoss2Faze = StageBoss2Faze.Start;

    Rigidbody2D rb;

    // Boss TriggerName
    public const string BossAngerTriggerName = "TriggerAngry";
    public const string BossSmileTriggerName = "TriggerSmile";

    private void Awake()
    {
        foreach (var item in BossHandAttackPrefeb)
        {
            item.SetActive(false);
        }
        FallingWaterAttack.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        
        stat.onDamage += CheckHalfHp;
        stat.onDeath += Boss2Death;

        AssignCheck();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            BossHandAttack();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            BossFallingWaterAttack();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public void BossAppear()
    {

    }

    public void BossFallingWaterAttack()
    {
        animator.SetTrigger(BossSmileTriggerName);

        
        Invoke("WaterFalling", 1.5f);
    }


    public void BossHandAttack()
    {
        int handIndex = UnityEngine.Random.Range(0, handsize);

        if (handIndex == 0)
        {
            BossLeftHandAttack();
        }
        else
        {
            BossRightHandAttack();
        }

    }

    private void WaterFalling()
    {
        Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
        Vector3 WaterPos = new Vector3(playerPos.x, FallingWaterAttack.transform.position.y, 0);

        FallingWaterAttack.SetActive(true);
        FallingWaterAttack.transform.position = WaterPos;
    }

    private void BossRightHandAttack()
    {
        BossHandAttackPrefeb[1].SetActive(true);
    }

    private void BossLeftHandAttack()
    {
        BossHandAttackPrefeb[0].SetActive(true);
    }

    private void AssignCheck()
    {

        if(BossHandAttackPrefeb.Length != handsize)
        {
            Debug.LogError("BossHandAttackPrefeb Length is not correct");
            return;
        }
        else
        {
            for(int i = 0; i < handsize; ++i)
            {
                BossHandAttackPrefeb[i].SetActive(false);
            }
        }

        if(FallingWaterAttack == null)
        {
            Debug.LogError("FallingWaterAttack is not assigned");
            return;
        }
        else
        {
            FallingWaterAttack.SetActive(false);
        }

    }

    private void CheckHalfHp()
    {
        if((stageBoss2Faze == StageBoss2Faze.Start) && (stat.GetCurrentHp() <= stat.GetMaxHp() / 2))
        {
            ChangeHalfHpFaze();
        }
    }


    private void ChangeHalfHpFaze()
    {
        OnHalfHp.Invoke();

        stageBoss2Faze = StageBoss2Faze.HalfHp;
        sfm.ChangeBehaviorGraph(angerBehaviorGraph);
        animator.SetTrigger(BossAngerTriggerName);
    }

    private void Boss2Death()
    {
        if (deathEffect != null && deathEffectTransform != null)
        {
            GameObject effect = Instantiate(deathEffect, deathEffectTransform.position, Quaternion.identity);
            effect.transform.localScale = new Vector3(40, 40, 40);
        }

        gameObject.SetActive(false);

        

        const float winDelay = 5f;
        Invoke("WinScene", winDelay);
    }

    private void WinScene()
    {
        loadingManager.LoadScene(6);
    }

}
