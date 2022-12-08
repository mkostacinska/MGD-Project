using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : NPC
{
    private Rigidbody rigidbody;
    public Walker(GameObject self, int health, int level, int range) : base(self, health, level, range)
    {

    }
}
