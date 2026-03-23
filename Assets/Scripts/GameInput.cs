using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_preformed;
        playerInputActions.Player.InteractAlternatte.performed += InteractAlt_preformed;
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
