using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {

    }
     //void OnTriggerEnter(Collider player)
     //{
     //   if (GameObject.FindGameObjectWithTag("Player").tag == "Player")
     //   {
     //       Time.timeScale = 0f;
     //       uiObject.SetActive(true);
     //       Destroy(gameObject);
     //   }
     //}
    
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            uiObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
}
