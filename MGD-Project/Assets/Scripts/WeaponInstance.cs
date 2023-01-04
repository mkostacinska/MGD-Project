using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInstance : MonoBehaviour
{
    [SerializeField] private int attack;
    [SerializeField] private float cooldown = 1;
    [SerializeField] private float cooldownEnd = 0f;
    private Weapon thisWeapon;
    private int enemyLayer;
    private bool attacking = false;
    [SerializeField] public string elementName = "NAME"; //set projectile element here, used for quick testing
    public Element element;

    void Start()
    {
        element = new Elements().getElement(elementName);
        thisWeapon = new Weapon(attack);
        enemyLayer = LayerMask.NameToLayer("Enemy");
        gameObject.name = (elementName + " Sword").ToUpper();
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
    private void OnTriggerStay(Collider other)
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
                //enemy.setHealth(enemy.getHealth() - 3);
                enemy.setElement(element);
                enemy.dealDamage(300);
            }
        }
    }

    void OnAttack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }

    void Update()
    {
        //there is a Unity Engine Input System bug where some gameobjects' actions are not triggered
        //this bug only occurs in the build and not the editor and which object's actions break is not consistent; changes each build
        //this code is a backup for when this object's actions are bugged
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().currentActionMap.FindAction("Attack").triggered)
        {
            OnAttack();
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")) 
        { 
            //keep checking if the attacking animation is playing
            attacking = true;
        }
    }
}
