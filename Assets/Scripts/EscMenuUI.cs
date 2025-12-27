using System;
using UnityEngine;
using UnityEngine.UI;

public class EscMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        GameInput.Instance.OnPlayerPaused += GameInput_OnPlayerPaused;
        resumeButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            });
        exitButton.onClick.AddListener(() => Application.Quit());

        gameObject.SetActive(false);
    }

    private void GameInput_OnPlayerPaused(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
