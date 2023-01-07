using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

//there is a Unity Engine Input System bug where some gameobjects' actions are not triggered
//this bug only occurs in the build and not the editor and which object's actions break is not consistent; changes each build
//I am refactoring to make everything that uses input to get PlayerInput from this script avoid this bug

//also another bug where no inputs work when there is a PlayerInput variable in this script
//which means that GetComponent<PlayerInput>() has to be used each time
public class InputManager : MonoBehaviour
{
    public InputActionMap actionMap;
    public int gamepadCursorSensistivity = 5;
    public string inputMode = "";

    void Awake()
    {
        //if there is already a source, delete this one: prevents duplicates whenever the menu loads
        if (GameObject.FindGameObjectsWithTag("InputManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject); //makes sure player input is persistent across scenes
        actionMap = GetComponent<PlayerInput>().currentActionMap;
    }

    public static InputActionMap getActionMap()
    {
        return GameObject.FindGameObjectsWithTag("InputManager")[0].GetComponent<InputManager>().actionMap;
    }

    public static string getInputMode()
    {
        return GameObject.FindGameObjectsWithTag("InputManager")[0].GetComponent<InputManager>().inputMode;
    }

    public static InputManager getInputManager()
    {
        return GameObject.FindGameObjectsWithTag("InputManager")[0].GetComponent<InputManager>();
    }

    /*
    to get a call from the player input:
    InputActionMap actionMap

    if (actionMap == null) { actionMap = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInput>().currentActionMap; }  //set the actionMap if it does not exist
    if (actionMap == null) { actionMap = InputManager.getActionMap(); } //set the actionMap if it does not exist


    To get the input values: examples:

    if (actionMap.FindAction("Move").ReadValue<Vector2>() != Vector2.zero) { OnMove(); }
    if (actionMap.FindAction("Button").triggered) { OnButton(); }
    if (actionMap.FindAction("Move").WasPressedThisFrame() || actionMap.FindAction("Move").WasReleasedThisFrame()) //this one is jittery and you cant move diagonally
    */

    private void Start()
    {
        foreach (Gamepad controller in Gamepad.all) {
            Debug.Log(controller);
        }
    }

    private void Update()
    {
        inputMode = GetComponent<PlayerInput>().currentControlScheme;   //get if it is mouse and keyboard or gamepad

        //moves the cursor using a controller
        //if (actionMap.FindAction("Direction").ReadValue<Vector2>() != Vector2.zero) {
        //    onDirection(actionMap.FindAction("Direction").ReadValue<Vector2>()); }

        if (actionMap.FindAction("SwitchControlScheme").triggered) {
            switch (inputMode)
            {
                case null:
                    GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
                    break;

                case "Keyboard&Mouse":
                    GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                    break;

                case "Gamepad":
                    GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
                    break;
            }
        }
        //if (actionMap.FindAction("Click").triggered) { OnClick(); }
    }

    //Code for gamepad controller
    //vector is in the range [(-1,-1), (1, 1)]
    //WarpCursorPosition only works on integer pixel so its actually floored https://stackoverflow.com/a/69196901
    void onDirection(Vector2 direction) {
        Vector2 move = Mouse.current.position.ReadValue() + direction * gamepadCursorSensistivity;
        move = new Vector2(Mathf.Round(move.x), Mathf.Round(move.y));       //round instead of floor for more accuracy
        Mouse.current.WarpCursorPosition(move);
    }

    /*
    private Mouse virtualMouse;
    void OnClick() {
        print("hello");
        if (virtualMouse == null) {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else {
            InputSystem.AddDevice(virtualMouse);
        }

        virtualMouse.WarpCursorPosition(Mouse.current.position.ReadValue());
        print(virtualMouse.position.ReadValue());
    }
    */
}
