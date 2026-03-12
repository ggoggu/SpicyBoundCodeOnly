using UnityEngine;
using UnityEngine.Events;

public class MovingBoat : MonoBehaviour
{
    public Transform startPos, endPos;
    public float speed = 4f;
    private Vector3 targetPos;
    private bool shouldBeMoving = false;
    private bool didNotDepartYet = true;

    public event UnityAction OnBoatDeparted; 

    private void Start()
    {
        targetPos = endPos.position;
    }
    
    private void Update()
    {
        if (Vector2.Distance(transform.position, startPos.position) < 0.05f)
            targetPos = endPos.position;
        
        /*if (Vector2.Distance(transform.position, endPos.position) < 0.05f)
            targetPos = startPos.position;*/
        
        if (shouldBeMoving)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            shouldBeMoving = true;

            if (didNotDepartYet)
            {
                OnBoatDeparted?.Invoke();
                didNotDepartYet = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.transform.rotation = Quaternion.identity;
            shouldBeMoving = false;
        }
    }
}
