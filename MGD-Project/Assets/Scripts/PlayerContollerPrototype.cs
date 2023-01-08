using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContollerPrototype : MonoBehaviour
{
    //Serialized so that it can still be seen/modified from the inspector while keeping the scope minimal
    [SerializeField] private float movementSpeed;
    [SerializeField] public float walkSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private new Rigidbody rigidbody;
    [SerializeField] private GameObject weapon;

    private Vector2 movementVector = new Vector2(0, 0);     //initialise movement to 0,0
    InputActionMap actionMap;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        checkInputs();
        RotatePlayer();

        if (!IsColliding(movementVector, 0.01f))
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);       //resets velocity without affecting gravity or jump
            transform.position += walkSpeed * movementVector.y * Vector3.forward * Time.deltaTime; //up down 
            transform.position += walkSpeed * movementVector.x * Vector3.right * Time.deltaTime; //left right
        }
        else //backup method to prevent vibrations when player runs into an object
        {
            Vector3 move = walkSpeed * movementVector;
            rigidbody.velocity = new Vector3(move.x, rigidbody.velocity.y, move.y); //changes velocity without affecting gravity or jump
        }
    }

    private bool IsGrounded() { return IsColliding(Vector3.down, 0.1f); }
    private bool IsColliding(Vector3 direction, float margin = 0.1f)
    {
        //get distance to object via raycast
        if (!Physics.Raycast(transform.position, direction, out var hit)) return false;
        //return true if distance to object is less than small margin
        print(hit.distance - 1);
        return (hit.distance-1 < margin);
    }

    private void checkInputs() {
        if (actionMap == null) { actionMap = InputManager.getActionMap(); } //set the actionMap if it does not exist
        if (actionMap.FindAction("Move").ReadValue<Vector2>() != Vector2.zero)  //check for OnMove()
        {
            OnMove(actionMap.FindAction("Move").ReadValue<Vector2>());
        }
        else { OnMove(Vector2.zero); }  //this just resets the movement
        if (actionMap.FindAction("Jump").triggered) { OnJump(); }
    }

    void OnMove(InputValue movementValue)   //legacy method, if this object has Player Input
    {
        movementVector = movementValue.Get<Vector2>();//gets a vector2 in form (x, z)
    }

    void OnMove(Vector2 movement)
    {
        movementVector = movement; //gets a vector2 in form (x, z)
    }

    void OnJump()   //triggers when jump button is pressed
    {
        if (IsGrounded()){
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);       //adds an impulse force to cause the jump
        }
    }

    private void RotatePlayer()
    {

        if (InputManager.getInputMode() == "Gamepad") { //rotate player to controller direction
            Vector2 direction = actionMap.FindAction("Direction").ReadValue<Vector2>();
            if (direction != Vector2.zero && direction.sqrMagnitude > 0.6) {       //magnitude check prevents direction change from controller rebound from flicking the RStick
                transform.forward = new Vector3(direction.x, 0, direction.y);
            }
        }
        else                    //rotate player to look at the current mouse position if input mode is mouse and keyboard
        {
            RaycastHit lookHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out lookHit, 100)) //only changes direction if pointed at a surface
            {
                Vector3 finalPoint = new Vector3(lookHit.point.x, 0, lookHit.point.z);
                Vector3 difference = finalPoint - transform.position;  //direction is the difference between player pos and point pos
                Vector3 direction = difference.normalized;
                transform.forward = new Vector3(direction.x, 0, direction.z);
            }
        }

    }

}
