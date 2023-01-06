using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject island;
    [SerializeField] private GameObject parentIsland;
    //[SerializeField] private int numberOfIslands;
    [SerializeField] private List<GameObject> islandPrefabs;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parentPickups;
    [SerializeField] private int keyCount;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject pickupLabel;
    [SerializeField] private GameObject parentLabel;
    private List<int> pickupKey = new List<int>();
    [SerializeField] private GameObject parentWeapon;
    [SerializeField] private List<GameObject> weaponPrefabs;

    private int counter = 1;
    private List<string> keys = new List<string>(){ "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
    private Dictionary<string, Vector3> locationOffset = new Dictionary<string, Vector3>()
    {
        { "BridgeN", new Vector3(0, 0, 32.92f)},
        { "BridgeS", new Vector3(0, 0, -30.52f)},
        { "BridgeE", new Vector3(37.90f, 0, 0)},
        { "BridgeW", new Vector3(-38.88f, 0, 0)},
    };


    // Start is called before the first frame update
    void Awake()
    {
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

    void SpawnIslands()
    {
        //Spawns the first island as well as the final island to the north of it
        GameObject initial = Instantiate(island, position: Vector3.zero, rotation: Quaternion.identity, parent: parentIsland.transform);
        initial.name = "initial";
        List<Transform> bridges = GetBridges(initial);
        Transform finalBridge = bridges.Where(b => b.gameObject.name == "BridgeN").First();
        finalBridge.gameObject.SetActive(false);
        game.GetComponent<GameController>().bridge = finalBridge.gameObject;

        //Instantiate(player, position: new Vector3(0, 196.0869f, 0) + initial.transform.position, rotation: Quaternion.identity);

        //randomly generate each of the remaining islands
        GameObject prev = initial;
        for(counter = 0; counter< PlayerToFollow.shared.islandNum; counter++)
        {
            var spawned = SpawnNext(prev, finalBridge);
            print(spawned);
            prev = spawned.Item1;
            prev.name = counter.ToString();
            if (spawned.Item2 != new GameObject().transform)
            {
                finalBridge = spawned.Item2;
            }
        }

        SpawnEnemies(initial);
    }

    List<Transform> GetBridges(GameObject island)
    {
        List<Transform> bridges = island.GetComponentsInChildren<Transform>(includeInactive: true).ToList()
                                        .Where(b => b.gameObject.tag == "bridgeSpawner")
                                        .ToList();

        return bridges;
    }

    Tuple<GameObject, Transform> SpawnNext(GameObject prev, Transform leading)
    {
        //pick a random prefab and spawn it
        GameObject i = islandPrefabs[UnityEngine.Random.Range(0, islandPrefabs.Count())];
        GameObject current = Instantiate(i, position: prev.transform.position + locationOffset[leading.gameObject.name], rotation: Quaternion.identity, parent: parentIsland.transform);
        var free = GetBridges(current).Where(b => b.gameObject.name != keys[(keys.IndexOf(leading.gameObject.name) + 2) % 4]).ToList();


        Transform b = new GameObject().transform;
        if (counter < PlayerToFollow.shared.islandNum - 1)
        {
            var bridgeG = free[UnityEngine.Random.Range(0, free.Count())];

            //ensure the bridge can be spawned (and will be leading to an empty space in a map)
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


        //(randomly) spawn enemies on the newly created island
        SpawnEnemies(current);
        SpawnKeys(current);
        if(weaponPrefabs.Count()>0)
        {
            SpawnWeapons(current);

        }


        return new Tuple<GameObject, Transform>(current, b);
    }

    void SpawnEnemies(GameObject island)
    {
        //on regular islands, spawn between 3 and 6 enemies (?)
        int enemyCount = UnityEngine.Random.Range(PlayerToFollow.shared.difficulty+counter, 2*PlayerToFollow.shared.difficulty + counter);
        for(int i=1; i<=enemyCount; i++)
        {
            // pick a random enemy to spawn
            var enemyPrefab = enemies[UnityEngine.Random.Range(0, enemies.Count())];
            //UnityEngine.Random.Range(-10, 10)
            var enemyOffset = new Vector3(UnityEngine.Random.Range(-6,6), 2, UnityEngine.Random.Range(-4, 4));
            while(enemyOffset.x == 0 && enemyOffset.z == 0)
            {
                enemyOffset = new Vector3(UnityEngine.Random.Range(-6, 6), 2, UnityEngine.Random.Range(-4, 4));
            }
            var parentEnemy = island.GetComponentsInChildren<Transform>(includeInactive: true).ToList()
                                        .Where(b => b.gameObject.name == "Enemies")
                                        .ToList()
                                        .First();

            GameObject e = Instantiate(enemyPrefab, position: island.transform.position + enemyOffset, rotation: Quaternion.identity, parent: parentEnemy.transform);
            if(counter != 0)
            {
                e.SetActive(false);
            }
        }

        return;
    }

    private float spawnOffsetY = 2.0f;
    void SpawnKeys(GameObject current)
    {
        int keyIndex = pickupKey[UnityEngine.Random.Range(0, pickupKey.Count())];
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

    void SpawnWeapons(GameObject current)
    {
        var elements = new List<string>() { "cryo", "pyro", "electro" };
        GameObject weapon = weaponPrefabs[UnityEngine.Random.Range(0, weaponPrefabs.Count())];
        string element = elements[UnityEngine.Random.Range(0, elements.Count())];
        var weaponOffset = new Vector3(UnityEngine.Random.Range(-6, 6), spawnOffsetY, UnityEngine.Random.Range(-4, 4));
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
