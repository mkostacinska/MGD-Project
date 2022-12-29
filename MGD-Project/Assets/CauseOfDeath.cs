using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CauseOfDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //sets the text to the "death by" stored in player preferences
        //if there is no data, returns "Unknown which is the default value"
        this.gameObject.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Death by", "Unknown");
    }
}
