using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

     void OnTriggerEnter(Collider player)
    {
        Time.timeScale = 0f;
        
        if (GameObject.FindGameObjectWithTag("Player").tag == "Player")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Resume();
        }
    }

    void Resume()
    {
        Time.timeScale = 1f;
    }
}
