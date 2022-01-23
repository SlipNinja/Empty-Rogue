using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuelSlider : MonoBehaviour
{
    public float maxFuel;
    public float fuel;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxFuel;
        slider.value = maxFuel;
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        changeFuel(-Time.deltaTime);
    }

    
    public void changeFuel(float change)
    {
        if (fuel+change<maxFuel) {
            fuel += change;
            slider.value = fuel;
        }
        else {
            fuel = maxFuel;
            slider.value = fuel;
        }
    }


}
