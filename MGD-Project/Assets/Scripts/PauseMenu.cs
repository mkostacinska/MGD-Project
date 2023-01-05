using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour //Tutorial used:https://www.youtube.com/watch?v=bxKEftSIGiQ&list=WL&index=9&t=604s
{
    public static bool Paused =false; //check if game is paused 
    public GameObject PauseCanvas;

    void Start()
    {
        Time.timeScale =1f;//default time scale 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//if the user pressed esc
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()//pause game
    {
        PauseCanvas.SetActive(true);
        Time.timeScale=0f;//freeze time
        Paused=true;
    }

    public void Play()//unpause game
    {
        PauseCanvas.SetActive(false);
        Time.timeScale=1f;//standard time flow
        Paused=false;
    }
    
    public void MenuM()
    {
        SceneManager.LoadScene("mainMenu");//load main menu is main menu buuton is pressed
    }
}
