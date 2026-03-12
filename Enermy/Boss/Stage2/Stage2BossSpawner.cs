using UnityEngine;

public class Stage2BossSpawner : MonoBehaviour
{
    public GameObject[] needAwake;

    Collider2D col;

    private void Start()
    {
        foreach (var item in needAwake)
        {
            item.SetActive(false);
        }

        col = GetComponent<Collider2D>();

        needAwake[0].GetComponent<EnermyStat>().onDeath += BossDeath;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var item in needAwake)
            {
                item.SetActive(true);
            }

            col.enabled = false;
        }


    }

    private void BossDeath()
    {

        foreach (var item in needAwake)
        {
            item.SetActive(false);
        }

        
    }




}
