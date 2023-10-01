This README file provides instructions on how to set up the server (robot) to use the web and mobile applications to control the robot.


Authors:
21B6015 Wee Chee Hung [LEADER]
21B6004 Mohammad Aiman Safwan Bin Abdullah
21B6008 Nur Wajihah Nadhirah Binti Alidon


Contents: 
* Connect and control robot on Web application
* Connect and control robot on Mobile phone application
* Setting up robot/server




WEB APPLICATION [Wee]


Before beginning, make sure to have ;
* A working computer 
* An active wifi connection to G38B


If Unity Hub is not installed on the computer;
* Download and install Unity Hub from the Unity website (https://unity.com/download)
* After installing Unity Hub on the device, install the unity version 2021.3.10f1, since this is the version we have been using to build the web application. 
* During the installation process of unity version 2021.3.10f1, make sure to have the option for webGL ticked as we will be needing that to build the web application 
* To open the project cloned from the Github repo, we need to click on the add project from disk button on the top right of unity hub. 
* Search for the folder named ‘my project’ under the website folder and select that folder


Building the project;
1. When the project is first opened, the scene is empty, and to fix this we first need to go to the scenes folder under assets, and double click on sample scene. 
2. Click on the file tab located on the top left of unity and click on build settings
3. A window should open up after clicking on build settings, select the webGL platform in target build platform
4. click on WebGL under the Platform list and then click on switch platform on the bottom right
5. After switching the platform, we can build the project by clicking on files and build. It will ask for where to save the build


Running the web application;
1. Make sure all the files are placed inside the same folder where our server is running on
2. Open up a web browser
3. Enter "localhost:8000” if it is on the PC running the server, and “192.168.1.102:8001” if it is from another device connected on the same network, into the search bar 
4. The web application should open up as long as the computer is connected to the G38B network and the server is running on the same network. 
5. Fill in the IP(192.168.1.102) and Port number(8001) and click on the connect socket button
6. Move the robot by changing the slider and click on the move joints button to confirm the movement 
7. We can control the gripper by clicking on the open/close gripper button 
8. Home button returns the robot arm to its initial position 
9. Repeat steps 2-5 to connect to the robot from different devices from a web browser


Troubleshooting: 
* Double-check the IP address and port number entered when connecting to socket
* Ensure both the computer and the robot are connected to the same network. 
* Ensure that the server is running
* Check for any error messages or logs shown in server terminal






MOBILE PHONE APPLICATION [Wajihah]


Before beginning, make sure to have;
* A working Android mobile phone
* An active wifi connection (Ethernet G38B)
* Unity APK installed in the phone *


Opening project on Unity: 
1. On a desktop computer, download and install Unity Hub from browser (https://unity.com/download)
2. After installing Unity Hub, install the unity version 2021.3.8f1, make sure to tick option for ‘Android Build Support’ as we will be needing that to build the APK
3. Pull the “Ned Controller” project under “Mobile Application” file from GitHub repository to local disk
4. To open the project, click ‘Open’ and ‘Add project from disk’ on the top right corner
5. Select “Ned Controller” project from local disk


* If Unity APK is not in the phone storage, 
1. Launch Unity Hub on desktop 
2. Open the project file labeled “Ned Controller '' (make sure the Unity version is Unity 2021.3.8f1)
3. Click on file’ and select ‘Build Settings’ in the dropdown menu
4. Switch platform to “Android build”
5. Click ‘Build’ to build the mobile application for the target platform; Android
6. Once APK is installed on the desktop computer, transfer it to the mobile phone using the USB provided
7. Install APK in mobile phone


Starting the mobile application:
1. Ensure that the mobile phone is connected to the same network as the robot
2. Ensure the APK is installed successfully on mobile phone
3. Launch the mobile application
4. Enter the IP address and port number from the server (Note: IP address may vary so confirm again on the server side) at the top part of the interface
5. Click on “Connect” button at the bottom of the interface
6. Control the robot using the provided controllers on screen
7. To connect from multiple mobile devices, repeat steps 1-6


Troubleshooting: 
* Double-check the IP address and port number entered at the top part of the interface 
* Ensure both the mobile device and the robot are connected to the same network. 
* Ensure that the server is running
* Check for any error messages or logs shown in server terminal


ROBOT [Safwan]
To set up the robot, it is important to ensure that:
1. The robot is placed on a flat surface with enough space to account for its movements.
2. The robot must be plugged in and the wall plug is switched on.
3. Robot’s gripper is attached to the robot and wired properly. In this case, we are using the Adaptive Grippers.
4. The camera is installed properly on the robot and connected via the USB available on the robot..
5. The platform used is attached to the robot, enabling the calibration of the robot.
6. An ethernet cable is used, connecting the robot to the same network as the PC used for the demonstration.


After making sure all the above requirements are met, we can now switch on the robot. To do so, we simply flick the switch available at the back of the robot. The light indicator will now turn red. Wait until the light indicator turns blue, which indicates the robot is ready for a connection.


SERVER [Safwan]
To start the server, you need to run two things,
* A python web server
* server.py


Python web server
You are required to set up the web server to host the website. The files needed for the website are available after you finish building the project in the Web Application section. Place these files in a folder. This folder will be used as the directory to run the server.
To run the web server, navigate to the folder in which the web application files are located and open a terminal there. Alternatively, you can just open a terminal and navigate to the created folder. You are required to run the following command:


python -m http. server


For example: create a new folder “Website” on the desktop. Place the web application files in this folder and open a terminal. Navigate to /Website and run the command.


Setting up your environment
If the machine used to run this demonstration already has the virtual environment set up, you can skip this part of the documentation.
To set up a virtual environment to store the packages needed in the server.py file. You can install miniconda from https://docs.conda.io/en/main/miniconda.html to set up the virtual environment.


Once installed, search for Anaconda Prompt and open it. A terminal will open. Now we will create an environment using the environment file (pyniryo.yml) included in this repository. To do it, run:
conda env create -f pyniryo.yml


This creates a new virtual environment named “pyniryo_env” and installs the packages specified in the environment file. To activate the new environment, run:
conda activate pyniryo_env


Now we can use this environment to run the server.py file.


Packages used
The most important package in this virtual environment is the PyNiryo library. This library is used to communicate and control the Ned robot. You can get the full documentation of PyNiryo from here https://docs.niryo.com/dev/pyniryo/v1.1.2/en/index.html 


There are other packages used for the server-client communication:
socket: used to automatically get the machine’s IP address.
websockets: used for client-server communication.
asyncio: used in conjunction with websockets.
opencv-python (cv2): used to encode robot stream image to .jpg.


Starting the server
Once everything is set up, you are ready to start the server. Run the server.py file from the /Server directory in this repository. If you are using an IDE/text editor, make sure to select the correct interpreter (pyniryo_env). If you’re using the terminal to run the file, just use the previous conda terminal and navigate to /Server and run:
python server.py
As an additional note, an error may occur when running the server.py code due to a change in the robot’s IP address. To fix the issue, you can open NiryoStudio and search for the robot’s new IP address. Replace the existing IP address in the file with the new one from NiryoStudio.


Note: 
Regarding the camera issue, each of us have collectively attempted to solve the problem. We have tried different codes to alter and/or add to the existing codes in the SocketController.cs files (for web and mobile applications). However, there was little to no difference on the camera image not receiving in real-time.
Attached are the altered code snippets from each of us: 


Safwan;
Tried using multiple ports to get the image and robot commands sent through different ports. This method was abandoned as there was no point in doing so. Experimented with parallel processing through the use of “asyncio.gather”, which in theory allows two functions to be run simultaneously. However, through testing it is found that the same issue where the camera is not updated while the robot is moving still persists. This altered code can be found in the AIR_CS_LAB branch inside the /Server directory under the name server_copy.py. In this modified code, the previous image and robot joint sending lines are each converted into a separate async function.
# Define asynchronous functions for camera images and robot joint values
async def send_camera_images(websocket):
    image_data = stream_image(robot)
    await websocket.send(image_data)
    await asyncio.sleep(0.1)  # Adjust the sleep time as needed


async def send_robot_joint_values(websocket):
    joint_values = to_string(robot.joints).encode(FORMAT)
    await websocket.send(joint_values)
    await asyncio.sleep(0.1)  # Adjust the sleep time as needed


These functions are run using the asyncio.gather function inside the handler whenever the robot is connected.
async def handler(websocket):
    print(f"[NEW CONNECTION] New device connected.\n")
    robot.update_tool()
    robot.release_with_tool()   # Initialize the robot to open gripper
    gripper_open = True
    connected = True
   
   
   
    while connected:
        try:
           
            await asyncio.gather(send_camera_images(websocket),send_robot_joint_values(websocket))




Wee;
Tried multiple different codes and functions from libraries to try to fix the camera synching issue. I have uploaded one example of code testing to fix this issue from the website in the Github AIR_CS_LAB branch called SocketController Testing for camera improvement.cs. In this code, I have edited the code under async void update(), utilizing the function ‘await’ and ‘async’ to try to update the robots movement and to process the image from the webcam at the same time. However despite many attempts and testing, the result seems to be negligible as the image processing seems to still be quite laggy. 


websocket.OnMessage += async (bytes) =>
    {
        if (System.Text.Encoding.UTF8.GetString(bytes).Length < 35)
            {
                await MoveJointFollowRobotAsync(System.Text.Encoding.UTF8.GetString(bytes));
            }
            else
            {
                ProcessImage(bytes);
            }
    };


async Task MoveJointFollowRobotAsync(string message)
{
    // Assuming `moveJointFollowRobot` is an asynchronous method
    await Task.Run(() => moveJointFollowRobot(message));
}


void ProcessImage(byte[] imageData)
{
    Texture2D target = new Texture2D(height, width);
    target.LoadImage(imageData);
    image.GetComponent<RawImage>().texture = target;
}




Wajihah;
In the SocketController.cs script file, under the async void Update()function, I have added this line byte[] newbytes = GetCameraFrame(); to call a new function named GetCameraFrame(). 


The GetCameraFrame function captures a frame from the camera as it creates a new Texture2D object with specified dimensions (width x height). It also reads pixel data from the screen (from the robot’s camera), applies changes, and encodes it as a PNG image before returning it as a byte array.


Existing conditional logic code snippet under async void Update()function;
                if (System.Text.Encoding.UTF8.GetString(bytes).Length < 35)
                {
                    moveJointFollowRobot(System.Text.Encoding.UTF8.GetString(bytes));
                }
                else
                {
                    Texture2D target = new Texture2D(height, width);
                    target.LoadImage(bytes);
                    image.GetComponent<RawImage>().texture = target;
                }
            };


The conditional logic checks the length of the string representation of a byte array (bytes) obtained from GetCameraFrame. If the length is less than 35, it assumes that the byte array contains some kind of data that can be converted to a string and passed to moveJointFollowRobot. If length is 35 or greater, it treats the byte array as image data, creates a new Texture2D, loads the image, and assigns it to a RawImage.


GetCameraFrame();
byte[] GetCameraFrame()
    {
        // Create a new Texture2D with dimensions width x height,
        // which will hold the pixel data captured from the camera.
        Texture2D texture = new Texture2D(width, height);


        // Read pixel data from the screen (in this case, from the camera)
        // and assign it to the texture.
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);


        // Apply changes to the texture to make it ready for use.
        // call Apply() after ReadPixels() to ensure that the changes take effect.
        texture.Apply();


        // Encode the texture as a PNG image and return it as a byte array.
        return texture.EncodeToPNG();
    }




Meanwhile the rest of the codes in the script remain unchanged. 


In summary, GetCameraFrame() captures camera frames, while the conditional logic processes the obtained byte array based on its length. If the length is below 35, it treats it as something other than image data and sends it to moveJointFollowRobot. Otherwise, it treats it as image data and displays it on a RawImage. 


GetCameraFrame is responsible for obtaining camera frames, and the conditional logic decides how to process those frames based on their content length.


However, after running the file in Unity and executing the APK on the mobile phone, there were no significant changes to the camera image seen on screen, with the initial APK’s camera image. 


I have also pushed SocketControllerAltered.cs in the GitHub repository under the AIR_CS_LAB branch.