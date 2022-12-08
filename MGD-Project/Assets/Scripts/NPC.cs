using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private int range;
    public NPC(int health, int level, int range) : base (health, level) //calls constructor of superclass
    {
        this.range = range; //sets range to 0 by default
    }

    //element needs to apply to each enemy seperately
    //run in each instance of enemy
    public class ElementalReaction : MonoBehaviour
    {
        //defining element states
        private const int NONE = 0;
        private const int PYRO = Elements.Pyro.elementNumber;
        private const int CRYO = 2;
        private const int ELECTRO = 3;
        //states have to be manually added if new reactions are added

        private int elementState = NONE;
        private Element element;
        //state transition model for elemental reactions
        //the state transition diagram is in the google doc
        void Update()
        {
            //detect element then switch
            //if not in NONE and element detected, cause reaction
            switch (elementState)
            {
                case NONE:
                    //change colour to element type, then switch state
                    if (element != null)
                    {
                        element = null; //achknowledge element
                        gameObject.GetComponent<MeshRenderer>().material.color = element.colour;
                        elementState = element.elementNumber;
                    }
                    break;

                case PYRO:
                    //if element = pyro, do nothing
                    //if element = cryo, "melt" reaction
                    //if element = electro, "overload" reaction
                    if (element is Elements.Pyro) { }
                    if (element is Elements.Cryo) { }
                    if (element is Elements.Electro) { }
                    break;

                case CRYO:
                    if (element is Elements.Pyro) { }
                    if (element is Elements.Cryo) { }
                    if (element is Elements.Electro) { }
                    break;

                case ELECTRO:
                    if (element is Elements.Pyro) { }
                    if (element is Elements.Cryo) { }
                    if (element is Elements.Electro) { }
                    break;

            }
        }
    }

}

