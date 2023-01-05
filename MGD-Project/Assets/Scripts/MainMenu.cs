using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");//load main
    }

    public void Quit()
    {
        //Application.Quit();
        //Debug.Log("Quit");
        SceneManager.LoadScene("credits");
    }//tutorial used:https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=14 - Accessed 11/2022, published 6/2022, used for menus(death , main), DB Dev on YouTube.
}
