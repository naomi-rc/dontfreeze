using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction OpenInventoryEvent = delegate { };
    public event UnityAction InteractEvent = delegate { };

    public bool cursorLockEnabled = true;

    private GameInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();

            gameInput.Gameplay.SetCallbacks(this);
            gameInput.UI.SetCallbacks(this);
        }
    }

    public void EnableGameplayInput()
    {
        gameInput.Gameplay.Enable();
        gameInput.UI.Disable();

        if (cursorLockEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnableUiInput()
    {
        gameInput.Gameplay.Disable();
        gameInput.UI.Enable();

        Cursor.lockState = CursorLockMode.None;
    }

    #region Gameplay actions
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent.Invoke();
        }
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OpenInventoryEvent.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractEvent.Invoke();
        }
    }
    #endregion

    #region UI actions
    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
    }
    #endregion
}