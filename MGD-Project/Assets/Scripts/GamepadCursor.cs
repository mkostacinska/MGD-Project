using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

/// <summary>
/// Creates a virtual mouse for the gamepad to control and move, for menus
/// </summary>
public class GamepadCursor : MonoBehaviour     //Modified version of Virtual Mouse from: https://www.youtube.com/watch?v=Y3WNwl1ObC8
{
    private Mouse virtualMouse;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    Vector2 newPosition;
    private float cursorSpeed = 300f;

    private bool previousMouseState;
    private Camera mainCamera;

    private void Starting()
    {
        mainCamera = Camera.main;
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        //pair the virtual mouse device with the player input
        InputUser.PerformPairingWithDevice(virtualMouse, InputManager.getInputManager().GetComponent<PlayerInput>().user);

        if (newPosition != null) //return to last known position before being disabled
        { InputState.Change(virtualMouse.position, newPosition); }
    }

    private void OnEnable()
    {
        Starting();
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
        InputSystem.RemoveDevice(virtualMouse);     //avoids creating many virtual mice
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        // Delta
        Vector2 deltaValue = Gamepad.current.rightStick.ReadValue();    //set right stick to virtual mouse position

        deltaValue *= cursorSpeed * Time.fixedDeltaTime;    //not affected by time scale so it still works when game is paused

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);


        virtualMouse.CopyState<MouseState>(out var mouseState);
        mouseState.scroll = Gamepad.current.leftStick.ReadValue() * cursorSpeed * Time.fixedDeltaTime;  //set left stick to virtual scroll wheel
        InputState.Change(virtualMouse, mouseState);

        AnchorCursor(newPosition);

        if (InputManager.getInputManager().inputMode != "Gamepad") { gameObject.SetActive(false); }     //automatically hide the virtual mouse when gamepad is not the input mode
    }

    //map screen coord to rect transform coord
    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
