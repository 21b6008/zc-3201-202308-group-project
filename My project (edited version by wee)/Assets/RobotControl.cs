using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotControl : MonoBehaviour
{
    public GameObject[] joints;
    public Slider[] jointInputs;
    public float[] jointSpeed = new float[8];
    public float stiffness = 100000F;
    public float damping = 40000F;
    public float forceLimit = 10000;

    void Start()
    {
        int i = 0;
        ArticulationBody[] articulationChain = this.GetComponentsInChildren<ArticulationBody>();
        foreach (ArticulationBody joint in articulationChain)
        {
            // Adds the MoveJoint script to the joint and initializes the speed.
            // Initialize stiffness, damping and forceLimit to all joints.

            MoveJoint moveJointScript = joint.gameObject.AddComponent<MoveJoint>();
            moveJointScript.speed = jointSpeed[i];
            ArticulationDrive currentDrive = joint.xDrive;
            currentDrive.stiffness = stiffness;
            currentDrive.damping = damping;
            currentDrive.forceLimit = forceLimit;
            joint.xDrive = currentDrive;
            i++;
        }
    }

    private float toDegrees(double angle)
    {
        angle = (180 / Math.PI) * angle;
        return (float)angle;
    }

    public void UpdateJointValues()
    {
        // Convert inputs from string/radians to float/degrees.
        // Sets newTarget to the degrees value of input.
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].GetComponent<MoveJoint>().newTarget = toDegrees(jointInputs[i].value);
        }
    }
    public void UpdateJointValuesFromRobot(String[] jointVals)
    {
        // Convert inputs from string/radians to float/degrees.
        // Sets newTarget to the degrees value of input.
        for (int i = 0; i < 6; i++)
        {
            joints[i].GetComponent<MoveJoint>().newTarget = toDegrees(float.Parse(jointVals[i]));
        }
    }
}