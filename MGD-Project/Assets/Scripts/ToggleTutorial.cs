using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    public GameObject tutorial;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            tutorial.gameObject.SetActive(!tutorial.gameObject.activeSelf);
        }
    }
}
