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
        MonoBehaviour.print(defaultColour);
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
                    MonoBehaviour.print(element.getElementNumber());
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
                    renderer.material.color = defaultColour;
                    elementState = NONE;
                    this.element = null; //achknowledge element and reset
                    break;
                }
                if (element is Elements.Electro) {
                    MonoBehaviour.print("overload");
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
                    MonoBehaviour.print("overload");
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
    }
}

