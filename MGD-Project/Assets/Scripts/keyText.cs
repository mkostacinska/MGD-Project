using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyText : MonoBehaviour
{
    void Start()
    {
        PlayerToFollow.shared.pickupText = this.gameObject;
    }
}
