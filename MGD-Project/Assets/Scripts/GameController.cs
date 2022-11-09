using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

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
    [SerializeField] private GameObject currentIsland;
    private List<string> islandsCleared;

    // Start is called before the first frame update
    void Start()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;
        islandsCleared = new List<string>();

        //get the initial island the player is on
        Physics.Raycast(player.transform.position, Vector3.down, out var hit);
        if (hit.collider.gameObject.layer == groundLayer)
        {
            currentIsland = hit.collider.gameObject.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        checkKeyCount();
        if (enemiesKilled())
        {
            islandsCleared.Add(currentIsland.name);
            var walls = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "walls")
            .FirstOrDefault();
            if(walls)
            {
                walls.gameObject.SetActive(false);
            }
            else
            {
                updateIsland();
            }
        }
    }

    void updateIsland()
    {
        Physics.Raycast(player.transform.position, Vector3.down, out var hit);
        if (hit.collider.gameObject.layer == groundLayer && hit.collider.gameObject != currentIsland)
        {
            currentIsland = hit.collider.gameObject.transform.parent.gameObject;
            if(!islandsCleared.Contains(currentIsland.name))
            {
                print("setting enemies");
                var enemiesParent = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "enemies")
            .FirstOrDefault();

                var enemies = enemiesParent.gameObject.GetComponentsInChildren<Transform>(true)
                    .Where(enemy => enemy.gameObject != enemiesParent.gameObject)
                    .ToList();

                print(enemies.Count());
                enemies.ForEach(enemy => print(enemy.gameObject.name));

                enemies.ForEach(enemy => enemy.gameObject.SetActive(true));
            }
        }
    }

    bool enemiesKilled()
    {
        var enemiesParent = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "enemies")
            .FirstOrDefault();

        var enemies = enemiesParent.gameObject.GetComponentsInChildren<Transform>()
            .Where(enemy => enemy.gameObject.activeInHierarchy && enemy.gameObject != enemiesParent.gameObject)
            .ToList();

        return enemies.Count() == 0;
    }

    void checkKeyCount()
    {
        List<Transform> keysChildren = pickups.GetComponentsInChildren<Transform>()
            .Where(key => key.gameObject.activeInHierarchy && key != pickups.transform)
            .ToList();

        keyCount = keyNum - keysChildren.Count;
        updateText();
    }
    void updateText()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;
    }
}
