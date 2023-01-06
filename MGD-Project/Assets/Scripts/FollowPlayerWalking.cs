using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWalking : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float range = 1;

    void Update()
    {
        // stop if enemy is within certain range of player (so that it can attack)
        Vector3 difference = PlayerToFollow.shared.player.transform.position - transform.position;
        Vector3 direction = difference.normalized;      //gets the unit vector direction
        transform.forward = new Vector3(direction.x, 0, direction.z);  //enemy rotates to face player (ignoring y axis)

        if (difference.magnitude > range)
        {
            //move to (player.x, original.y, player.z)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerToFollow.shared.player.transform.position.x,transform.position.y, PlayerToFollow.shared.player.transform.position.z), speed * Time.deltaTime); //line from https://www.youtube.com/watch?v=wp8m6xyIPtE modified to ignore player heights
        }
    }
}
