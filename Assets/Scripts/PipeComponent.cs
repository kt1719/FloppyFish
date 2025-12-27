using System;
using System.ComponentModel;
using UnityEngine;

public class PipeComponent : MonoBehaviour
{
    public event EventHandler OnPlayerHit;
    private bool hasBeenHit = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenHit) return;

        if (collision.gameObject.TryGetComponent<BoundingBox>(out BoundingBox _)) {
            return;
        }

        if (!collision.gameObject.TryGetComponent<FlappyController>(out FlappyController _)) {
            Debug.LogError("Colliding with invalid object: " + collision.transform.name);
        }
        hasBeenHit = true;
        OnPlayerHit.Invoke(this, EventArgs.Empty);
    }
}
