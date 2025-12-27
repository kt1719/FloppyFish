using UnityEngine;

public class PipeDespawnPoint : MonoBehaviour
{
    public static PipeDespawnPoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
