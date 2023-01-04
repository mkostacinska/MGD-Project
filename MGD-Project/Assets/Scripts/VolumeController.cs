using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour //https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=1
{
    [SerializeField] private Slider VolumeSliderUI=null;//UI slider
    [SerializeField] private Text VolumeText=null;//UI text
    private float defaultVolume = 0.2f;
    
    private void Start()
    {
        LoadValues();
    }

    public void VolumeSlider(float volume)
    {
        VolumeText.text= ((int)(volume * 100)).ToString() + "%";//UI text
    }

    public void SaveVolume()
    {
        //Debug.Log("Saved");
        float volumeValue=VolumeSliderUI.value;//slider posistion determines volume
        PlayerPrefs.SetFloat("gameVolume",volumeValue);
        LoadValues();
  
    }

    void LoadValues()
    {
        float volumeValue=PlayerPrefs.GetFloat("gameVolume", defaultVolume);    //sets the volume to default volume if no player prefs exist
        VolumeSliderUI.value=volumeValue;
        AudioListener.volume=volumeValue;//audioListener effects/controls game volume 
    }


}
