using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public event EventHandler OnPlayerHit;

    [SerializeField] private PipeComponent pipeBase;
    [SerializeField] private PipeComponent pipeTop;

    private void Start()
    {
        pipeBase.OnPlayerHit += PipeComponent_OnPlayerHit;
        pipeTop.OnPlayerHit += PipeComponent_OnPlayerHit;
    }

    private void PipeComponent_OnPlayerHit(object sender, EventArgs e)
    {
        OnPlayerHit.Invoke(this, EventArgs.Empty);
    }
}
