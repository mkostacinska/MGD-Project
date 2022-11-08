using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//code from following tutorial: https://www.youtube.com/watch?v=BLfNP4Sc_iA
public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void Sethealth(int health) {
        slider.value = health;
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }
}
