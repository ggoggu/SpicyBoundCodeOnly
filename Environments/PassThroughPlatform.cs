using System.Collections;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D _platformCollider; // The main solid collider of the platform

    private bool _bIsPlayerOnPlatform;

    // Using a counter for players overlapping the trigger ensures multiple players or other objects don't interfere
    private int _playersInPassThroughTriggerCount = 0; 
    
    [Header("Platform Settings")]
    [Tooltip("The time in seconds before reactivating the main collider after player initiates pass-through.")]
    public float initialDisableDuration = 0.1f; // Small initial delay to ensure player clears
    
    private Coroutine _disableAndReEnableCoroutine; // Reference to the active coroutine

    void Awake()
    {
        // Get the main solid collider (usually the first one found)
        _platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_bIsPlayerOnPlatform && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            DisablePlatform();
        }
    }

    private void DisablePlatform()
    {
        if (_platformCollider.enabled)
        {
            _platformCollider.enabled = false;
            
            // Stop any existing coroutine to prevent conflicts
            if (_disableAndReEnableCoroutine != null)
            {
                StopCoroutine(_disableAndReEnableCoroutine);
            }
            _disableAndReEnableCoroutine = StartCoroutine(WaitForPlayerToClear());
        }
    }

    private IEnumerator WaitForPlayerToClear()
    {
        yield return new WaitForSeconds(initialDisableDuration); 

        // Only re-enable when the _playersInPassThroughTriggerCount is zero; player is not inside trigger box
        while (_playersInPassThroughTriggerCount > 0)
        {
            yield return null; // Wait for the next frame
        }

        // Once no players are in the trigger zone, re-enable the main platform collider.
        _platformCollider.enabled = true;
        _disableAndReEnableCoroutine = null; // Clear coroutine reference
    }

    // --- Handling the main solid collider collisions ---
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _bIsPlayerOnPlatform = true;

            // If player lands back on the platform, ensure it's enabled and stop any pending re-enable
            if (!_platformCollider.enabled)
            {
                _platformCollider.enabled = true;
            }
            if (_disableAndReEnableCoroutine != null)
            {
                StopCoroutine(_disableAndReEnableCoroutine);
                _disableAndReEnableCoroutine = null;
            }
        }
    }

    // for outer trigger box
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            _bIsPlayerOnPlatform = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playersInPassThroughTriggerCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playersInPassThroughTriggerCount--;
            // Ensure count doesn't go below zero
            if (_playersInPassThroughTriggerCount < 0) _playersInPassThroughTriggerCount = 0; 
        }
    }
}
