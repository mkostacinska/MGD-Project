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
