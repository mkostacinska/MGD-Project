using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private GameObject island;
    [SerializeField] private GameObject parentIsland;
    [SerializeField] private int numberOfIslands;
    [SerializeField] private List<GameObject> islandPrefabs;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parentPickups;

    private int counter = 0;
    private List<string> keys = new List<string>(){ "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
    private Dictionary<string, Vector3> locationOffset = new Dictionary<string, Vector3>()
    {
        { "BridgeN", new Vector3(0, 0, 32.92f)},
        { "BridgeS", new Vector3(0, 0, -30.52f)},
        { "BridgeE", new Vector3(37.90f, 0, 0)},
        { "BridgeW", new Vector3(-38.88f, 0, 0)},
    };

    // Start is called before the first frame update
    void Start()
    {
        SpawnIslands();
    }

    void SpawnIslands()
    {
        //Spawns the first island as well as the final island to the north of it
        GameObject initial = Instantiate(island, position: Vector3.zero, rotation: Quaternion.identity, parent: parentIsland.transform);
        List<Transform> bridges = GetBridges(initial);
        Transform finalBridge = bridges.Where(b => b.gameObject.name == "BridgeN").First();
        finalBridge.gameObject.SetActive(true);

        //Instantiate(player, position: new Vector3(0, 196.0869f, 0) + initial.transform.position, rotation: Quaternion.identity);

        //randomly generate each of the remaining islands
        GameObject prev = initial;
        for(counter = 0; counter<=numberOfIslands; counter++)
        {
            var spawned = SpawnNext(prev, finalBridge);
            prev = spawned.Item1;
            finalBridge = spawned.Item2;
        }        
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


        Transform b = null;
        if (counter < numberOfIslands)
        {
            var bridgeG = free[UnityEngine.Random.Range(0, free.Count())];
            
            //ensure the bridge can be spawned (and will be leading to an empty space in a map)
            Collider[] intersecting = Physics.OverlapSphere(current.transform.position + locationOffset[bridgeG.gameObject.name], 5.0f);
            while(intersecting.Length != 0)
            {
                free.Remove(bridgeG);
                bridgeG = free[UnityEngine.Random.Range(0, free.Count())];
                intersecting = Physics.OverlapSphere(current.transform.position + locationOffset[bridgeG.gameObject.name], 0.2f);
            }

            //when a suitable position is found, spawn an island there
            bridgeG.gameObject.SetActive(true);
            free.Remove(bridgeG);
            b = bridgeG;

            //(randomly) spawn enemies on the newly created island
            SpawnEnemies(current);
       
        }

        return new Tuple<GameObject, Transform>(current, b);
    }

    void SpawnEnemies(GameObject island)
    {
        //on regular islands, spawn between 3 and 6 enemies (?)
        int enemyCount = UnityEngine.Random.Range(3, 6);
        for(int i=1; i<=enemyCount; i++)
        {
            // pick a random enemy to spawn
            var enemyPrefab = enemies[UnityEngine.Random.Range(0, enemies.Count)];
            //UnityEngine.Random.Range(-10, 10)
            var enemyOffset = new Vector3(UnityEngine.Random.Range(-6,6), 2, UnityEngine.Random.Range(-4, 4));
            var parentEnemy = island.GetComponentsInChildren<Transform>(includeInactive: true).ToList()
                                        .Where(b => b.gameObject.name == "Enemies")
                                        .ToList()
                                        .First();

            GameObject e = Instantiate(enemyPrefab, position: island.transform.position + enemyOffset, rotation: Quaternion.identity, parent: parentEnemy.transform);
            
        }

        return;
    }
}
