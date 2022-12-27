using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private int range;
    private Element element;
    private Renderer renderer;
    public NPC(GameObject self, int health, int level, int range) : base(self, health, level) //calls constructor of superclass
    {
        this.range = range; //sets range to 0 by default
    }

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
        if (damageBonus.type == "NONE" || dmg == 0){ return; }//do nothing
        else if (damageBonus.type == "ADD") {
            dmg = (int)(dmg + damageBonus.amount);  //float is rounded down
        }
        else if (damageBonus.type == "MULTIPLY") {
            dmg = (int)(dmg * damageBonus.amount);  //float is rounded down
        }

        MonoBehaviour.print("damage: " + dmg);
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
    Color defaultColour;

    public void Start() //this needs to be called within the instance script: e.g NPC.Start()
    {
        renderer = self.GetComponent<Renderer>();
        defaultColour = renderer.material.color;
        //MonoBehaviour.print(defaultColour);
    }

    /* 
    The functions for each reaction 
    */
    private float overloadMagnitude = 5.0f; //set this to change the amount of knockback
    private void Overload()
    {
        //apply knockback: add a random force for now
        //rb.AddForce(Vector3.back forwardForce Time.deltaTime, ForceMode.VelocityChange);
        //MonoBehaviour.print(-self.transform.forward);
        self.GetComponent<Rigidbody>().AddForce(overloadMagnitude * -self.transform.forward.normalized, ForceMode.Impulse);       //adds an impulse force to move a backwards
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
                    renderer.material.color = element.getColour();
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
                    //renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                    }
                if (element is Elements.Cryo) {
                    MonoBehaviour.print("melt");
                    damageBonus.set("MULTIPLY", 2.0f);

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    Overload();

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

            case CRYO:
                if (element == null) { break; }
                if (element is Elements.Pyro) {
                    MonoBehaviour.print("melt");
                    damageBonus.set("MULTIPLY", 2.0f);

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Cryo) {
                    //MonoBehaviour.print("nothing");
                    //renderer.material.color = defaultColour;

                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    MonoBehaviour.print("superconduct");

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

            case ELECTRO:
                if (element == null) { break; }
                if (element is Elements.Pyro) {
                    Overload();

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Cryo) {
                    MonoBehaviour.print("superconduct");

                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    //MonoBehaviour.print("nothing");
                    //renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                break;

        }
        damageCalc();
    }
}

