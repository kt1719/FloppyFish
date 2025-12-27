using System;
using UnityEngine;

public class PipeComposite : MonoBehaviour
{
    public event EventHandler OnPlayerHit;
    public event EventHandler OnPlayerScore;
    public event EventHandler OnPipeCompositeDespawned;

    [SerializeField] private float pipeMovementSpeed = 10f;
    [SerializeField] private float pipeGapLength = 8f;
    [SerializeField] private Pipe topPipe;
    [SerializeField] private Pipe bottomPipe;
    [SerializeField] private PipeComponent pipeScoreTrigger;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -pipeMovementSpeed;
        topPipe.transform.position += new Vector3(0, pipeGapLength/2, 0);
        bottomPipe.transform.position -= new Vector3(0, pipeGapLength/2, 0);
    }

    private void Start()
    {
        topPipe.OnPlayerHit += Pipe_OnPlayerHit;   
        bottomPipe.OnPlayerHit += Pipe_OnPlayerHit;   
        pipeScoreTrigger.OnPlayerHit += PipeScoreTrigger_OnPlayerHit;   
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.Ready:
                CleanupEvents();
                Destroy(gameObject);
                break;
            case GameManager.GameState.Playing:
                rb.linearVelocityX = -pipeMovementSpeed;
                break;
            case GameManager.GameState.PlayerDeathAnimation:
                rb.linearVelocityX = 0f;
                break;
        }   
    }

    private void PipeScoreTrigger_OnPlayerHit(object sender, EventArgs e)
    {
        OnPlayerScore.Invoke(this, EventArgs.Empty);
    }

    private void Pipe_OnPlayerHit(object sender, EventArgs e)
    {
        OnPlayerHit.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if (rb.position.x < PipeDespawnPoint.Instance.GetPosition().x)
        {
            OnPipeCompositeDespawned.Invoke(this, EventArgs.Empty);
            CleanupEvents();
            Destroy(gameObject);
        }
    }

    private void CleanupEvents()
    {
        topPipe.OnPlayerHit -= Pipe_OnPlayerHit;   
        bottomPipe.OnPlayerHit -= Pipe_OnPlayerHit;   
        pipeScoreTrigger.OnPlayerHit -= PipeScoreTrigger_OnPlayerHit;   
        GameManager.Instance.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }
}
