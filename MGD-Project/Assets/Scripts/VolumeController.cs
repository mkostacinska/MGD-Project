using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour //https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=2- Accessed 11/2022, published 6/2021, used for volume slider and volume controller , SpeedTutor on YouTube.
{
    [SerializeField] private Slider VolumeSliderUI=null;//UI slider
    [SerializeField] private Text VolumeText=null;//UI text

    private void Start()
    {
        LoadVolume();
    }

    public void VolumeSlider(float volume)
    {
        VolumeText.text=volume.ToString("0.0");//UI text
    }

    public void SaveVolume()
    {
        //Debug.Log("Saved");
        float volumeValue=VolumeSliderUI.value;//slider posistion determines volume
        PlayerPrefs.SetFloat("gameVolume",volumeValue);
        LoadVolume();
  
    }

    void LoadVolume()//get game audio to be new volume
    {
        float new_volumeValue=PlayerPrefs.GetFloat("gameValue");
        VolumeSliderUI.value=new_volumeValue;
        AudioListener.volume=new_volumeValue;//audioListener effects/controls game Audio
    }


}
