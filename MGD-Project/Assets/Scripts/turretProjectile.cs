using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    [SerializeField] private GameObject Player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerPrototype")    //for now, it searches by gameObject name
            {    //if collide with player
            MonoBehaviour.print("hit player");
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            p.setHealth(p.getHealth() - 1);
            Destroy(gameObject); //breaks after colliding with player
        }
    }
}
