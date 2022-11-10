using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    public NPC thisTurret;
    [SerializeField] private GameObject Player;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        thisTurret = new NPC(health, level, range);
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        //set healthbar to health
        healthBar.Sethealth(thisTurret.getHealth());

        if (thisTurret.getHealth() <= 0)
        {
            //MonoBehaviour.print("dead");
            Destroy(gameObject);
        }
    }
}
