using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour// based on https://www.youtube.com/watch?v=CE9VOZivb3I&list=WL&index=2&t=614s - Accessed 12/2022, published 1/2020, used for scene transistions, brackeys on YouTube
{
    public float TTime =1f;//transistion time 
    public Animator transistion;


    public void LoadNextScene(){
        StartCoroutine(sceneLoad(SceneManager.GetActiveScene().buildIndex+1));//load next scene
    }
   
   IEnumerator sceneLoad(int levelIndex)
   {
     transistion.SetTrigger("Start");//starts animation
     yield return new WaitForSeconds(TTime);//wait for transistion time
     SceneManager.LoadScene(levelIndex);//load next scene
   }

}
