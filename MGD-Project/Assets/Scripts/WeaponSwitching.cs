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
