using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //when try again (Button) is pressed, reload main scene
    public void respawnButton()
    {
        SceneManager.LoadScene("Main");
    }//modified from gameOverController script 


    public void Quit()
    {
        //Application.Quit();
        //Debug.Log("Quit");
        SceneManager.LoadScene("credits");
    }// tutorial used :https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=13

    public void Menu()
    {
        SceneManager.LoadScene("mainMenu");//reload main menu
    }
}
