using System;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private enum Scenes
    {
        GameMenu,
        GameScene
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        TitleMenuUI.Instance.OnPlayButtonPressed += TitleMenuUI_OnPlayButtonPressed;
        TitleMenuUI.Instance.OnExitButtonPressed += TitleMenuUI_OnExitButtonPressed;
    }

    private void TitleMenuUI_OnPlayButtonPressed(object sender, EventArgs e)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes.GameScene.ToString());
    }

    private void TitleMenuUI_OnExitButtonPressed(object sender, EventArgs e)
    {
        Application.Quit();
    }
}
