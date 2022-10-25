using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private string name;

    public Player(int health, int level, string name) : base(health, level) //calls superclass's constructor
    {
        this.name = name;
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
