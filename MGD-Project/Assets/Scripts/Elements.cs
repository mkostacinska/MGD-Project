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
    /*
    public Element Pyro;
    void Start()
    {
        Element Pyro = new Element(Color.red, 1);
        Element Cryo = new Element(Color.cyan, 2);
        Element Electro = new Element(Color.magenta, 3);
    }
    */
    
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
public class Element
{
    public Color colour = Color.green; //this should never happen so this signifies an error
    public int elementNumber = 0; //also an error

    /*
    public Element(Color colour, int elementNumber)
    {
        this.colour = colour;
        this.elementNumber = elementNumber;
    }
    */
}