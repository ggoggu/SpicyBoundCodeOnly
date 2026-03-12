using UnityEngine;

public class SwingingBoxPlatform : MonoBehaviour
{
    private Transform playerTransform = null;
    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;

    void LateUpdate()
    {
        // We use LateUpdate to ensure this runs after the platform has finished its movement for the frame.
        if (playerTransform)
        {
            // Calculate how much the platform has moved and rotated since the last frame
            Vector3 positionDelta = transform.position - lastPlatformPosition;
            Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(lastPlatformRotation);

            // Apply the same deltas to the player
            playerTransform.position += positionDelta;
            playerTransform.rotation = rotationDelta * playerTransform.rotation;

            // Update the platform's last known position and rotation for the next frame
            UpdateLastTransform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instead of parenting, we just store a reference to the player's transform
            playerTransform = other.transform;
            UpdateLastTransform();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform.rotation = Quaternion.identity;
            // Clear the reference when the player leaves
            playerTransform = null;
        }
    }
    
    // A helper function to store the platform's current transform state
    private void UpdateLastTransform()
    {
        lastPlatformPosition = transform.position;
        lastPlatformRotation = transform.rotation;
    }
}
