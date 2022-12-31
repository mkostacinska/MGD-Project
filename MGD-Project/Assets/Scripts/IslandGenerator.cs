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
    private int counter = 0;
    private List<string> keys = new List<string>(){ "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
    private Dictionary<string, Vector3> locationOffset = new Dictionary<string, Vector3>()
    {
        { "BridgeN", new Vector3(0, 0, 3292)},
        { "BridgeS", new Vector3(0, 0, -3052)},
        { "BridgeE", new Vector3(3790, 0, 0)},
        { "BridgeW", new Vector3(-3888, 0, 0)},
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
        Transform finalBridge = bridges.Where(b => b.gameObject.name == "BridgeS").First();
        finalBridge.gameObject.SetActive(true);

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
            bridgeG.gameObject.SetActive(true);
            free.Remove(bridgeG);
            b = bridgeG;
        }

        return new Tuple<GameObject, Transform>(current, b);
    }
}
