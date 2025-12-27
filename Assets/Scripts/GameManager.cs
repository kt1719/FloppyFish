using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler<GameStateChangedEventArgs> OnGameStateChanged;

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState gameState;
    }

    public enum GameState
    {
        Ready,
        Playing,
        PlayerDeathAnimation,
        PlayerDead
    }

    private GameState gameState;

    private void Awake()
    {
        Instance = this;

        gameState = GameState.Ready;
    }

    private void Start()
    {
        FlappyController.Instance.OnPlayerDead += FlappyController_OnPlayerDead;
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;
        FlappyPhysics.Instance.OnPlayerDeathAnimationEnd += FlappyPhysics_OnPlayerDeathAnimationEnd;
    }

    private void FlappyPhysics_OnPlayerDeathAnimationEnd(object sender, EventArgs e)
    {
        gameState = GameState.PlayerDead;
        OnGameStateChanged.Invoke(this, new GameStateChangedEventArgs 
        {
            gameState = gameState
        });
    }

    private void GameInput_OnPlayerInteract(object sender, EventArgs e)
    {
        if (gameState == GameState.Playing || gameState == GameState.PlayerDeathAnimation) return;

        gameState = gameState == GameState.PlayerDead ? GameState.Ready : (gameState + 1);
        OnGameStateChanged.Invoke(this, new GameStateChangedEventArgs 
        {
            gameState = gameState
        });
    }

    private void FlappyController_OnPlayerDead(object sender, EventArgs e)
    {
        gameState = GameState.PlayerDeathAnimation;
        OnGameStateChanged.Invoke(this, new GameStateChangedEventArgs 
        {
            gameState = gameState
        });
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
