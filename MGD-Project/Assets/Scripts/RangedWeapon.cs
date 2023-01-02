using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeapon : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5;
    [SerializeField] private string elementName = "NAME"; //set projectile element here, used for quick testing
    private Element element;

    [SerializeField] private float cooldown = 0.2f;
    [SerializeField] private float cooldownEnd = 0f;
    bool cooldownCheck()
    {
        if (Time.time > cooldownEnd)
        {
            cooldownEnd = Time.time + cooldown; //acknowledge and reset cooldown
            return true;
        }
        return false;
    }

    private bool attackKeyDown = false;
    void OnAttack(InputValue attackValue) {
        if (transform.parent.name == "WeaponHolder") { //only attack if its in weapon holder
            attackKeyDown = true; 
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
            }
        }
    }
    private void Update()
    {
        //if the weapon is the ranged weapon, shoot projectiles when the attack button is pressed
        if (attackKeyDown)
        {
            if (cooldownCheck())
            {
                //shoot at cursor position
                //rotate projectile to look at the current mouse position
                RaycastHit lookHit;
                Vector3 direction;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out lookHit, 100)) //only changes direction if pointed at a surface
                {
                    Vector3 finalPoint = new Vector3(lookHit.point.x, 0, lookHit.point.z);
                    Vector3 difference = finalPoint - transform.position;  //direction is the difference between weapon pos and point pos
                    direction = difference.normalized;
                    direction = new Vector3(direction.x, 0, direction.z);
                }
                else {
                    direction = projectileSpawnPoint.forward;
                }
                var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                //projectile.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
                projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
                projectile.GetComponent<Projectile>().element = this.element;
            }
            attackKeyDown = false; //acknowledge and reset
        }
    }
}
