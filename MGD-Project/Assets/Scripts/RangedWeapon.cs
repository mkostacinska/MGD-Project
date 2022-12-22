using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5;

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
    private void Update()
    {
        //if the weapon is the ranged weapon, shoot projectiles when the attack button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
            }
        }
    }
}
