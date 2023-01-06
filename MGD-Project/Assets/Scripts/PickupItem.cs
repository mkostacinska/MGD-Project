using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : PickupController
{
    [SerializeField] GameObject labelPrefab;

    //override method so the item goes into player inventory
    new private void Start(){
        if (player == null) {player = PlayerToFollow.getPlayer(); } //prevents breaking other scenes
        text = Instantiate(labelPrefab);
        base.Start();       //get the start() from superclass

        //if there is an animator, disable it so that item can spin
        if (TryGetComponent(out Animator animator)) { animator.enabled = false; }
        if (TryGetComponent(out BoxCollider collider)) { collider.enabled = false; }

        if (transform.parent != null)
        {
            if (transform.parent.name == "WeaponHolder") {//weapon already picked up
                transform.rotation = Quaternion.identity; //reset the transform from superclass' start
                Destroy(text);
                Destroy(this);
            } 
        }
    }

    new void Update(){
        RotateObject(); //periodic key movement (rotation + moving up and down)
        checkDistance(); //check the distance to the player to decide whether or not to display the prompt

        checkInputs();  //method is in superclass
    }
    private void checkInputs()
    {
        if (actionMap == null) { actionMap = InputManager.getActionMap(); } //set the actionMap if it does not exist
        if (actionMap.FindAction("Pickup").triggered) { OnPickup(); }
    }

    void OnPickup()
    {
        if (text)
        {
            keyDown = true;
            checkDistance();
            keyDown = false; //acknowledge and reset
        }
    }

    void checkDistance()
    {
        //if the player is within 1.5 unit from the item, display the prompt 
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            text.SetActive(true);
            //if (Input.GetKeyDown(KeyCode.E))
            if (keyDown == true)
            {
                //if inventory is full, swap with active weapon first
                Inventory inventory = GameObject.Find("Hotbar Panel").GetComponent<Inventory>();
                if (inventory.items.Count >= inventory.maxSlots)
                {
                    GameObject activeItem = inventory.items[inventory.scrollPos];

                    activeItem.transform.SetParent(this.transform.parent);
                    activeItem.transform.localPosition = this.transform.localPosition;
                    activeItem.transform.localRotation = this.transform.localRotation;

                    //copying script to new item
                    var script = activeItem.AddComponent<PickupItem>();
                    script.player = this.player;
                    script.labelPrefab = this.labelPrefab;
                }

                Destroy(text);
                transform.SetParent(player.transform.GetChild(0)); //moves the object to the weapon holder
                //reset transform
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                //if there is an animator or collider, re-enable it
                if (TryGetComponent(out Animator animator)) { animator.enabled = true; }
                if (TryGetComponent(out BoxCollider collider)) { collider.enabled = true; }
                inventory.refreshInventory();

                Destroy(this);     //destroy this script
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}