
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
        InvokeRepeating("resetMovement", 2.0f, 1f); //check every second to avoid instantly resetting before the slime leaves the ground
    }

    /// <summary>
    /// Checks if slime is touching ground
    /// </summary>
    /// <returns> True if touching ground, else false </returns>
    bool IsGrounded() {
        //get distance to ground via raycast
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return false;
        //return true if distance to object below is less than small margin
        //distance changes based on object size, for slime, we can get and subtract the sphere collider's radius
        float subtract = GetComponent<SphereCollider>().radius * transform.localScale.y;
        return (hit.distance - subtract < 0.01);    //as long as the delta is small enough that isGrounded() is false the next frame
    }

    bool CooldownCheck()
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
        if (IsGrounded() && CooldownCheck())
        {
            //jump when grounded
            rb.AddForce(new Vector3(0, jumpAmount, 0), ForceMode.Impulse);
            Vector3 difference = PlayerToFollow.shared.player.transform.position - transform.position;
            //normalize the direction vector
            Vector3 direction = difference.normalized;
            //jump towards player
            rb.AddForce(new Vector3(direction.x, 0, direction.z) * moveAmount, ForceMode.Impulse);
        }
    }

    void resetMovement() {
        if (IsGrounded())
        {
            //reset non-vertical force when not jumping to prevent rolling
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
