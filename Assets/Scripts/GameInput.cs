using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPlayerInteract;
    
    public event EventHandler OnPlayerPaused;
    

    PlayerInputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    private void Start()
    {
        inputActions.Player.Interact.performed += PlayerInputActions_Flap;
        inputActions.Player.Paused.performed += PlayerInputActions_Paused;
    }

    private void PlayerInputActions_Paused(InputAction.CallbackContext context)
    {
        OnPlayerPaused.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInputActions_Flap(InputAction.CallbackContext context)
    {
        OnPlayerInteract.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputActions.Disable();
        inputActions.Dispose();
    }
}
