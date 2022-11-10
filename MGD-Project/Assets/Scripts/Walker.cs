using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : NPC
{
    private Rigidbody rigidbody;
    public Walker(int health, int level, int range) : base(health, level, range)
    {

    }
}
