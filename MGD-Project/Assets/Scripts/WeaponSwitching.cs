using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour {

    //public int selectedWeapon = 0; //initially, the first element of the inventory is selected
    public int selectedWeapon;
    int previousSelectedWeapon;
    
    void Start()
    {
        selectedWeapon = GameObject.Find("Hotbar Panel").GetComponent<Inventory>().scrollPos;
        SelectWeapon();
    }

    void Update()
    {
        selectedWeapon = GameObject.Find("Hotbar Panel").GetComponent<Inventory>().scrollPos;

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
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        previousSelectedWeapon = selectedWeapon;
    }
}
