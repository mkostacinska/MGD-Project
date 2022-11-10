using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private int range;
    public NPC(int health, int level, int range) : base (health, level) //calls constructor of superclass
    {
        this.range = range; //sets range to 0 by default
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
