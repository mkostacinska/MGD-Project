using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretProjectile : MonoBehaviour
{
    public float life = 3;

    /// <summary>
    /// Ensure that the projectile is destroyed after passing it's lifespan.
    /// </summary>
    void Awake()
    {
        Destroy(gameObject, life); //destroy the projectile after 3 seconds
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == PlayerToFollow.shared.player.name)   
        {    
            // deal damage to the player on collision
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            p.dealDamage("Plant", 1);

            // destroy the projectile after colliding with player
            Destroy(gameObject); 
        }
    }
}
