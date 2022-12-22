using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is code to turn the healthbar canvas of enemies or any other canvas to face the camera/front
public class CanvasForwardRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.forward = new Vector3(0,0,0); //face the front

        /*
        //difference between canvas position (assuming parent is an object) and camera position
        Vector3 difference = transform.parent.position - Camera.main.transform.position;
        Vector3 direction = difference.normalized;      //gets the unit vector direction
        transform.forward = new Vector3(0, direction.y, 0); //face the camera
        */
    }
}
