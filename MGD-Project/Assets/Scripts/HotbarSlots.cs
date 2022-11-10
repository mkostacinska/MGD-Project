using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlots : MonoBehaviour
{
    public Transform original;
    public Transform hotbarSlot;
    public Transform currentSlot;
    public Transform[] Items;
    public int scrollPos;

    [SerializeField] private int slots = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
     {
        //print(scrollPos);
        //print(currentSlot.name);
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
