using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : Character
{
    private int range;
    private Element element;
    private Renderer renderer;
    public NPC(GameObject self, int health, int level, int range) : base(self, health, level) //calls constructor of superclass
    {
        this.range = range; //sets range to 0 by default
    }

    public float resistance = 0.0f;
    class damageBonusClass {
        public string type;
        public float amount;
        public damageBonusClass() {
            type = "NONE";
            amount = 0;
        }
        public void set(string type, float amount) { this.type = type; this.amount = amount; }
    }
    private damageBonusClass damageBonus = new damageBonusClass();
    private int dmg = 0;
    public void dealDamage(int dmg) { this.dmg = dmg; }  //store damage so that damage bonuses can be calculated

    public void damageCalc()
    {
        float damage = dmg;
        if (dmg == 0) { return; }
        else if (damageBonus.type == "NONE"){ } //do nothing
        else if (damageBonus.type == "ADD") {
            damage = dmg + damageBonus.amount;
        }
        else if (damageBonus.type == "MULTIPLY") {
            damage = dmg * damageBonus.amount;
        }
        //resistance multiplier formula from https://genshin-impact.fandom.com/wiki/Resistance#RES_Multiplier
        float resistanceMultiplier;
        if (resistance < 0)
            resistanceMultiplier = 1 - (resistance / 2);
        else if (resistance < 0.75)
            resistanceMultiplier = 1 - resistance;
        else
            resistanceMultiplier = 1 / (4 * resistance + 1);

        dmg = (int)(damage * resistanceMultiplier); //float is rounded down at the end
        //MonoBehaviour.print("damage: " + dmg);
        this.setHealth(this.getHealth() - dmg);

        //reset damage and damage bonus
        dmg = 0;
        damageBonus = new damageBonusClass(); 
    }
    public void setElement(Element element) { this.element = element; }
    public Element getElement() { return element; }

    //element needs to apply to each enemy seperately
    //run in each instance of enemy
    //public class ElementalReaction : MonoBehaviour

    //defining element states
    const int NONE = 0;
    const int PYRO = 1;
    const int CRYO = 2;
    const int ELECTRO = 3;
    //states have to be manually added if new reactions are added

    int elementState = NONE;
    ElementIcon elementIcon;
    Color defaultHealthColor;
    public void Start() //this needs to be called within the instance script: e.g NPC.Start()
    {
        elementIcon = self.transform.Find("Canvas/Health Bar/Element Icon").GetComponent<ElementIcon>();      //find child "Element Icon" of "Health bar" of "Canvas"
        healthbarFill = self.transform.Find("Canvas/Health Bar/Fill").GetComponent<Image>();                  //get the healthbar fill to change the colour
        defaultHealthColor = healthbarFill.color;
    }

    /* 
    The functions for each reaction 
    */
    private float overloadMagnitude = 5.0f; //set this to change the amount of knockback
    private float reactionTextTime = 0.4f;  //time that the reaction text stays in seconds
    private void Overload()
    {
        elementIcon.setReaction("Overload", reactionTextTime);

        //apply knockback: add a random force for now
        //rb.AddForce(Vector3.back forwardForce Time.deltaTime, ForceMode.VelocityChange);
        //MonoBehaviour.print(-self.transform.forward);
        self.GetComponent<Rigidbody>().AddForce(overloadMagnitude * -self.transform.forward.normalized, ForceMode.Impulse);       //adds an impulse force to move a backwards
    }

    float superConductTime = 0;
    float cooldown = 12f;   //cooldown in seconds
    Image healthbarFill;
    private void Superconduct() {
        elementIcon.setReaction("Superconduct", reactionTextTime);

        //set resistance for a certain amount of time
        resistance = -0.4f;
        superConductTime = Time.time + cooldown;    //reset cooldown if superconduct reaction is triggered again

        //get the healthbar and change colour to signify superconduct
        healthbarFill.color = Color.yellow;
    }

    //Element element;
    //state transition model for elemental reactions
    //the state transition diagram is in the google doc
    public void Update() //this needs to be called within the instance script: e.g NPC.Update()
    {
        //detect element then switch
        //if not in NONE and element detected, cause reaction
        switch (elementState)
        {
            case NONE:
                //MonoBehaviour.print("case none");
                //change colour to element type, then switch state
                if (element != null)
                {
                    //get element icon and change to element
                    //var elementIcon = this.self.transform.Find("Element Icon");
                    elementIcon.setElementIcon(element);
                    elementState = element.getElementNumber();
                    //MonoBehaviour.print(element.getElementNumber());
                    this.element = null; //achknowledge element and reset
                }
                break;

            case PYRO:
                //if element = pyro, do nothing
                //if element = cryo, "melt" reaction
                //if element = electro, "overload" reaction
                if (element == null) { break; }
                if (element is Elements.Pyro) {
                    //MonoBehaviour.print("nothing");

                    this.element = null; //achknowledge element and reset
                    break;
                    }
                if (element is Elements.Cryo) {
                    //MonoBehaviour.print("melt");
                    damageBonus.set("MULTIPLY", 2.0f);
                    elementIcon.setReaction("Melt", reactionTextTime);

                    elementIcon.setElementIcon(null);
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    Overload();

                    elementIcon.setElementIcon(null);
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

            case CRYO:
                if (element == null) { break; }
                if (element is Elements.Pyro) {
                    //MonoBehaviour.print("melt");
                    damageBonus.set("MULTIPLY", 2.0f);
                    elementIcon.setReaction("Melt", reactionTextTime);

                    elementIcon.setElementIcon(null);
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Cryo) {
                    //MonoBehaviour.print("nothing");

                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    //MonoBehaviour.print("superconduct");
                    Superconduct();

                    elementIcon.setElementIcon(null);
                    //renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

            case ELECTRO:
                if (element == null) { break; }
                if (element is Elements.Pyro) {
                    Overload();

                    elementIcon.setElementIcon(null);
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Cryo) {
                    //MonoBehaviour.print("superconduct");
                    Superconduct();

                    elementIcon.setElementIcon(null);
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    //MonoBehaviour.print("nothing");

                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

        }
        damageCalc();
        if (Time.time > superConductTime) { 
            resistance = 0.0f; //reset resistance decrease from superconduct
            healthbarFill.color = defaultHealthColor;
        } 
    }
}

