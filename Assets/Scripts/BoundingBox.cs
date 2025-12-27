using System;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public static event EventHandler OnPlayerHitBounds;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<FlappyController>(out FlappyController player) &&
            GameManager.Instance.GetGameState() == GameManager.GameState.Playing)
        {
            OnPlayerHitBounds.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy()
    {
        OnPlayerHitBounds = null;
    }
}
