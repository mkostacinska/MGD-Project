using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class Character, used to model all entities in the game
/// </summary>
public abstract class Character
{
    //Properties
    protected int health, level, attack;
    protected GameObject self; //self is the gameobject for the instance, useful since this class does not inherit from MonoBehaviour
    protected string lastHitBy;
    
    //Constructor
    public Character(GameObject self, int health, int level)
    {
        this.self = self;
        this.health = health;
        this.level = level;
    }

    //Getters and Setters
    public int getAttack(){return getLevel();} //set attack to level for now
    public void setLevel(int level){this.level = level;}
    public int getLevel(){return level;}
    public void setHealth(int health){this.health = health;}
    public int getHealth(){return health;}


    /// <param name="name">The name of the enemy/damage source</param>
    public void dealDamage(string name, int damage) {
        setHealth(getHealth() - damage);
        lastHitBy = name;
    }

    /// <summary>
    /// Used to get the last thing player was hit by e.g. for cause of death
    /// </summary>
    public string getLastHitBy() { return lastHitBy; }
}
