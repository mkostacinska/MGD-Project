using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public static void slowTime(float seconds, float amount = 0f, bool screen = true)
    {
        GameObject.FindGameObjectsWithTag("SlowTime")[0].GetComponent<SlowTime>().slowTimeNonStatic(seconds, amount, screen);
    }
    public static void slowTime(float seconds, bool screen) //method overloading
    {
        slowTime(seconds, 0f, screen = true);
    }

    //method to slow time: pass in seconds and amount; freezes time by default if only seconds specified
    public void slowTimeNonStatic(float seconds, float amount = 0f, bool screen = true) { StartCoroutine(iSlowTime(seconds, amount, screen)); }

    [SerializeField] private GameObject freezeMenu;
    private IEnumerator iSlowTime(float seconds, float amount, bool screen)
    {
        //When timescale = 1: game is running at normal speed
        //if amount is 0: game is frozen
        Time.timeScale = amount;    //set the game speed
        GameObject freezeScreen = null;
        if (screen == true) { freezeScreen = Instantiate(freezeMenu); }

        yield return new WaitForSecondsRealtime(seconds);

        Time.timeScale = 1f;    //resume time
        if (screen == true) { Destroy(freezeScreen); }
    }
}
