using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoint : MonoBehaviour
{
    public float speed;
    public float newTarget;
    public ArticulationBody joint;

    void Start()
    {
        joint = this.GetComponent<ArticulationBody>();
    }

    void FixedUpdate()
    {
        ArticulationDrive currentDrive = joint.xDrive;
        float newTargetDelta = Time.fixedDeltaTime * speed;

        // Check if the appointed target joint value is beyond the limits
        if (newTarget > currentDrive.upperLimit)
        {
            newTarget = currentDrive.upperLimit;
        }
        else if (newTarget < currentDrive.lowerLimit)
        {
            newTarget = currentDrive.lowerLimit;
        }

        // Change the joint target value
        if (currentDrive.target < newTarget)
        {
            currentDrive.target += newTargetDelta;

            if (currentDrive.target > newTarget)
            {
                currentDrive.target = newTarget;
            }
        }
        else if (currentDrive.target > newTarget)
        {
            currentDrive.target -= newTargetDelta;

            if (currentDrive.target < newTarget)
            {
                currentDrive.target = newTarget;
            }
        }

        // Apply the value to the joint to rotate 
        joint.xDrive = currentDrive;
    }
}
