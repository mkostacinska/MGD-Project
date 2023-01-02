using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour// based on https://www.youtube.com/watch?v=CE9VOZivb3I&list=WL&index=2
{
    public Animator transistion;
    public float TTime =1f;//transistion time 

    public void LoadNextLevel(){
        StartCoroutine(levelLoad(SceneManager.GetActiveScene().buildIndex+1));//load next scene
    }
   
   IEnumerator levelLoad(int levelIndex)
   {
     transistion.SetTrigger("Start");//starts animation
     yield return new WaitForSeconds(TTime);//wait for transistion time
     SceneManager.LoadScene(levelIndex);//load next scene
   }

}
