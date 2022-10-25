using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public int test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
