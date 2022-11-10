using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : NPC
{
    private Rigidbody rigidbody;
   
    //call to the superclass
    public Slime(int health, int level, int range) : base(health, level, range) { }
}
