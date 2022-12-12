using System;
using UnityEngine;
using UnityEngine.UI;

// This script is used to constantly update and display the value of joints when we move the slider
public class SliderBehaviour : MonoBehaviour {
    public Text[] myText;
    public Slider[] mySlider;
    public String jointVals;

    void Update() 
    {
        // Updates the string of joint values based on current slider values to 2 decimal places
        jointVals = Math.Round(mySlider[0].value, 2) + " " + Math.Round(mySlider[1].value, 2) + " " + Math.Round(mySlider[2].value, 2) + " " + Math.Round(mySlider[3].value, 2) + " " + Math.Round(mySlider[4].value, 2) + " " + Math.Round(mySlider[5].value, 2);
        
        // loops through the text array and changes the content to match current slider value
        for (int i = 0; i < myText.Length; i++)
        {
            myText[i].text = "Joint " + (i + 1) + ": " + Math.Round(mySlider[i].value, 2);
        }
    }
}
