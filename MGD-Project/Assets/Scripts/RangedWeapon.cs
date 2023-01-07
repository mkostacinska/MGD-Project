using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeapon : MonoBehaviour
{
    // Projectile Properties
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5;

    // Weapon Element Properties
    [SerializeField] public string elementName = "NAME"; // set projectile element here, used for quick testing
    private Element element;

    // Cooldown Properties
    [SerializeField] private float cooldown = 0.2f;
    [SerializeField] private float cooldownEnd = 0f;

    // Action maps
    InputActionMap actionMap;

    /// <summary>
    /// Check if the cooldown period has passed and resets the cooldown.
    /// </summary>
    /// <returns> true if cooldown has passed, else false.</returns>
    bool CooldownCheck()
    {
        if (Time.time > cooldownEnd)
        {
            cooldownEnd = Time.time + cooldown; // acknowledge and reset cooldown
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check if the action map is set approprtiately and call the relevant function when inputs are triggered
    /// </summary>
    /// <returns> true if cooldown has passed, else false.</returns>
    protected void CheckInputs()
    {
        //set the actionMap if it does not exist
        if (actionMap == null) 
        {
            actionMap = InputManager.getActionMap(); 
        }

        if (actionMap.FindAction("Attack").triggered) 
        { 
            OnAttack();
        }       //trigger attack
    }

    /// <summary>
    /// Shoot projectile gameObjects (respecting a cooldown period).
    /// </summary>
    void OnAttack() {
        if (transform.parent != null) {
            //only attack if its in weapon holder
            if (transform.parent.name == "WeaponHolder") { 
                if (CooldownCheck())
                {

                    //shoot at cursor position
                    //rotate projectile to look at the current mouse position
                    RaycastHit lookHit;
                    Vector3 direction;
                    if (InputManager.getInputMode() == "Gamepad") //direction to be player direction
                    {
                        direction = projectileSpawnPoint.forward;
                    }
                    else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out lookHit, 100)) //only changes direction if pointed at a surface
                    {
                        Vector3 finalPoint = new Vector3(lookHit.point.x, 0, lookHit.point.z);  //gets the x and y of the raycasted position
                        Vector3 difference = finalPoint - transform.position;  //direction is the difference between weapon pos and point pos
                        direction = difference.normalized;
                        direction = new Vector3(direction.x, 0, direction.z);
                    }
                    else //a backup direction of raycast does not work e.g when mouse is above the void
                    {
                        direction = projectileSpawnPoint.forward;
                    }
                    //spawn projectile and set it's properties
                    var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                    projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
                    projectile.GetComponent<Projectile>().element = this.element;
                    projectile.transform.GetChild(0).GetComponent<Renderer>().material.color = this.element.getColour();
                }
            }
        }
    }

    private void Start()
    {
        element = new Elements().getElement(elementName);
        gameObject.name = (elementName + " Staff").ToUpper();

        //change staff colour to match element
        Material[] materials = transform.GetComponentInChildren<MeshRenderer>().materials;
        foreach (Material m in materials) {
            if (m.name.StartsWith("Crystal")) { //StartsWith() is used because "(instance)" is added by unity
                m.color = element.getColour();
                m.EnableKeyword("_EMISSION");   //https://stackoverflow.com/a/33671094
                m.SetColor("_EmissionColor", element.getColour());
            }
        }
    }
    private void Update()
    {
        CheckInputs();
    }
}
