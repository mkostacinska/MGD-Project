using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private int range;
    private Element element;
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
    void Start()
    {
        defaultColour = self.GetComponent<MeshRenderer>().material.color;
        MonoBehaviour.print("TEST");
    }

    //Element element;
    //state transition model for elemental reactions
    //the state transition diagram is in the google doc

    void Update()
    { 
        //detect element then switch
        //if not in NONE and element detected, cause reaction
        switch (elementState)
        {
            case NONE:
                MonoBehaviour.print("case none");
                //change colour to element type, then switch state
                if (element != null)
                {
                    self.GetComponent<MeshRenderer>().material.color = element.colour;
                    elementState = element.elementNumber;
                    this.element = null; //achknowledge element and reset
                }
                break;

            case PYRO:
                //if element = pyro, do nothing
                //if element = cryo, "melt" reaction
                //if element = electro, "overload" reaction
                if (element is Elements.Pyro) {
                    MonoBehaviour.print("nothing");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                    }
                if (element is Elements.Cryo) {
                    MonoBehaviour.print("melt");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                if (element is Elements.Electro) {
                    MonoBehaviour.print("overload");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                break;

            case CRYO:
                if (element is Elements.Pyro) {
                    MonoBehaviour.print("melt");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                if (element is Elements.Cryo) {
                    MonoBehaviour.print("nothing");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                if (element is Elements.Electro) {
                    MonoBehaviour.print("superconduct");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                break;

            case ELECTRO:
                if (element is Elements.Pyro) {
                    MonoBehaviour.print("overload");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                if (element is Elements.Cryo) {
                    MonoBehaviour.print("superconduct");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                if (element is Elements.Electro) {
                    MonoBehaviour.print("nothing");
                    self.GetComponent<MeshRenderer>().material.color = defaultColour;
                    elementState = NONE;
                    break;
                }
                break;

        }
    }
}

