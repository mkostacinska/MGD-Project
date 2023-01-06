using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public static void slowTime(float seconds, float amount)
    {
        GameObject.FindGameObjectsWithTag("SlowTime")[0].GetComponent<SlowTime>().slowTimeNonStatic(seconds, amount);
    }

    //method to slow time: pass in seconds and amount; freezes time by default if only seconds specified
    public void slowTimeNonStatic(float seconds, float amount = 0f) { StartCoroutine(iSlowTime(seconds, amount)); }

    [SerializeField] private GameObject freezeMenu;
    public IEnumerator iSlowTime(float seconds, float amount)
    {
        //When timescale = 1: game is running at normal speed
        //if amount is 0: game is frozen
        Time.timeScale = amount;    //set the game speed
        GameObject freezeScreen = Instantiate(freezeMenu);
        print("freezing");

        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;    //resume time
        Destroy(freezeScreen);
    }
}
