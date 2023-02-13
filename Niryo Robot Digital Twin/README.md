# Digital Twin
This directory contains the Unity project for the testing of the digital twin. Read further to import the digital twin to your project.

## Getting the URDF Importer package
The URDF Importer package is used to import URDF files into Unity. It also contains scripts for the robot. (Your project will have lots of warning signs if this is not added.)
<br>
To add the package to your project: 

1. Go to the menu bar -> `Window` -> `Package Manager`
2. Click the `+` button and `Add package from git URL...`
3. Copy and paste this link into the new window: https://github.com/Unity-Technologies/URDF-Importer.git?path=/com.unity.robotics.urdf-importer#v0.5.2
<br> You should have the package now.
If you need any help, check out this repo: https://github.com/Unity-Technologies/URDF-Importer

## Importing the digital twin into your project
In this directory (Niryo Robot Digital Twin), there is a file called `NiryoOnePrefab.unitypackage`. This is a package that contains a prefab of the digital twin in this testing project. To import it to your project:

1. Go to your project.
2. Go to the menu bar -> `Assets` -> `Import Package` -> `Custom Package...`
3. Find and select `NiryoOnePrefab.unitypackage` to open.
4. Make sure to select all dependencies in the following window and import.
5. The robot prefab should now be in your Assets. Drag the prefab to the Hierarchy or Scene window to get the robot.

### Changing `Robot Control` script
You should change the `UpdateJointValues()` function in this script so that the twin can follow the real robot's movements. Right now it is setup to read inputs from the user and change its joint values that way. If you already have a functioning script to get the real robot's joint values, you can instead use those values in a list to move the digital twin: 

1. You need to pass the joint values you got from the robot into the function. So change the function header.<br>
E.g: `public void UpdateJointValuesFromRobot(String[] jointVals)`
2. You can remove the first line, which has `inputInRad`.
3. Change `jointInputs[i].text` in the loop into `jointVals[i]` or your preferred name for the passed values.
<br>
In Unity, you can access this function from another script to move the digital twin. Example: <br>
```C#
// Function to move the digital twin based on values received from server
    public void moveJointFollowRobot(String message)
    {
        // splits the message into an array
        String[] jointVals = message.Split(" ");
        // calls the method that moves the digital twin with the split message as parameter
        RobotControl.UpdateJointValues(jointVals);
    }
```