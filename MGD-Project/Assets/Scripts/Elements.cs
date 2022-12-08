using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 This class holds all the elements in the game
to access an element: Elements.pyro for pyro type
 */
//outer class
public class Elements : MonoBehaviour
{
    private void Start()
    {
        //create the elements which are instances of element
        public Element pyro;
        Pyro.colour = Color.red;
    }

    //set as a class in case other features require access to element type
    public class Pyro : Element { 
        //amount
        Color colour = Color.red;
        public int elementNumber = 1;
    }

    public class Cryo : Element {
        Color colour = Color.cyan;
        public int elementNumber = 2;
    }

    public class Electro : Element {
        Color colour = Color.magenta;
        public int elementNumber = 3;
    }
}

//superclass used so that polymorphism can be used when element is unknown
public class Element : MonoBehaviour
{
    public Color colour = Color.green; //this should never happen so this signifies an error
    public int elementNumber = 0; //also an error

    public Element(Color colour, int elementNumber)
    {
        this.colour = colour;
    }
}