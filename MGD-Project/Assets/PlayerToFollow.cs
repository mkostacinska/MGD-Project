using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToFollow
{
    public static PlayerToFollow shared = new PlayerToFollow();
    public GameObject player;
    public GameObject pickupText;
    public int islandNum = 3;
}
