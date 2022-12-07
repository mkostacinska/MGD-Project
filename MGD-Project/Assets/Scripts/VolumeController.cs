using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour //https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=1
{
    [SerializeField] private Slider VolumeSliderUI=null;
    [SerializeField] private Text VolumeText=null;

    private void Start()
    {
        LoadValues();
    }

    public void VolumeSlider(float volume)
    {
        VolumeText.text=volume.ToString("0.0");
    }

    public void SaveVolume()
    {
        //Debug.Log("Saved");
        float volumeValue=VolumeSliderUI.value;
        PlayerPrefs.SetFloat("gameVolume",volumeValue);
        LoadValues();
  
    }

    void LoadValues()
    {
        float volumeValue=PlayerPrefs.GetFloat("gameValue");
        VolumeSliderUI.value=volumeValue;
        AudioListener.volume=volumeValue;
    }


}
