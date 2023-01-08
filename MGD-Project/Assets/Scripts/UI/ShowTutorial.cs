using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject uiObject;

    private void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

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
        if (InputManager.getActionMap().FindAction("Pause").triggered)
        {
            Time.timeScale = 1f;
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
