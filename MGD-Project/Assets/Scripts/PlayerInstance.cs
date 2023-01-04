using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int level = 1;
    [SerializeField] private new string name = "Player 1";
    public Player thisPlayer;
    public HealthBar healthBar;
    
    void Start()
    {
        thisPlayer = new Player(gameObject, health, level, name);
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //set healthbar to player health
        healthBar.Sethealth(thisPlayer.getHealth());

        //if player falls below certain y into the void, kill player
        //if player has no hp, kill player
        if (thisPlayer.getHealth() <= 0 || gameObject.transform.position.y < -30)
        {
            //MonoBehaviour.print("dead");
            //Destroy(gameObject);
            PlayerPrefs.SetString("Death by", thisPlayer.getLastHitBy()); //method from: https://stackoverflow.com/questions/32306704/how-to-pass-data-and-references-between-scenes-in-unity
            SceneManager.LoadScene("DeathMenu");
        }
    }
}
