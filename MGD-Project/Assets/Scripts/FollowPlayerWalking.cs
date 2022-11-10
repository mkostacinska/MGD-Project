using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWalking : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float range = 1;

    // Update is called once per frame
    void Update()
    {
        // stop if enemy is within certain range of player (so that it can attack)
        Vector3 difference = Player.transform.position - transform.position;
        if (difference.magnitude > range)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime); //line from https://www.youtube.com/watch?v=wp8m6xyIPtE
        }
    }
}
