using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : PickupController
{
    [SerializeField] GameObject labelPrefab;

    //override method so the item goes into player inventory
    new private void Start(){
        text = Instantiate(labelPrefab);
        base.Start();       //get the start() from superclass

        //if there is an animator, disable it so that item can spin
        if (TryGetComponent(out Animator animator)) { animator.enabled = false; }

        if (transform.parent != null)
        {
            if (transform.parent.name == "WeaponHolder") {//weapon already picked up
                Destroy(text);
                Destroy(this);
            } 
        }
    }

    new void Update(){
        rotateObject(); //periodic key movement (rotation + moving up and down)
        checkDistance(); //check the distance to the player to decide whether or not to display the prompt
    }

    void OnPickup(InputValue value)
    {
        keyDown = true;
        checkDistance();
        keyDown = false; //acknowledge and reset
    }

    void checkDistance()
    {
        //if the player is within 1.5 unit from the item, display the prompt 
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            text.SetActive(true);
            if (keyDown)
            {
                Destroy(text);
                transform.SetParent(player.transform.GetChild(0)); //moves the object to the weapon holder
                //reset transform
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                //if there is an animator, re-enable it
                if (TryGetComponent(out Animator animator)) { animator.enabled = true; }

                Destroy(this);     //destroy this script
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}