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
    private int counter = 0;
    private List<string> keys = new List<string>(){ "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
    private Dictionary<string, Vector3> locationOffset = new Dictionary<string, Vector3>()
    {
        { "BridgeN", new Vector3(0, 0, 3292)},
        { "BridgeS", new Vector3(0, 0, -3052)},
        { "BridgeE", new Vector3(3790, 0, 0)},
        { "BridgeW", new Vector3(-4081, 0, 0)},
    };
    private int bridgeLen = 1685;

    // Start is called before the first frame update
    void Start()
    {
        SpawnIslands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnIslands()
    {
        //Spawns the first island as well as the final island to the north of it
        GameObject initial = Instantiate(island, position: Vector3.zero, rotation: Quaternion.identity, parent: parentIsland.transform);
        List<Transform> bridges = GetBridges(initial);
        print(bridges);
        Transform finalBridge = bridges.Where(b => b.gameObject.name == "BridgeS").First();
        finalBridge.gameObject.SetActive(true);
        //GameObject current = Instantiate(island, position: new Vector3(initial.transform.position.x, initial.transform.position.y, finalBridge.position.z - bridgeLen), Quaternion.identity, parent: parentIsland.transform);

        ////randomly generate each of the remaining islands
        GameObject prev = initial;
        List<string> occupied = bridges.Where(b => b.gameObject.active == true).Select(b => b.gameObject.name).ToList();
        var keys = new List<string>() { "BridgeN", "BridgeE", "BridgeS", "BridgeW" };
        
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
        //List<string> occupied = GetBridges(prev).Where(b => b.gameObject.active == true).Select(b => b.gameObject.name).ToList();
        GameObject current = Instantiate(island, position: prev.transform.position + locationOffset[leading.gameObject.name], rotation: Quaternion.identity, parent: parentIsland.transform);
        var free = GetBridges(current).Where(b => b.gameObject.name != keys[(keys.IndexOf(leading.gameObject.name) + 2) % 4]).ToList();

        //var gen = Random.Range(1, 3);
        var gen = 1;
        Transform b = null;
        if (counter < numberOfIslands)
        {
            for (int i = 1; i <= gen; i++)
            {
                var bridgeG = free[UnityEngine.Random.Range(0, free.Count())];
                bridgeG.gameObject.SetActive(true);
                free.Remove(bridgeG);
                b = bridgeG;
                //SpawnNext(current, bridgeG);
            }
        }

        return new Tuple<GameObject, Transform>(current, b);
    }
}
