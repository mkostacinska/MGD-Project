using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    public NPC thisSlime;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        thisSlime = new NPC(gameObject, health, level, range);
        healthBar.SetMaxHealth(health);
        thisSlime.Start();
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
                p.dealDamage("Slime", 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        thisSlime.Update();

        //rotate enemy to face player
        Vector3 difference = PlayerToFollow.shared.player.transform.position - transform.position;
        Vector3 direction = difference.normalized;      //gets the unit vector direction
        transform.forward = direction;

        //set healthbar to health
        healthBar.Sethealth(thisSlime.getHealth());

        if (thisSlime.getHealth() <= 0 || gameObject.transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
}
