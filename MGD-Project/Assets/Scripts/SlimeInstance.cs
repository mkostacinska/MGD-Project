using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    public Slime thisSlime;
    [SerializeField] private GameObject Player;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        thisSlime = new Slime(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player) {    //if collide with player
            MonoBehaviour.print("hit player");
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            p.setHealth(p.getHealth() - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set healthbar to health
        healthBar.Sethealth(thisSlime.getHealth());

        if (thisSlime.getHealth() <= 0)
        {
            //MonoBehaviour.print("dead");
            Destroy(gameObject);
        }
    }
}
