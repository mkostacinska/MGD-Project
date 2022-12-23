using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour// based on https://www.youtube.com/watch?v=CE9VOZivb3I&list=WL&index=2
{
    public Animator transistion;
    public float TTime =1f;

    public void LoadNextLevel(){
        StartCoroutine(levelLoad(SceneManager.GetActiveScene().buildIndex+1));
    }
   
   IEnumerator levelLoad(int levelIndex)
   {
     transistion.SetTrigger("Start");
     yield return new WaitForSeconds(TTime);
     SceneManager.LoadScene(levelIndex);
   }

}
