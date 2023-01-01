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

    //needed to make the 'press E...' text visible within a certain radius
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject text;

    protected void Start()
    {
        //set the 'press E...' text position
        text.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        text.transform.eulerAngles = text.transform.eulerAngles + new Vector3(45, 0, 0); //set the angle of the text so that it faces the camera
    }

    protected void Update()
    {
        rotateObject(); //periodic key movement (rotation + moving up and down)
        checkDistance(); //check the distance to the player to decide whether or not to display the prompt
    }

    protected bool keyDown = false;
    void OnPickup(InputValue value) { 
        keyDown = true;
        checkDistance();
        keyDown = false; //acknowledge and reset
    }

    void checkDistance()
    {
        //if the player is within 1.5 unit from the key, display the prompt 
        if(Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            text.SetActive(true);
            if(keyDown)
                {
                text.SetActive(false);
                transform.gameObject.SetActive(false); //disable the pickup and increase the total
                keyDown = false; //acknowledge and reset
            }
        }
        else
        {
            text.SetActive(false);
        }
    }


    protected void rotateObject()
    {
        //rotate the object around the world y axis
        Vector3 rotation = new Vector3(0, 0.07f, 0);
        transform.eulerAngles = transform.eulerAngles + rotation;

        //make the object move up and down
        Vector3 current = transform.position;
        float newY = Mathf.Sin(Time.time * heightSpeed);
        transform.position = new Vector3(current.x, (newY * deltaHeight) + offset, current.z); //change y to new position (offset by initial Y position!)
    }
}
