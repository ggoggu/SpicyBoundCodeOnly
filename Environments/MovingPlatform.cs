using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos, endPos;
    public float speed = 4f;
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = endPos.position;
    }
    
    private void Update()
    {
        if (Vector2.Distance(transform.position, startPos.position) < 0.05f)
            targetPos = endPos.position;
        
        if (Vector2.Distance(transform.position, endPos.position) < 0.05f)
            targetPos = startPos.position;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = this.transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = null;
    }
}
