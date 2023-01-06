using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    // necessary for the final bridge spawning 
    [SerializeField] private GameObject game;

    // necessary for spawning the enemies in the right position
    private int spawnOffsetY = 2;

    // needed for island spawning and scene organization
    [SerializeField] private GameObject parentIsland; // the parent in the scene under which the islands are to be spawned
    [SerializeField] private List<GameObject> islandPrefabs;
    private int counter = 1; // counter for number of islands spawned
    private List<string> keys = new List<string>() { "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
    private Dictionary<string, Vector3> locationOffset = new Dictionary<string, Vector3>() // offsets to account for the length of the bridge
    {
        { "BridgeN", new Vector3(0, 0, 32.92f)},
        { "BridgeS", new Vector3(0, 0, -30.52f)},
        { "BridgeE", new Vector3(37.90f, 0, 0)},
        { "BridgeW", new Vector3(-38.88f, 0, 0)},
    };

    // needed for enemy spawning 
    [SerializeField] private List<GameObject> enemies;

    // needed for pickup (key) spawning and scene organization
    [SerializeField] private int keyCount;
    [SerializeField] private GameObject parentPickups;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject parentLabel;
    [SerializeField] private GameObject pickupLabel;
    private List<int> pickupKey = new List<int>();

    // needed for weapon spawning and scene organization
    [SerializeField] private GameObject parentWeapon;
    [SerializeField] private List<GameObject> weaponPrefabs;


    void Awake()
    {
        // generate the list used for key generation 
        for(int i=0; i<PlayerToFollow.shared.islandNum; i++)
        {
            if(i<keyCount)
            {
                pickupKey.Add(1);
                continue;
            }
            pickupKey.Add(0);
        }
 
        SpawnIslands();
    }

    /// <summary>
    /// Spawns the predefined number of islands in the scene.
    /// </summary>
    void SpawnIslands()
    {
        // Spawns the first island (to be treated as the final island in the gameplay) and it's first bridge.
        GameObject initial = Instantiate(islandPrefabs[UnityEngine.Random.Range(0, islandPrefabs.Count())], position: Vector3.zero, rotation: Quaternion.identity, parent: parentIsland.transform);
        initial.name = "initial";
        List<Transform> bridges = GetBridges(initial);
        Transform finalBridge = bridges.Where(b => b.gameObject.name == "BridgeN").First();
        // initially, the bridge to the final island is disabled and is enabled only after collecting all keys
        finalBridge.gameObject.SetActive(false); 
        game.GetComponent<GameController>().bridge = finalBridge.gameObject;

        // randomly spawn the rest of the islands
        GameObject prev = initial;
        for(counter = 0; counter < PlayerToFollow.shared.islandNum; counter++)
        {
            var spawned = SpawnNext(prev, finalBridge);
            prev = spawned.Item1;
            prev.name = counter.ToString();
            if (spawned.Item2 != new GameObject().transform)
            {
                finalBridge = spawned.Item2;
            }
        }

        // after all islands have been spawned fully (including weapons and enemy spawning),
        // the enemies on the final island are spawned. This is so that the number of the enemies 
        // on the final island is the higest (and so the final island is the most difficult to clear).
        SpawnEnemies(initial);
    }

    /// <summary>
    /// Returns all the available bridges for a given island.
    /// </summary>
    List<Transform> GetBridges(GameObject island)
    {
        List<Transform> bridges = island.GetComponentsInChildren<Transform>(includeInactive: true).ToList()
                                        .Where(b => b.gameObject.tag == "bridgeSpawner")
                                        .ToList();

        return bridges;
    }

    /// <summary>
    /// Spawns the next island (including the enemies, pickups and weapons spawning).
    /// </summary>
    Tuple<GameObject, Transform> SpawnNext(GameObject prev, Transform leading)
    {
        // Randomly choose an island prefab and spawn it in the right position. 
        GameObject i = islandPrefabs[UnityEngine.Random.Range(0, islandPrefabs.Count())];
        GameObject current = Instantiate(i, position: prev.transform.position + locationOffset[leading.gameObject.name], rotation: Quaternion.identity, parent: parentIsland.transform);
        var free = GetBridges(current).Where(b => b.gameObject.name != keys[(keys.IndexOf(leading.gameObject.name) + 2) % 4]).ToList();


        // Ensures no bridge is spawned on the last of the spawned islands (to avoid a bridge to nowhere in the game)
        Transform b = new GameObject().transform;
        if (counter < PlayerToFollow.shared.islandNum - 1)
        {
            var bridgeG = free[UnityEngine.Random.Range(0, free.Count())];

            //ensure the bridge can be spawned (so that no two islands overlap)
            Collider[] intersecting = Physics.OverlapSphere(current.transform.position + locationOffset[bridgeG.gameObject.name], 5.0f);
            while (intersecting.Length != 0)
            {
                free.Remove(bridgeG);
                bridgeG = free[UnityEngine.Random.Range(0, free.Count())];
                intersecting = Physics.OverlapSphere(current.transform.position + locationOffset[bridgeG.gameObject.name], 0.2f);
            }

            //when a suitable position is found, spawn an island there
            bridgeG.gameObject.SetActive(true);
            free.Remove(bridgeG);
            b = bridgeG;
        }

        //(randomly) spawn enemies, keys and weapons on the newly created island
        SpawnEnemies(current);
        SpawnKeys(current);
        if(weaponPrefabs.Count()>0)
        {
            SpawnWeapons(current);

        }


        return new Tuple<GameObject, Transform>(current, b);
    }

    /// <summary>
    /// Spawns the enemies on an island. 
    /// The amount of enemies spawned increases on every iteration.
    /// </summary>
    void SpawnEnemies(GameObject island)
    {
        // pick the right number of enemies to be spawned, according to the chosen difficulty level
        int enemyCount = UnityEngine.Random.Range(PlayerToFollow.shared.difficulty+counter, 2*PlayerToFollow.shared.difficulty + counter);
        for(int i=1; i<=enemyCount; i++)
        {
            // pick a random enemy prefab to be spawned
            var enemyPrefab = enemies[UnityEngine.Random.Range(0, enemies.Count())];
            var enemyOffset = new Vector3(UnityEngine.Random.Range(-6,6), spawnOffsetY, UnityEngine.Random.Range(-4, 4));
            
            // ensure that the enemies do not spawn on top of the player
            while(enemyOffset.x == 0 && enemyOffset.z == 0)
            {
                enemyOffset = new Vector3(UnityEngine.Random.Range(-6, 6), spawnOffsetY, UnityEngine.Random.Range(-4, 4));
            }
            var parentEnemy = island.GetComponentsInChildren<Transform>(includeInactive: true).ToList()
                                        .Where(b => b.gameObject.name == "Enemies")
                                        .ToList()
                                        .First();

            // spawn the random enemy as a child of the parentEnemy object, for scene organization
            GameObject e = Instantiate(enemyPrefab, position: island.transform.position + enemyOffset, rotation: Quaternion.identity, parent: parentEnemy.transform);
            // disable the enemies on all but the first island 
            if(counter != 0)
            {
                e.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Spawns the pickups (keys) on a given island. Using the pickupKey array, determine on which islands to spawn keys on, and which ones to leave empty.
    /// </summary>
    void SpawnKeys(GameObject current)
    {
        // determine whether the key is to be spawned
        int keyIndex = pickupKey[UnityEngine.Random.Range(0, pickupKey.Count())];

        // spawn the key 
        if (keyIndex == 1)
        {
            var keyOffset = new Vector3(UnityEngine.Random.Range(-6, 6), spawnOffsetY, UnityEngine.Random.Range(-4, 4));
            var k = Instantiate(keyPrefab, position: current.transform.position + keyOffset, rotation: Quaternion.identity, parent: parentPickups.transform);
            k.name = "k";
            var label = Instantiate(pickupLabel, parent: parentLabel.transform);
            k.GetComponent<PickupController>().text = label;
        }

        pickupKey.Remove(keyIndex);
    }

    /// <summary>
    /// Spawn the weapons on the islands. 
    /// This is only done for the first four islands (so that the user gets two staffs and two swords). The elements assigned to the weapons are randomized.
    /// </summary>
    void SpawnWeapons(GameObject current)
    {
        // randomly determine the element to assign to the spawned weapon, as well as it's location and prefab
        var elements = new List<string>() { "cryo", "pyro", "electro" };
        GameObject weapon = weaponPrefabs[UnityEngine.Random.Range(0, weaponPrefabs.Count())];
        string element = elements[UnityEngine.Random.Range(0, elements.Count())];
        var weaponOffset = new Vector3(UnityEngine.Random.Range(-6, 6), spawnOffsetY, UnityEngine.Random.Range(-4, 4));

        // instantiate the weapon and assign the element to it.
        var w = Instantiate(weapon, position: current.transform.position + weaponOffset, rotation: Quaternion.identity, parent: parentWeapon.transform);
        if(weapon.gameObject.name == "STAFF")
        {
            w.GetComponent<RangedWeapon>().elementName = element;
        }
        else
        {
            w.GetComponent<WeaponInstance>().elementName = element;
        }
        weaponPrefabs.Remove(weapon);
    }
}
