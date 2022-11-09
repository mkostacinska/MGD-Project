using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContollerPrototype : MonoBehaviour
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
    [SerializeField] private GameObject weapon;

    private Vector2 movementVector = new Vector2(0, 0);     //initialise movement to 0,0
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotatePlayer();
        //Move();
        transform.position += walkSpeed * movementVector.y * new Vector3(0, 0, 1) * Time.deltaTime; //up down
        transform.position += walkSpeed * movementVector.x * new Vector3(1, 0, 0) * Time.deltaTime; //left right
    }

    private bool IsGrounded()
    {
        //get distance to ground via raycast
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return false;
        //return true if distance to object below is less than small margin
        return (hit.distance-1 < 0.1);
    }

 
    void OnMove(InputValue movementValue) //from lecture video
    {
        movementVector = movementValue.Get<Vector2>();      //gets a vector2 in form (x, z)
        //Debug.Log(movementVector);
    }

    void OnJump(InputValue jumpValue)   //triggers when jump button is pressed
    {
        //Debug.Log(jumpValue.isPressed);
        if (IsGrounded()){
            rigidbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);       //adds an impulse force to cause the jump
        }
    }

    void OnAttack(InputValue attackValue)
    {
        //animator.SetTrigger("Attack");
        //GetComponent<Animator>().SetTrigger("Attack");
        weapon.GetComponent<Animator>().SetTrigger("Attack");
    }

    private void RotatePlayer()
    {
        //rotate player to look at the current mouse position
        RaycastHit lookHit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out lookHit, 100)) //only changes direction if pointed at a surface
        {
            Vector3 finalPoint = new Vector3(lookHit.point.x, 0, lookHit.point.z);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalPoint), 10f * Time.deltaTime);
            Vector3 difference = finalPoint - transform.position;  //direction is the difference between player pos and point pos
            Vector3 direction = difference.normalized;
            transform.forward = new Vector3(direction.x, 0, direction.z);
        }
    }

}
