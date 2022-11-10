using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWalking : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float jumpAmount = 1;
    [SerializeField] private float moveAmount = 0.5f;
    [SerializeField] private float range = 1;
    public Rigidbody rb;

    private float distToGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool IsGrounded() {
        //get distance to ground via raycast
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return false;
        distToGround = hit.distance;
        //return true if distance to object below is less than small margin
        return (hit.distance < 0.3);
    }

    [SerializeField] private float cooldown = 1;
    [SerializeField] private float canJump = 0f;
    bool cooldownCheck()
    {
        if (Time.time > canJump)
        {
            canJump = Time.time + cooldown; //acknowledge jump and reset cooldown
            return true;
        }
        return false;
    }


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
