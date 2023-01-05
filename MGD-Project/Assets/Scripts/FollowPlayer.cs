using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float jumpAmount = 1;
    [SerializeField] private float moveAmount = 0.5f;

    [SerializeField] private float cooldown = 1;
    [SerializeField] private float canJump = 0f;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool IsGrounded() {
        //get distance to ground via raycast
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return false;
        //return true if distance to object below is less than small margin
        return (hit.distance < 0.3);
    }

    bool cooldownCheck()
    {
        if (Time.time > canJump)
        {
            canJump = Time.time + cooldown; //acknowledge jump and reset cooldown
            return true;
        }
        return false;
    }


    void Update()
    {
        //check if entity is grounded, then jump
        if (IsGrounded() && cooldownCheck())
        {
            //jump when grounded
            rb.AddForce(new Vector3(0, jumpAmount, 0), ForceMode.Impulse);

            Vector3 difference = PlayerToFollow.shared.player.transform.position - transform.position;
            //normalize the direction vector
            Vector3 direction = difference.normalized;      
            //jump towards player
            rb.AddForce(new Vector3(direction.x, 0, direction.z) * moveAmount, ForceMode.Impulse);
        }
        else if (IsGrounded()){
            //reset non-vertical force when not jumping to prevent rolling
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
