using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;
    [SerializeField] private float cooldown = 4;
    [SerializeField] private float readyTime = 0f;

    public NPC thisTurret;
    public HealthBar healthBar;

    //projectile variables
    public GameObject projectilePrefab;
    public float projectileSpeed = 2;

    /// <summary>
    /// Initialise the turret instance.
    /// </summary>
    void Start()
    {
        thisTurret = new NPC(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
        thisTurret.Start();
    }

    /// <summary>
    /// Check whether the cooldown period has passed and new projectile can be instantiated.
    /// </summary>
    bool CooldownCheck()
    {
        if (Time.time > readyTime)
        {
            readyTime = Time.time + cooldown; //acknowledge and reset cooldown
            return true;
        }
        return false;
    }

    /// <summary>
    /// Periodically spawn the projectiles shot by the turret, as well as ensure that the displayed information about the turret is up to date.
    /// </summary>
    void Update()
    {
        thisTurret.Update();

        // rotate the turret enemy to face the player
        Vector3 difference = PlayerToFollow.shared.player.transform.position - transform.position;
        // gets the unit vector direction
        Vector3 direction = difference.normalized;      
        transform.forward = direction;


        // if player is within range, fire projectile
        if (CooldownCheck())
        {
            // create projectile with offset so its in front of the object not in the object
            var projectile = Instantiate(projectilePrefab, transform.position +(transform.forward*0.2f), transform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }

        // set healthbar to health
        healthBar.Sethealth(thisTurret.getHealth());

        if (thisTurret.getHealth() <= 0 || gameObject.transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
}
