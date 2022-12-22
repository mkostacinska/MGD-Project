using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour {

    public int selectedWeapon = 0; //initially, the first element of the inventory is selected
    int previousSelectedWeapon;

    void Start()
    {
        SelectWeapon();
    }

    void OnSlot0(InputValue value) { selectedWeapon = 0; }
    void OnSlot1(InputValue value) { selectedWeapon = 1; }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //move to the next weapon (modular)
            selectedWeapon = (selectedWeapon + 1) % transform.childCount;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

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
