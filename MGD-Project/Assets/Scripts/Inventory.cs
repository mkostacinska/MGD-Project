using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

//this is the inventory
public class Inventory : MonoBehaviour
{
    //public Transform original;
    //public Transform hotbarSlot;
    //public Transform currentSlot;
    //public Transform[] Items;
    public int scrollPos;

    public int maxSlots = 5;
    private int slots;
    public List<GameObject> items; //Arraylist of items

    private void Start() {
        slots = transform.childCount;   //set the initial number of slots
        matchInventory();
        Selected();
    }

    void OnNextSlot() { scrollPos = (scrollPos + 1) % slots; Selected(); } //scroll loops back if over number of slots
    void OnPreviousSlot()
    {
        scrollPos--;
        if (scrollPos < 0)
        {
            scrollPos = slots - 1;
        }
        Selected();
    }
    void OnSlot0() { scrollPos = 0; Selected(); }
    void OnSlot1() { scrollPos = 1; Selected(); }
    void OnSlot2() { scrollPos = 2; Selected(); }
    void OnScroll(InputValue value)
    {
        //change the inventory slot on mouse scrollbar movement:
        if (value.Get<float>() < 0)
        {
            scrollPos = (scrollPos + 1) % slots; //scroll loops back if over number of slots
        }
        else {
            scrollPos--;
            if (scrollPos < 0)
            {
                scrollPos = slots - 1;
            }
        }
        Selected();
    }

    public void refreshInventory() {
        GameObject.Find("WeaponHolder").GetComponent<WeaponSwitching>().refreshItems();
    }

    private void Update()
    {
        if (slots != items.Count) { matchInventory(); }
    }

    [SerializeField] GameObject prefab;
    public void matchInventory() {
        //change the width of the HUD to match number of slots
        //formula is +13 to width per item
        GetComponent<RectTransform>().sizeDelta = new Vector2(1 + items.Count * 13, GetComponent<RectTransform>().sizeDelta.y);
        for (; slots < items.Count; slots++)
        {
            //add new item with correct text
            //copy first slot and move to correct location
            var copy = Instantiate(transform.GetChild(0).gameObject, transform);
            copy.GetComponent<Image>().color = Color.black;
            copy.GetComponent<RectTransform>().localPosition = new Vector2(7 + slots * 13, copy.GetComponent<RectTransform>().localPosition.y);
        }

        //items[] is set by WeaponSwitching.cs but if this script's Start() is faster, items[] is not set fast enough
        if (items.Count > 0)
        {
            int i = 0;
            foreach (Transform item in transform)
            {
                item.GetComponentInChildren<TMP_Text>().text = items[i].name;
                i++;
            }
        }
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
