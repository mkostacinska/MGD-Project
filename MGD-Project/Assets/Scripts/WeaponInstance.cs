using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    [SerializeField] private int attack;
    private Weapon thisWeapon;
    private int enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        thisWeapon = new Weapon(attack);
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        //MonoBehaviour.print("hit something"));
        //player can only deal damage when they are in the attack animation
        if (other.gameObject.layer == enemyLayer && GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {    
            MonoBehaviour.print("hit enemy");
            NPC enemy = other.gameObject.GetComponent<SlimeInstance>().thisSlime;
            enemy.setHealth(enemy.getHealth() - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
