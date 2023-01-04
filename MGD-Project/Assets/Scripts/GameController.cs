using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //needed for the key collection and counting
    [SerializeField] private GameObject pickups;
    [SerializeField] private TextMeshProUGUI keyLabel;
    private int keyCount = 0;
    [SerializeField] private int keyNum = 3;

    //needed for locking the player in an island/enabling/disabling enemies
    [SerializeField] private GameObject player;
    [SerializeField] private int groundLayer = 3;
    private GameObject currentIsland;
    private List<string> islandsCleared;
    public LayerMask playerLayer;

    //needed for spawning the bridge when all keys are collected:
    //[SerializeField] private GameObject bridge;

    //needed for winning
    [SerializeField] private int numberOfIslands = 4;

    //speed increase when rooms are cleared
    private float bonusSpeedMultiplyer = 1.8f; //multiplier
    private float defaultSpeed;
    void Start()
    {
        //get default speed of player
        defaultSpeed = player.GetComponent<PlayerContollerPrototype>().walkSpeed;
        //initial set of key counter text
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;

        //declare a list for storing the islands that have been completed by the player
        islandsCleared = new List<string>();

        //get the initial island the player is on
        Physics.Raycast(player.transform.position, Vector3.down, out var hit, Mathf.Infinity);
        if (hit.collider.gameObject.layer == groundLayer)
        {
            currentIsland = hit.collider.gameObject;
        }
    }

    void Update()
    {
        //check the amount of keys collected and upadte the key counter text
        checkKeyCount();

        //if the island has been cleared, disable the walls around it
        if (enemiesKilled())
        {
            player.GetComponent<PlayerContollerPrototype>().walkSpeed = defaultSpeed * bonusSpeedMultiplyer;
            if (!islandsCleared.Contains(currentIsland.name))
            {
                islandsCleared.Add(currentIsland.name);
            }//add to cleared islands so that the enemies do not get respawned on enter

            //get the gameObject corresponding to walls
            var walls = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "walls")
            .FirstOrDefault();

            if (walls)
            {
                //if the walls have not already been disabled, disable
                walls.gameObject.SetActive(false);
            }
            else
            {
                //if the walls are already disabled, begin checking for a new island object to repeat the process
                updateIsland();
            }

            //print(islandsCleared.Count());
            if (islandsCleared.Count() == numberOfIslands)
            {
                SceneManager.LoadScene("Win");
            }
        }
     
     
    }

    void updateIsland()
    {
        //raycast from the player straignt down below to get the ground they're on
        Physics.Raycast(player.transform.position, Vector3.down, out var hit);

        //if the object the player is standing on is classified as ground & DOES NOT correspond to the most recent island
        if (hit.collider != null) //prevents errors
        if (hit.collider.gameObject.layer == groundLayer && hit.collider.gameObject != currentIsland)
        {
            //reset the current island to the new island they are on
            currentIsland = hit.collider.gameObject.transform.parent.gameObject;
            
            //if the island has not been cleared before, spawn enemies
            if(!islandsCleared.Contains(currentIsland.name))
            {
                player.GetComponent<PlayerContollerPrototype>().walkSpeed = defaultSpeed;
                //get the parent 'enemies' object
                var enemiesParent = currentIsland.GetComponentsInChildren<Transform>()
                    .Where(child => child.gameObject.name == "enemies")
                    .FirstOrDefault();

                var enemies = enemiesParent.gameObject.GetComponentsInChildren<Transform>(true) //true -> get inactive objects as well
                    .Where(enemy => enemy.gameObject != enemiesParent.gameObject)
                    .ToList();

                //set the enemies to active to spawn them 
                enemies.ForEach(enemy => enemy.gameObject.SetActive(true));
            }
        }
    }

    //check if all the enemies on an island have been killed
    bool enemiesKilled()
    {
        var enemiesParent = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "Enemies")
            .FirstOrDefault();

        var enemies = enemiesParent.gameObject.GetComponentsInChildren<Transform>()
            .Where(enemy => enemy.gameObject.activeInHierarchy && enemy.gameObject != enemiesParent.gameObject)
            .ToList();

        return enemies.Count() == 0;
    }

    //check how many keys have been collected (the collected keys become inactive)
    void checkKeyCount()
    {
        List<Transform> keysChildren = pickups.GetComponentsInChildren<Transform>()
            .Where(key => key.gameObject.activeInHierarchy && key != pickups.transform)
            .Where(key => key.gameObject.name == "k")
            .ToList();

        //the number of keys collected is (total number of keys) - (the ones remaining in the scene)
        keyCount = keyNum - keysChildren.Count;
        updateText(); //update the key counter accordinglu
        
        //if all keys have been collected, allow access to the final island by spawning a bridge
        if(keyCount == keyNum)
        {
            //bridge.SetActive(true);
        }
    }
    void updateText()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;
    }
}
