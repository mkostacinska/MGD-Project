using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// set the camera position to follow the player (with offset on z to reflect the angle)
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        //set the camera position to follow the player (with offset on z to reflect the angle)
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 5);
    }
}
