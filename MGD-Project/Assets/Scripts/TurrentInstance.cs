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
    private GameObject Player = PlayerToFollow.shared.player;
    public HealthBar healthBar;

    //projectile variables
    public GameObject projectilePrefab;
    public float projectileSpeed = 2;

    void Start()
    {
        thisTurret = new NPC(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
        thisTurret.Start();
    }

    bool cooldownCheck()
    {
        if (Time.time > readyTime)
        {
            readyTime = Time.time + cooldown; //acknowledge and reset cooldown
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        thisTurret.Update();
        //rotate the turret enemy to face the player
        Vector3 difference = Player.transform.position - transform.position;
        Vector3 direction = difference.normalized;      //gets the unit vector direction
        transform.forward = direction;


        //if player is within range, fire projectile
        if (cooldownCheck())
        {
            //create projectile with offset so its in front of the object not in the object
            var projectile = Instantiate(projectilePrefab, transform.position +(transform.forward*0.2f), transform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }

        //set healthbar to health
        healthBar.Sethealth(thisTurret.getHealth());

        if (thisTurret.getHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }
}
