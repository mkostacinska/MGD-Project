using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//this is the inventory
public class Inventory : MonoBehaviour
{
    //public Transform original;
    //public Transform hotbarSlot;
    //public Transform currentSlot;
    //public Transform[] Items;
    public int scrollPos;

    [SerializeField] private int slots = 2;

    void OnSlot0(InputValue value) { scrollPos = 0; }
    void OnSlot1(InputValue value) { scrollPos = 1; }

    void Update()
    {
        //change the inventory slot on mouse scrollbar movement:
        if (Input.mouseScrollDelta.y >= 1)
        {
            scrollPos = (scrollPos + 1) % slots; //scroll loops back if over number of slots
        }
        else if (Input.mouseScrollDelta.y <= -1)
        {
            scrollPos--;
            if (scrollPos < 0)
            {
                scrollPos = slots - 1;
            }
        }
        Selected();
    }

    //highlight the currently selected slot
    void Selected()
    {
        //make the slot active and disable all the other slots
        int i = 0;
        foreach (Transform item in transform) // compare each child to current transform
        {
            if (i == scrollPos)
            {
                item.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                item.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
    }

}
