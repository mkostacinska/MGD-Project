using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

//there is a Unity Engine Input System bug where some gameobjects' actions are not triggered
//this bug only occurs in the build and not the editor and which object's actions break is not consistent; changes each build
//I am refactoring to make everything that uses input to get PlayerInput from this script avoid this bug
//it seems to have something to do with the order the PlayerInputs are enabled

//also another bug where no inputs work when there is a PlayerInput variable in this script
//which means that GetComponent<PlayerInput>() has to be used each time


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
public class InputManager : MonoBehaviour
{
    //Data Access Properties
    public InputActionMap actionMap;
    public string inputMode = "";

    //Gamepad Mouse Properties
    private bool gamepadMouseMode = false;
    public int gamepadCursorSensistivity = 5;

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

    private void Update()
    {
        if (actionMap == null) { actionMap = GetComponent<PlayerInput>().currentActionMap; }

        //toggles gamepadMouseMode and moves a virtual mouse using a controller
        if (actionMap.FindAction("MouseMode").triggered)
        { 
            gamepadMouseMode = !gamepadMouseMode;
            transform.GetChild(0).gameObject.SetActive(gamepadMouseMode);
        }
        

        inputMode = GetComponent<PlayerInput>().currentControlScheme;   //get which control scheme is being used
        if (actionMap.FindAction("SwitchControlScheme").triggered && Time.timeScale == 1f)    //for some reason everything breaks when paused
        {    //switch control scheme
            switch (inputMode)
            {
                case null:
                    if (Keyboard.current != null)
                    {
                        GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
                        gamepadMouseMode = false;       //turn of mouse mode if swapping control schemes
                    }
                    break;

                case "Keyboard&Mouse":
                    if (Gamepad.current != null)
                    { GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.current); }
                    break;

                case "Gamepad":
                    {
                        GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
                        gamepadMouseMode = false;       //turn of mouse mode if swapping control schemes
                    }
                    break;
            }
        }
    }

/// <summary>
/// Converts string of button name to button based on type of controller
/// e.g. "buttonNorth" returns "Triangle" on a Playstation Controller
/// </summary>
/// <param name="gamepad"></param>
/// <param name="button"></param>
/// <returns></returns>
public static string controllerButtonToString(Gamepad gamepad, string button)
    { //https://forum.unity.com/threads/how-can-i-detect-the-gamepad-model-like-xboxone-ps4-etc.753758/
        if (gamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
        {
            switch (button)
            {
                case "buttonNorth":
                    return "Triangle";
                case "buttonEast":
                    return "Circle";
                case "buttonSouth":
                    return "X";
                case "buttonWest":
                    return "Square";
            }
        }
        else
        {
            switch (button)
            {
                case "buttonNorth":
                    return "Y";
                case "buttonEast":
                    return "B";
                case "buttonSouth":
                    return "A";
                case "buttonWest":
                    return "X";
            }
        }
        return button;  //incase it gets to the end
    }

    /// <summary>
    /// Helper method to convert path to key name
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string getKeyFromPath(string path) {
        var pos = path.IndexOf('/');
        return path.Substring(pos + 1);
    }
}
