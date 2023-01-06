using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//there is a Unity Engine Input System bug where some gameobjects' actions are not triggered
//this bug only occurs in the build and not the editor and which object's actions break is not consistent; changes each build
//I am refactoring to make everything that uses input to get PlayerInput from this script avoid this bug
public class InputManager : MonoBehaviour
{
    public InputActionMap actionMap;


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

    /*
    to get a call from the player input:
    InputActionMap actionMap

    if (actionMap == null) { actionMap = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInput>().currentActionMap; }  //set the actionMap if it does not exist
    if (actionMap == null) { actionMap = npuInputManager.getActionMap(); } //set the actionMap if it does not exist


    To get the input values: examples:

    if (actionMap.FindAction("Move").ReadValue<Vector2>() != Vector2.zero) { OnMove(); }
    if (actionMap.FindAction("Button").triggered) { OnButton(); }
    if (actionMap.FindAction("Move").WasPressedThisFrame() || actionMap.FindAction("Move").WasReleasedThisFrame()) //this one is jittery and you cant move diagonally
    */
}
