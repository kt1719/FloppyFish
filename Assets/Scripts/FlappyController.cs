using System;
using UnityEngine;

public class FlappyController : MonoBehaviour
{
    public event EventHandler OnPlayerDead;
    
    public static FlappyController Instance { get; private set; }

    private FlappyPhysics flappyPhysics;

    private void Awake()
    {
        Instance = this;
        
        flappyPhysics = GetComponent<FlappyPhysics>();
    }

    private void Start()
    {
        PipeController.Instance.OnPlayerHit += PipeController_OnPlayerHit;
        BoundingBox.OnPlayerHitBounds += PipeController_OnPlayerHit;
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;
    }

    private void GameInput_OnPlayerInteract(object sender, EventArgs e)
    {
        if (GameManager.Instance.GetGameState() != GameManager.GameState.Playing) return;
        
        flappyPhysics.Flap();
    }

    private void PipeController_OnPlayerHit(object sender, EventArgs e)
    {
        if (!(GameManager.Instance.GetGameState() == GameManager.GameState.Playing)) return;

        OnPlayerDead.Invoke(this, EventArgs.Empty);
    }
}
