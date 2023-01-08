using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// rotates the healthbar canvas of enemies or any other canvas to face the camera/front
/// </summary>
public class CanvasForwardRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.parent)   //this script is put in a Canvas which should be a child of a physical gameobject
        {
            //difference between canvas position (assuming parent is an object) and camera position
            Vector3 difference = transform.parent.position - Camera.main.transform.position;
            Vector3 direction = difference.normalized;      //gets the unit vector direction
            transform.forward = new Vector3(0, direction.y, direction.z); //face the camera
        }
        else {  //backup if somethng has gone wrong
            transform.forward = new Vector3(0,0,0); //face the front
        }
    }
}
