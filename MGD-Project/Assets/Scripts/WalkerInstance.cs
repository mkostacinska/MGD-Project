using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    public Walker thisWalker;
    [SerializeField] private GameObject Player;
    public HealthBar healthBar;

    void Start()
    {
        thisWalker = new Walker(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {   
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            p.setHealth(p.getHealth() - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set healthbar to health
        healthBar.Sethealth(thisWalker.getHealth());

        if (thisWalker.getHealth() <= 0)
        {
            //MonoBehaviour.print("dead");
            Destroy(gameObject);
        }
    }
}
