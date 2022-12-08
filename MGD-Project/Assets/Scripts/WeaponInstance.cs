using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    [SerializeField] private int attack;
    [SerializeField] private GameObject Player;
    [SerializeField] private float cooldown = 1;
    [SerializeField] private float cooldownEnd = 0f;
    private Weapon thisWeapon;
    private int enemyLayer;
    private bool attacking = false;

    void Start()
    {
        thisWeapon = new Weapon(attack);
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    bool cooldownCheck()
    {
        if (Time.time > cooldownEnd)
        {
            cooldownEnd = Time.time + cooldown; //acknowledge and reset cooldown
            return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //player can only deal damage when they are in the attack animation
        if (cooldownCheck())
        {
            Debug.Log(attacking);
            if (other.gameObject.layer == enemyLayer && attacking == true)
            {
                NPC enemy = null;
                if (other.gameObject.TryGetComponent(out SlimeInstance slime))
                { 
                    //set the enemy to the slime that has been hit
                    enemy = other.gameObject.GetComponent<SlimeInstance>().thisSlime; 
                }
                else if (other.gameObject.TryGetComponent(out WalkerInstance walker))
                {
                    //set the enemy to the walker that has been hit
                    enemy = other.gameObject.GetComponent<WalkerInstance>().thisWalker; 
                }    
                else if (other.gameObject.TryGetComponent(out TurrentInstance turret))
                {
                    //set the enemy to the turret that has been hit
                    enemy = other.gameObject.GetComponent<TurrentInstance>().thisTurret;
                }    

                //decrease the health of the enemy appropriately
                enemy.setHealth(enemy.getHealth() - 3);
            }
        }
    }

    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")) 
        { 
            //keep checking if the attacking animation is playing
            attacking = true;
        }
    }
}
