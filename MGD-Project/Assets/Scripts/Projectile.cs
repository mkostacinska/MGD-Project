using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float life = 3;

    //destroy the projectile after 3 seconds
    void Awake()
    {
        Destroy(gameObject, life);
    }
}
