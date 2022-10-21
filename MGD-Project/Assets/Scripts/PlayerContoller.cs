using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    //Serialized so that it can still be seen/modified from the inspector while keeping the scope minimal
    [SerializeField] private float movementSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private Vector3 velocity;
    public Camera camera;

    private Animator animator;
    private Rigidbody rigidbody;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotatePlayer();
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask) || Physics.CheckSphere(transform.position, groundDistance, enemyMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        if (isGrounded)
        {
            if (moveZ != 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveZ != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveZ == 0)
            {
                Idle();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        transform.position += movementSpeed * moveZ * transform.forward * Time.deltaTime;

    }

    private void Idle()
    {
        //when idle, queue idle animation
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        movementSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        movementSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        rigidbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void RotatePlayer()
    {
        //rotate player to look at the current mouse position
        RaycastHit lookHit;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out lookHit, 100);

        Vector3 finalPoint = new Vector3(lookHit.point.x, 0, lookHit.point.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalPoint), 10f * Time.deltaTime);
    }
}
