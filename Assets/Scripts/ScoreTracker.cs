using System;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;
    private void Start()
    {
        PipeController.Instance.OnPlayerScore += PipeController_OnScorePoint;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        
        UpdateText();
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.Ready:
                score = 0;
                UpdateText();
                break;
        }   
    }

    private void PipeController_OnScorePoint(object sender, EventArgs e)
    {
        score += 1;
        UpdateText();
    }

    public void UpdateText()
    {
        scoreText.text = "Score: " + score;
    }
}
