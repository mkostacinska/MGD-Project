using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretProjectile : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life); //destroy the projectile after 3 seconds
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerPrototype")    //for now, it searches by gameObject name
        {    
            //if the projectile collides with player
            MonoBehaviour.print("hit player");
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            //p.setHealth(p.getHealth() - 1); //decrease the player's health
            p.dealDamage("Turret", 1);
            Destroy(gameObject); //breaks after colliding with player
        }
    }
}
