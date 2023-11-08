using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used for sending commands to the robot
// all functions on this script relies on the SendCommand function
// as server does different actions based on what message is sent
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
    public void controlGripper()
    {
        SocketController.SendCommand("GRIPPER");
    }
    public void moveJoints()
    {
        SocketController.SendCommand(sliderBehaviour.jointVals);
        learningMode = false;
    }
    public void moveJointsxyz()
    {
        SocketController.SendCommand("xyz," + sliderBehaviour.jointValsxyz);
        learningMode = false;
    }
    public void returnHome()
    {
        SocketController.SendCommand("HOME");
        learningMode = false;
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
