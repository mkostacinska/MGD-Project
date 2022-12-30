using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour {

    //public int selectedWeapon = 0; //initially, the first element of the inventory is selected
    public int selectedWeapon;
    int previousSelectedWeapon;
    private Inventory inventory;
    
    void Start()
    {
        inventory = GameObject.Find("Hotbar Panel").GetComponent<Inventory>();
        selectedWeapon = inventory.scrollPos;
        SelectWeapon();
    }

    private List<GameObject> items = new List<GameObject>(); //Arraylist of items
    void Update()
    {
        //for every item the player is holding: send it to the inventory
        if (transform.childCount != this.items.Count)
        {
            items.Clear();
            foreach (Transform item in transform)
            {
                items.Add(item.gameObject);
            }
            inventory.items = this.items; //link to inventory
        }

        selectedWeapon = inventory.scrollPos;

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon ()
    {
        //make the weapon that is to be used active in the scene and disable all the other weapons
        int i = 0;
        foreach (Transform weapon in transform) // compare each child to current transform
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                //stop the animation before switching
                if (weapon.gameObject.TryGetComponent(out Animator animator)) {
                    //print("animation playing " + animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                    animator.StopPlayback();
                    weapon.gameObject.transform.localRotation = Quaternion.identity;
                    weapon.gameObject.transform.localPosition = Vector3.zero;
                }
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        previousSelectedWeapon = selectedWeapon;
    }
}
