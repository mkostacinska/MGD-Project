using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverController : MonoBehaviour
{
    //when the restartButton is pressed, reload the main scene
    public void restartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
