using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour //Tutorial used:https://www.youtube.com/watch?v=bxKEftSIGiQ&list=WL&index=10&t=604s- Accessed 11/2022, published 10/2022, used for pause menu, DB Dev on YouTube.
{
    public static bool Paused =false; //check if game is paused 
    public GameObject PauseCanvas;

    void Update()
    {
        if (InputManager.getActionMap().FindAction("Pause").triggered)
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

    public void Play()//unpause game/play game
    {
        PauseCanvas.SetActive(false);
        Time.timeScale=1f;//standard time flow
        Paused=false;
    }
    
    public void MenuM()
    {
        Play(); //so the game isn't frozen
        SceneManager.LoadScene("mainMenu"); //load main menu is main menu button is pressed
    }
}
