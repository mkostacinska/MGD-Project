using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private string name;

    public Player(GameObject self, int health, int level, string name) : base(self, health, level) //calls superclass's constructor
    {
        this.name = name;
    }  
  
}
