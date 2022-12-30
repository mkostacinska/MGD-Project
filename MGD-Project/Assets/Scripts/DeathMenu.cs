using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //when the restartButton is pressed, reload the main scene
    public void restartButton()
    {
        SceneManager.LoadScene("Main");
    }


    public void Quit()
    {
        //Application.Quit();
        //Debug.Log("Quit");
        SceneManager.LoadScene("credits");
    }// tutorial used :https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=13

    public void Menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
