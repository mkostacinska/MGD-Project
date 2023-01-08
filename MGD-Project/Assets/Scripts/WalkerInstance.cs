using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    public NPC thisWalker;
    public HealthBar healthBar;

    void Start()
    {
        thisWalker = new NPC(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
        thisWalker.Start();
    }

    [SerializeField] private float cooldown = 0.5f;
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == PlayerToFollow.shared.player)
        {
            if (cooldownCheck())
            {
                Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
                //p.setHealth(p.getHealth() - 1);
                p.dealDamage("Snail", 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        thisWalker.Update();
        //set healthbar to health
        healthBar.Sethealth(thisWalker.getHealth());

        if (thisWalker.getHealth() <= 0 || gameObject.transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
}
