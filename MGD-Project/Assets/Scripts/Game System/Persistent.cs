using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class makes any object persistent between scenes as long as it is the only one with the name
public class Persistent : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //if there is already an object with this name, delete this one: prevents duplicates
        if (GameObject.Find(gameObject.name) != gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
