using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretProjectile : MonoBehaviour
{
    private GameObject Player = PlayerToFollow.shared.player;
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life); //destroy the projectile after 3 seconds
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == Player.name)    //for now, it searches by gameObject name
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
