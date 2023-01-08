using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class makes sure the music is persistent between scenes
//modified code from http://answers.unity.com/answers/1260412/view.html
public class MusicClass : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        //if there is already a music source, delete this one: prevents duplicates whenever the menu loads
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) {
            Destroy(this.gameObject); 
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
