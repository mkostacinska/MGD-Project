using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : PickupController
{
    [SerializeField] GameObject labelPrefab;

    //override method so the item goes into player inventory
    new private void Start(){
        text = Instantiate(labelPrefab);
        base.Start();       //get the start() from superclass
    }

    new void Update(){
        rotateObject(); //periodic key movement (rotation + moving up and down)
        checkDistance(); //check the distance to the player to decide whether or not to display the prompt
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
                Destroy(this);     //destroy this script
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}