using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupController : MonoBehaviour
{
    //needed for the periodical movement of the keys up and down
    [SerializeField] private float heightSpeed = 4f;
    [SerializeField] private float deltaHeight = 0.1f;
    [SerializeField] private float offset = 0.8f;
    protected float spinAngle = 30f;

    //needed to make the 'press E...' text visible within a certain radius
    [SerializeField] protected GameObject player;
    [SerializeField] public GameObject text;

    protected InputActionMap actionMap;
    protected bool keyDown = false;

    /// <summary>
    /// Set the initial position of the pickup label as well as rotation of the pickup itself.
    /// </summary>
    protected void Start()
    {
        // get the actionMap from the input manager
        actionMap = InputManager.getActionMap();

        //set the 'press E...' text position
        UpdateText();
        text.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        text.transform.eulerAngles = text.transform.eulerAngles + new Vector3(45, 0, 0); //set the angle of the text so that it faces the camera

        //setting properties so the object spins properly in rotateObject()
        offset = transform.position.y;
        transform.rotation = Quaternion.Euler(spinAngle, 0, 0);   //sets the localRotation to spinAngle degrees for a fancy spin
    }

    /// <summary>
    /// Update the text to be displayed on the label according to the most recent rebinding.
    /// </summary>
    public void UpdateText() {
        string keyText = actionMap.FindAction("Pickup").bindings[0].effectivePath;
        var pos = keyText.IndexOf('/');
        keyText = keyText.Substring(pos + 1).ToUpper();
        text.GetComponent<TMP_Text>().text = "Press " + keyText + " to collect";
    }

    /// <summary>
    /// Rotates the object, determines whether the label is to be displayed and checks the current inputs.
    /// </summary>
    protected void Update()
    {
        RotateObject(); //periodic key movement (rotation + moving up and down)
        CheckDistance(); //check the distance to the player to decide whether or not to display the prompt
        CheckInputs();
    }

    private void CheckInputs()
    {
        //set the actionMap if it does not exist
        if (actionMap == null) 
        {
            actionMap = InputManager.getActionMap(); 
        }

        if (actionMap.FindAction("Pickup").triggered) {
            OnPickup(); 
        }
    }

    /// <summary>
    /// Acknowledge that the 'Pickup' key has been pressed.
    /// </summary>
    void OnPickup() {
        //acknowledge and reset
        keyDown = true;
        CheckDistance();
        keyDown = false; 
    }

    /// <summary>
    /// Check the distance between the player and the pickup and display the label if neccessary.
    /// </summary>
    void CheckDistance()
    {
        // if the player is within 1.5 units from the key, display the prompt 
        if(Vector3.Distance(transform.position, PlayerToFollow.shared.player.transform.position) <= 1.5f)
        {
            UpdateText();
            text.SetActive(true);
            if(keyDown)
            {
                text.SetActive(false);
                transform.gameObject.SetActive(false); // disable the pickup and increase the total
                keyDown = false; // acknowledge and reset
            }
        }
        else
        {
            text.SetActive(false);
        }
    }

    /// <summary>
    /// Rotate the object around the y axis.
    /// </summary>
    protected void RotateObject()
    {
        //rotate the object around the world y axis
        Vector3 rotation = new Vector3(0, 0.07f, 0);
        transform.eulerAngles = transform.eulerAngles + rotation;

        //make the object move up and down
        Vector3 current = transform.position;
        float newY = Mathf.Sin(Time.time * heightSpeed);       
        transform.position = new Vector3(current.x, (newY * deltaHeight) + offset, current.z); 
    }
}
