using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches between each page of the tutorial.
/// Requires the children (pages) to be in the correct order in the heirarchy.
/// transform.GetChild(0) is reserved for TutorialStart GameObject.
/// </summary>
public class nextPage : MonoBehaviour
{
    int currentPanel = 1;   //same as the child number (starts at one because child 0 is reserved)
    
    void Start() { UpdatePanel(); }

    public void nextPanel() //Increments next panel and updates
    {
        currentPanel++;
        UpdatePanel();
    }
    public void backPanel() //Decrements next panel and updates
    {
        currentPanel--;
        UpdatePanel();
    }

    /// <summary>
    /// Enables the correct page
    /// </summary>
    void UpdatePanel()
    {
        if (currentPanel > transform.childCount)    //Read all pages
        {
            Destroy(transform.parent.gameObject);   //Destroy the Tutorial Object
        }

        int i = 0;                             //used to loop through all the children
        foreach (Transform panel in transform) //gets all the children
        {
            if (panel.name == "TutorialStart") { }              //if the child is TutorialStart, ignore
            else if (i == currentPanel)                         //if the child is current panel set to active
            {
                panel.gameObject.SetActive(true);
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
            i += 1;
        }
    }
}
