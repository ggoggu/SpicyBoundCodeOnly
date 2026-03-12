using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class CloudRemove : MonoBehaviour
{
    [SerializeField] private Transform StartPoint;
    [SerializeField] private GameObject effect;
    TriggerAction triggerAction;

    private void Start()
    {
        triggerAction = GetComponentInChildren<TriggerAction>();
        triggerAction.EnterTrigger += CloudBoom;
    }

    public void Init(float speed)
    {
        GetComponentInChildren<Collider2D>().enabled = true;
        IMovingPlatform movingPlatform = GetComponent<IMovingPlatform>();
        movingPlatform.speed = speed;
    }

    private void CloudBoom(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GetPlayer().transform.parent = null;
            gameObject.transform.position = StartPoint.position;

            Debug.Log("CloudBoom");
        }

        
    }

}
