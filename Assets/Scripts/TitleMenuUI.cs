using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuUI : MonoBehaviour
{
    public static TitleMenuUI Instance;

    public event EventHandler OnPlayButtonPressed;
    public event EventHandler OnExitButtonPressed;

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playButton.onClick.AddListener(() => OnPlayButtonPressed.Invoke(this, EventArgs.Empty));
        exitButton.onClick.AddListener(() => OnExitButtonPressed.Invoke(this, EventArgs.Empty));
    }
}
