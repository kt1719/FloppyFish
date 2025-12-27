using UnityEngine;

public class PipeSpawnPoint : MonoBehaviour
{
    public static PipeSpawnPoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
