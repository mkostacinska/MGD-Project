using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HotbarSlots : MonoBehaviour
{
    public Transform original;
    public Transform hotbarSlot;
    public Transform currentSlot;
    public Transform[] Items;
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
            if(scrollPos < 0)
            {
                scrollPos = slots-1;
            }
        }
        Selected();
    }

    //highlight the currently selected slot
    void Selected()
    {
       if (currentSlot.name == "Hotbar Slot " + scrollPos)
        {
            currentSlot.GetComponent<Image>().color = Color.grey;
        } else
        {
            currentSlot.GetComponent<Image>().color = Color.black;
        }
    }

}
