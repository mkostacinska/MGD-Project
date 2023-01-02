using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour //https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=1
{
    [SerializeField] private Slider VolumeSliderUI=null;//UI slider
    [SerializeField] private Text VolumeText=null;//UI text

    private void Start()
    {
        LoadValues();
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
        LoadValues();
  
    }

    void LoadValues()
    {
        float volumeValue=PlayerPrefs.GetFloat("gameValue");
        VolumeSliderUI.value=volumeValue;
        AudioListener.volume=volumeValue;//audioListener effects/controls game volume 
    }


}
