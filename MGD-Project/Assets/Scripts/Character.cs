using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class Character, used to model all entities in the game
public abstract class Character
{
    protected int health, level, attack;

    public Character(int health, int level)
    {
        this.health = health;
        this.level = level;
    }

    //getters and setters
    public int getAttack(){return getLevel();} //set attack to level for now
    public void setLevel(int level){this.level = level;}
    public int getLevel(){return level;}
    public void setHealth(int health){this.health = health;}
    public int getHealth(){return health;}
}
