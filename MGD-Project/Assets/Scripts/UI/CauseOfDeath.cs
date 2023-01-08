using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Ran when Game Over scene runs
/// Sets the "Cause of death" text
/// </summary>
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
