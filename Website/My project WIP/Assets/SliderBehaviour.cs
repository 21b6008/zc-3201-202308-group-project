using System;
using UnityEngine;
using UnityEngine.UI;

// This script is used to constantly update and display the value of joints when we move the slider
public class SliderBehaviour : MonoBehaviour {
    public Text[] myText;
    public Slider[] mySlider;
    public String jointVals;
    public String jointValsxyz;

    void Update()
    {
        jointVals = Math.Round(mySlider[0].value, 2) + " " + Math.Round(mySlider[1].value, 2) + " " + Math.Round(mySlider[2].value, 2) + " " + Math.Round(mySlider[3].value, 2) + " " + Math.Round(mySlider[4].value, 2) + " " + Math.Round(mySlider[5].value, 2);
        for (int i = 0; i < myText.Length; i++)
        {
            myText[i].text = "Joint " + (i + 1) + ": " + Math.Round(mySlider[i].value, 2);
        }

        jointValsxyz = Math.Round(mySlider[8].value, 2) + " " + Math.Round(mySlider[7].value, 2) + " " + Math.Round(mySlider[6].value, 2) + " " + Math.Round(mySlider[9].value, 2) + " " + Math.Round(mySlider[10].value, 2) + " " + Math.Round(mySlider[11].value, 2);

    }
    
    public void moveslider(string[] jointval)
    {
        for (int i = 0; i < 6; i++)
        {
            mySlider[i].value = (float.Parse(jointval[i]));
        }
    }
}