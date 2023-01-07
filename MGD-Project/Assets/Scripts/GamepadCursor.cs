using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour     //Modified version of Virtual Mouse from: https://www.youtube.com/watch?v=Y3WNwl1ObC8
{
    private Mouse virtualMouse;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    private float cursorSpeed = 300f;

    private bool previousMouseState;
    private Camera mainCamera;

    private void Start()
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

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);     //set the starting position of the cursor
        }
    }

    private void OnEnable()
    {
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
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
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);


        virtualMouse.CopyState<MouseState>(out var mouseState);
        mouseState.scroll = Gamepad.current.leftStick.ReadValue() * cursorSpeed * Time.fixedDeltaTime;  //set left stick to virtual scroll wheel
        InputState.Change(virtualMouse, mouseState);

        AnchorCursor(newPosition);
    }

    //map screen coord to rect transform coord
    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
