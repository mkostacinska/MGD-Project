using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour //modification of VolumeController
{
    [SerializeField] private Slider DifficultySliderUI = null;//UI slider
    [SerializeField] private Text DifficultyText = null;//UI text
    private int defaultDifficulty = 1;
    private int difficultyValue;

    private void Start()
    {
        LoadValues();
        DifficultySlider(); //set initial text before player moves the slider
        DifficultySliderUI.maxValue = 3;    //set number of difficulty modes here (n + 1) modes
    }

    public void DifficultySlider()  //triggers whenever the slider moves
    {
        SaveDifficulty();
        string text = "Default";
        switch (difficultyValue)
        {
            case 0:
                text = "Easy";
                break;

            case 1:
                text = "Medium";
                break;

            case 2:
                text = "Hard";
                break;

            case 3:
                text = "Hardcore";
                break;
        }
        DifficultyText.text = text;//UI text
    }

    public void SaveDifficulty()
    {
        //Debug.Log("Saved");
        difficultyValue = (int) DifficultySliderUI.value; //slider position determines difficulty, expects an int
        PlayerPrefs.SetInt("gameDifficulty", difficultyValue);
        LoadValues();

    }

    void LoadValues()
    {
        difficultyValue = PlayerPrefs.GetInt("gameDifficulty", defaultDifficulty);    //sets the difficulty to default difficulty if no player prefs exist
        DifficultySliderUI.value = difficultyValue;   //sets the slider value
        //call an external script to set the difficulty of game here
        PlayerToFollow.shared.difficulty = difficultyValue+1;
    }


}
