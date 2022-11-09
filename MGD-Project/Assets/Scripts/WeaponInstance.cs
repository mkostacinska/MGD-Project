using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    [SerializeField] private int attack;
    [SerializeField] private GameObject Player;
    private Weapon thisWeapon;
    private int enemyLayer;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        thisWeapon = new Weapon(attack);
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    [SerializeField] private float cooldown = 1;
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
    private void OnTriggerEnter(Collider other)
    {
        //MonoBehaviour.print("hit something");
        //player can only deal damage when they are in the attack animation
        if (cooldownCheck())
        {
            Debug.Log(attacking);
            if (other.gameObject.layer == enemyLayer && attacking == true)
            {
                MonoBehaviour.print("hit enemy");
                NPC enemy = null;
                if (other.gameObject.TryGetComponent(out SlimeInstance slime))
                { enemy = other.gameObject.GetComponent<SlimeInstance>().thisSlime; }      //sets enemy to slime if it exists
                if (other.gameObject.TryGetComponent(out WalkerInstance walker))
                { enemy = other.gameObject.GetComponent<WalkerInstance>().thisWalker; }    //sets enemy to walker if it exists
                enemy.setHealth(enemy.getHealth() - 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")) { //keep checking if the attacking animation is playing
            attacking = true;
        }
    }
}
