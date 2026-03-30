using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;
    private PlayerInputActions playerInputActions;
    public event EventHandler OnPauseAction;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_preformed;
        playerInputActions.Player.InteractAlternatte.performed += InteractAlt_preformed;
        playerInputActions.Player.pause.performed += pause_preformed;
    }

    private void pause_preformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    private void InteractAlt_preformed(InputAction.CallbackContext obj)
    {
        OnInteractAltAction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_preformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 GetMovemenetVectorNormailized()
    {
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
