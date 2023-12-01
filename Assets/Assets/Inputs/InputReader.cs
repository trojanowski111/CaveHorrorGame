using UnityEngine;
using System;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions, PlayerInputActions.IUIActions, PlayerInputActions.IVehicleActions
{
    // PLAYER EVENTS
    public Action<Vector2> moveEvent = delegate {};
    public Action<Vector2> cameraMoveEvent = delegate {};
    public Action<bool> sprintEvent = delegate {};
    public Action<bool> crouchEvent = delegate {};
    public Action interactEvent = delegate {};
    public Action leaveInpectionEvent = delegate {};
    public Action<Vector2> inspectionZoomEvent = delegate {};
    public Action skipDialogueEvent = delegate {};
    public Action pauseEvent = delegate {};

    // UI EVENTS
    public Action navigateEvent = delegate {};
    public Action submitEvent = delegate {};
    public Action cancelEvent = delegate {};
    public Action pointEvent = delegate {};
    public Action clickEvent = delegate {};
    public Action scrollWheelEvent = delegate {};
    public Action middleClickEvent = delegate {};
    public Action rightClickEvent = delegate {};
    public Action trackedDevicePositionEvent = delegate {};
    public Action trackedDeviceOrientationEvent = delegate {};
    public Action resumeEvent = delegate {};


    // ACTUAL INPUT ACTION CLASS
    private PlayerInputActions playerInput;

    private void OnEnable()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInputActions();
            playerInput.Player.SetCallbacks(this);
            playerInput.UI.SetCallbacks(this);
            playerInput.Vehicle.SetCallbacks(this);
        }

        EnableGameplayInput();
    }
    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableGameplayInput()
    {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
        playerInput.Vehicle.Disable();
    }
    public void EnableUIInput()
    {
        playerInput.UI.Enable();
        playerInput.Player.Disable();
        playerInput.Vehicle.Disable();
    }
    public void EnableVehicleInput()
    {
        playerInput.Vehicle.Enable();
        playerInput.Player.Disable();
        playerInput.UI.Disable();
    }
    public void DisableAllInput()
    {
        playerInput.Player.Disable();
        playerInput.UI.Disable();
        playerInput.Vehicle.Disable();
    }

    // PLAYER INPUT
    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        cameraMoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
            sprintEvent.Invoke(true);
            break;

            case InputActionPhase.Canceled:
            sprintEvent.Invoke(false);
            break;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
            crouchEvent.Invoke(true);
            break;

            case InputActionPhase.Canceled:
            crouchEvent.Invoke(false);
            break;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        interactEvent.Invoke();
    }
    public void OnLeaveInspection(InputAction.CallbackContext context)
    {
        if(context.performed)
        leaveInpectionEvent.Invoke();
    }
    public void OnInspectionZoom(InputAction.CallbackContext context)
    {
        if(context.performed)
        inspectionZoomEvent.Invoke(context.ReadValue<Vector2>());
    }
    public void OnSkipDialogue(InputAction.CallbackContext context)
    {
        if(context.performed)
        skipDialogueEvent.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        pauseEvent.Invoke();
    }

    // UI INPUT
    public void OnNavigate(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.performed)
        resumeEvent.Invoke();
    }
    
    // DIALOGUE INPUT
    public void OnNewaction(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
