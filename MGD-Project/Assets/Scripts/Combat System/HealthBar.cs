using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//tutorial for this code has been credited in the README
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
