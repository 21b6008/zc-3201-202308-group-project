using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used for sending commands to the robot
public class RobotController : MonoBehaviour
{
    public Text buttonText;
    public bool learningMode = true;
    public SliderBehaviour sliderBehaviour;
    private void Update()
    {
        if (learningMode)
        {
            buttonText.text = "Learning Mode: On";
        }
        else
        {
            buttonText.text = "Learning Mode: Off";
        }
    }
    public void ControlGripper()
    {
        SocketController.SendCommand("GRIPPER");
    }
    public void moveJoints()
    {
        learningMode = false;
        SocketController.SendCommand(sliderBehaviour.jointVals);
    }
    public void returnHome()
    {
        learningMode = false;
        SocketController.SendCommand("HOME");
    }
    public void setLearningMode()
    {
        if (learningMode)
        {
            SocketController.SendCommand("LEARN_OFF");
            learningMode = false;
        }
        else
        {
            SocketController.SendCommand("LEARN_ON");
            learningMode = true;
        }
    }
}
