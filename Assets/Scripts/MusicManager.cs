using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private SoundFxScriptableObject soundFxScriptableObject;

    private void Start()
    {
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        PipeController.Instance.OnPlayerScore += PipeController_OnPlayerScore;
    }

    private void PipeController_OnPlayerScore(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(soundFxScriptableObject.pointScoreSound, Camera.main.transform.position, 0.5f);
    }

    private void GameInput_OnPlayerInteract(object sender, EventArgs e)
    {
        if (!(GameManager.Instance.GetGameState() == GameManager.GameState.Playing) 
            && !(GameManager.Instance.GetGameState() == GameManager.GameState.Ready)) return;

        AudioSource.PlayClipAtPoint(soundFxScriptableObject.flapSound, Camera.main.transform.position, 0.5f);
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.PlayerDeathAnimation:
                AudioSource.PlayClipAtPoint(soundFxScriptableObject.gameOverSound, Camera.main.transform.position, 0.5f);
                break;
        }   
    }
}
