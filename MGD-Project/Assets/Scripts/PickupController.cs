using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private float heightSpeed = 2f;
    [SerializeField] private float deltaHeight = 0.1f;
    [SerializeField] private float offset = 0.5f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject text;
    [SerializeField] private TextMeshProUGUI canvasLabel;

    private int collected = 0;

    // Start is called before the first frame update
    void Start()
    {
        text.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        text.transform.eulerAngles = text.transform.eulerAngles + new Vector3(45, 0, 0); //set the angle of the text so that it faces the camera
    }

    // Update is called once per frame
    void Update()
    {
        rotateObject();
        checkDistance();
    }

    void checkDistance()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            text.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                text.SetActive(false);
                transform.gameObject.SetActive(false); //disable the pickup and increase the total
                collected += 1;
                updateUILabel();
            }
        }
        else
        {
            text.SetActive(false);
        }
    }


    void rotateObject()
    {
        //rotate the object around the world y axis
        Vector3 rotation = new Vector3(0, 0.03f, 0);
        transform.eulerAngles = transform.eulerAngles + rotation;

        //make the object move up and down
        Vector3 current = transform.position;
        float newY = Mathf.Sin(Time.time * heightSpeed);
        transform.position = new Vector3(current.x, (newY * deltaHeight) + offset, current.z); //change y to new position (offset by initial Y position!)
    }

    void updateUILabel()
    {
        canvasLabel.text = "keys: " + collected + "/3";
    }
}
