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

    public int getAttack() { return this.attack; }
    public void setAttack(int attack) { this.attack = attack; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
