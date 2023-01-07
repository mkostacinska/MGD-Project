using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTutorialPage : MonoBehaviour
{
    public GameObject[] SwitchPage;
    [SerializeField] GameObject buttonLeft;
    [SerializeField] GameObject buttonRight;

    int currentIndex = 0;

    public int CurrentIndex
    {
        get
        {
            return currentIndex;
        }
        set
        {
            if (SwitchPage[currentIndex] != null)
            {
                GameObject activeObj = SwitchPage[currentIndex];
                activeObj.SetActive(false);
            }

            if (value < 0)
            { 
                currentIndex = SwitchPage.Length -1;
            }
            else if (value > SwitchPage.Length -1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = value;
            }
            if (SwitchPage[currentIndex] != null)
            {
                GameObject activeObj = SwitchPage[currentIndex];
                activeObj.SetActive(true);
            }
        }
    }

    public void Previous(int direction)
    {
        if (direction == 0)
        {
            currentIndex--;

            if (currentIndex <= 3)
            {
                buttonRight.SetActive(true);
            }

            if (currentIndex <= 0)
            {
                buttonLeft.SetActive(false);
            }
        }
    }

    public void Next(int direction)
    {
        if (direction >= 1)
        {
            currentIndex++;

            if (currentIndex >=4)
            {
                buttonRight.SetActive(false);
            }

            if (currentIndex >=1)
            {
                buttonLeft.SetActive(true);
            }
        }
    }

    public void OnClickPanel()
    {
        if (currentIndex <=0)
        {
            buttonLeft.SetActive(false);
            buttonRight.SetActive(true);
        }

        SwitchPage[0].SetActive(true);
    }


    //public void SwitchPage()
    //{
    //    GameObject panel1 = GameObject.Find("Panel1");
    //    GameObject panel2 = GameObject.Find("Panel2");

    //    if (panel1.activeInHierarchy)
    //    {
    //        panel1.SetActive(false);
    //        panel2.SetActive(true);
    //    }
    //    else 
    //    {
    //        panel1.SetActive(true);
    //        panel2.SetActive(false);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
