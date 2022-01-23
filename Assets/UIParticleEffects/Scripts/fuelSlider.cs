using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuelSlider : MonoBehaviour
{
    public float maxFuel;
    public float fuel;
    public float pityFuel=5;
    public Slider slider;
    public GameObject loseScreen;

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
        if (change>0)
        {
            FindObjectOfType<AudioManager>().play("fuelRefill");
        }

        //checks if we lose
        if (fuel<=0-pityFuel)
        {
            loseScreen.SetActive(true);
        }
    }


}
