using System;
using TMPro;
using UnityEngine;

public class IndicatorUI : MonoBehaviour
{
    [SerializeField] private Transform pressStartUI;
    [SerializeField] private Transform gameOverUI;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        pressStartUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.Ready:
                pressStartUI.gameObject.SetActive(true);
                gameOverUI.gameObject.SetActive(false);
                break;
            case GameManager.GameState.PlayerDead:
                pressStartUI.gameObject.SetActive(false);
                gameOverUI.gameObject.SetActive(true);
                break;
            default:
                pressStartUI.gameObject.SetActive(false);
                gameOverUI.gameObject.SetActive(false);
                break;
        }   
    }
}
