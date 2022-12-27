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
    //set as a class in case other features require access to element type
    public class Pyro : Element {
        public override Color getColour() { return Color.red; }
        public override int getElementNumber() { return 1; }
    }

    public class Cryo : Element {
        public override Color getColour() { return Color.cyan; }
        public override int getElementNumber() { return 2; }
    }

    public class Electro : Element {
        public override Color getColour() { return Color.magenta; }
        public override int getElementNumber() { return 3; }
    }
}

//superclass used so that polymorphism can be used when element is unknown
public abstract class Element
{
    public abstract Color getColour();
    public abstract int getElementNumber();
}