using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextPage : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    int currentPanel = 1;

    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }
    public void nextPanel()
    {
        currentPanel++;
        UpdatePanel();
    }
    public void backPanel()
    {
        currentPanel--;
        UpdatePanel();
    }

    void UpdatePanel()
    {
        if (currentPanel == 1)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
        }
        else if (currentPanel == 2)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
        }
        else if (currentPanel == 3)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
        }
    }
}
