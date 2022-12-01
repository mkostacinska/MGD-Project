using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }//https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=13
}
