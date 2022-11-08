using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set the camera position to follow the player (with offset on z to reflect the angle)
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 5);
    }
}
