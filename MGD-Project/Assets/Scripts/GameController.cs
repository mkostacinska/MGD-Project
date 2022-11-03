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
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;

        //get the initial island the player is on
        Physics.Raycast(transform.position, Vector3.down, out var hit);
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
            var walls = currentIsland.GetComponentsInChildren<Transform>()
            .Where(child => child.gameObject.name == "walls")
            .FirstOrDefault();
            if (walls)
            {
                walls.gameObject.SetActive(false);
            }
            else
            {
                updateIsland();
            }
        }
        print(currentIsland);
    }

    void updateIsland()
    {
        Physics.Raycast(player.transform.position, Vector3.down, out var hit);
        if (hit.collider.gameObject.layer == groundLayer && hit.collider.gameObject != currentIsland)
        {
            currentIsland = hit.collider.gameObject.transform.parent.gameObject;
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
