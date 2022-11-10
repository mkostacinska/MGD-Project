using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private int attack;

    public Weapon(int attack)
    {
        this.attack = attack;
    }

    //getters and setters for the weapons
    public int getAttack() { return this.attack; }
    public void setAttack(int attack) { this.attack = attack; }

}
