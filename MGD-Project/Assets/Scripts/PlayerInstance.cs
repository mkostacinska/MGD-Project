using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int level = 1;
    [SerializeField] private string name = "Player 1";
    public Player thisPlayer;
    // Start is called before the first frame update
    void Start()
    {
       thisPlayer = new Player(health, level, name);
    }

    // Update is called once per frame
    void Update()
    {
        //this.Update();
        //update health and level if it is changed via unity field
        //thisPlayer.setHealth(health);
        //thisPlayer.setLevel(level);

        //if player has no hp, kill player
        if (thisPlayer.getHealth() <= 0)
        {
            //MonoBehaviour.print("dead");
            Destroy(gameObject);
        }
    }
}
