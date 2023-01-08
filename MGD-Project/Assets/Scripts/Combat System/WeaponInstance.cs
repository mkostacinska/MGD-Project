using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInstance : MonoBehaviour
{
    //Cooldown Properties
    [SerializeField] private float cooldown = 1;
    [SerializeField] private float cooldownEnd = 0f;

    //Attacking Properties
    private int enemyLayer;
    private bool attacking = false;

    //Element Properties
    [SerializeField] public string elementName = "NAME"; //set projectile element here, used for quick testing
    public Element element;

    InputActionMap actionMap;

    void Start()
    {
        element = new Elements().getElement(elementName);
        enemyLayer = LayerMask.NameToLayer("Enemy");
        gameObject.name = (elementName + " Sword").ToUpper();

        //change colour to match element
        Material[] materials = transform.GetComponentInChildren<MeshRenderer>().materials;
        foreach (Material m in materials)
        {
            //print(m.name);
            if (m.name.StartsWith("Crystal"))
            { //StartsWith() is used because "(instance)" is added by unity
                m.color = element.getColour();
                m.EnableKeyword("_EMISSION");   //https://stackoverflow.com/a/33671094
                m.SetColor("_EmissionColor", element.getColour());
            }
        }
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
                enemy.setElement(element);
                enemy.dealDamage(300);
            }
        }
    }
    protected void checkInputs()
    {
        if (actionMap == null) { actionMap = InputManager.getActionMap(); } //set the actionMap if it does not exist
        if (actionMap.FindAction("Attack").triggered) { OnAttack(); }
    }
    void OnAttack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }

    void Update()
    {
        checkInputs();
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")) 
        { 
            //keep checking if the attacking animation is playing
            attacking = true;
        }
    }
}
