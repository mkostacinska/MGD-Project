using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int level = 1;
    [SerializeField] private string name = "Player 1";
    public Player thisPlayer;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
       thisPlayer = new Player(health, level, name);
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //set healthbar to player health
        healthBar.Sethealth(thisPlayer.getHealth());

        //if player has no hp, kill player
        if (thisPlayer.getHealth() <= 0)
        {
            //MonoBehaviour.print("dead");
            //Destroy(gameObject);
            SceneManager.LoadScene("Restart");
        }
    }
}
