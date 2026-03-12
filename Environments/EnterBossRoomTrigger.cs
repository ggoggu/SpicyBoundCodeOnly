using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnterBossRoomTrigger : MonoBehaviour
{
    public GameObject boss;
    public BoxCollider2D bossRoomCollider;
    public GameObject BossHpbar;
    public string targetbooleanName = "bIsTarget";

    private void Awake()
    {
        BossHpbar.SetActive(false);
        GetComponent<Collider2D>().isTrigger = true;
        bossRoomCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            EnterBossRoom();

            Debug.Log("Entered Boss Room Trigger");
        }

        
    }

    private void EnterBossRoom()
    {
        BossHpbar.SetActive(true);
        bossRoomCollider.enabled = true;
        boss.GetComponent<EnermySFM>().SetBoolean(targetbooleanName, true);
    }
}
