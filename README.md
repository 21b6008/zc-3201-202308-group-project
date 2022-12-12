# **Web and Mobile Apps for Remote Control of a 6-DoF Robotic Arm**

## **Description**:
The School of Digital Science has acquired a Niryo NED 6 DOF robotic arm. The aim of this project is to develop a system that can control the robotic arm, there are three main parts to this project:
<br>
1. Web development
2. Mobile development
3. Server and robot control

<p style="text-align: center;"><img src="NiryoNed250x250.jpg"/></p>

## **Setup Instructions**
- Web
- Mobile
- Server

## Web
### Installing Unity
Firstly, we need to install Unity Hub. After installing Unity Hub we can install the version of Unity Editor we want which is 2021.3.10f1. For the installation of the Unity Editor make sure to have WebGL ticked, as we will be needing that to build our WebGL app.

### Opening the Project
To open the project that was cloned from the github on the Unity Hub app, click on the add project from disk button and find the folder under Website named My project. After adding the folder the project should now appear in the Unity Hub list, from this list we can click on the project to open it.

### Loading the Scene
Upon opening the project you will notice that the scene is currently empty. To load the created scene, we must open the scenes folder under assets, where we will find the SampleScene. Double click on the SampleScene to load in the created scene.

### Building the Project
To build the project we open the file category on the menu and click on Build Settings. Once we've opened build settings, we now need to change the target build platform to WebGL platform, this can be done by clicking on WebGL under the Platform list and then clicking on switch platform on the bottom right. This will take some time. Once the platform has been switched, we can now build the WebGL app, to do so we click on build and it will ask us to select a folder where the build will go. Building the project will take a few minutes.

### Running the Project
Now that we have built the website to run it we have to place all the files inside the folder our server is running on or start our server within the folder the build was created in. When running the website make sure that the Python Socket Server is running as the connecting to the socket will fail otherwise and you will need to reload the webpage and connect once the Python Socket Server is running, as the project will need to first connect to the Socket before any other command can be used. To connect the website to the Python Socket, first fill in the IP address and port where the Python Socket is hosted and then click on the Connect to Socket button. Now we can control the button by moving the 6 sliders to change the joint values and click on the "Move Joint" button to move the robot arm with the values in the sliders or we can control the gripper by clicking on the "Open/Close Gripper" button. In addition to this you can press the learning mode button to turn learning mode on or off, and finally there is a button to return the robot arm to the home position. After you are done using the robot arm do not forget to disconnect from the socket.

## Mobile

### Installing Unity:
1.	Install Unity Hub from a browser.
2.	After installing, launch Unity Hub and choose Unity version 2021.3.8f1 to be installed. If successfully found and installed, skip step 3.
3.  If the Unity version could not be found on the Unity Hub itself, install the 2021.3.8f1 version from this link:
    https://unity3d.com/get-unity/download/archive
4.	After being directed to “Add modules to your install”, tick Android Build Support.
5.	Complete installation of Unity 2021.3.8f1.

### Pulling Unity project from GitHub:
1.	In “zx-3201-202208-group-c-assignment2” repository, click Mobile Application file.
2.	Pull “Ned Controller” project from the Mobile Application file in GitHub to your GitHub desktop.
3.	Copy project file from GitHub files to your own local disk.

### Opening project in Unity:
1.	Run Unity 2021.3.8f1.
2.	In “Projects”, click “Open” and choose “Add project from disk”.
3.	Choose the copied “Ned Controller” project from your local disk to open project in Unity.

### Changing Socket script IP address and port number:
1.	Open “Ned Controller” project in Unity.
2.	Under “Assets”, click the C# script “SocketButton”.
3.	Modify the content by changing the IP address to your python server IP address and chosen port number of your python server.

### Setting up Unity settings suitable for Mobile Application build:
1.	Open “Ned Controller” project in Unity.
2.	Go to File and click “Build Settings”.
3.	Under “Scenes In Build”, tick “Scenes/SampleScene” for the built scene to be viewed in the later built mobile application.
4.	Under “Platform”, click Android.
5.	Click “Build” to build the mobile application apk into your disk under preferred name.

### Running built Unity Android Mobile Application apk on Android Mobile Phone:
1.	Connect your Android Mobile Phone to your pc with the phone cable.
2.	Copy the built mobile application apk saved from your disk to your Android Mobile Phone under “Downloads” file.
3.	After transferring completed, click “Downloads” files on your phone and install the Unity Mobile apk.
4.	After installation completed, find the app on your phone and you can now control the Ned robot arm through your phone.

## Digital Twin
In case the digital twin from the web or mobile applications fail to load the correct properties, here are a list of things you need to change. If not, just skip towards the Server part.

### niryo_one Game Object
The `niryo_one` game object can be found in the hierarchy window. Here you need to head to its child `base_link` and check `Immovable` in one of its `Articulation Body` properties.

Since Niryo Ned's URDF is not readily available, we are using Niryo One instead as both are very similar. As a result, we need to change its minimum and maximum joint values to fit our Niryo Ned robot. You can do this by expanding the niryo_one game object and selecting each of its links. Change its joint limits by going to its `Articulation Body` and changing `Lower Limit` and `Upper Limit` below `X Drive`. Here are the six joints and their limits:
- shoulder_link : `-170` - `170`
- arm_link : `-120` - `35`
- elbow_link : `-77` - `90`
- forearm_link : `-120` - `120`
- wrist_link : `-100` - `55`
- hand_link : `-145` - `145`

### Robot Control Script
This script's properties can be accessed through the `niryo_one` game object. In case the `Joints` are not properly set, you need to drag each of the six robot links to its fields in order. The six links are already discussed above.

Next, its `Joint Speed`, there are a total of eight elements, however we will be only using six of them, corresponding to the six joints. From `Element 1` through `Element 6`, the values as of testing are: 52, 40, 50, 62.8, 62.8, 62.8.

## Server
To start the server, you need to run two things:
1. server.py
2. A python web server

You first need to install several things before running **server.py**.
### Setting up your environment
Firstly, set up a virtual environment on the main directory. You can install **miniconda** from https://docs.conda.io/en/main/miniconda.html to set up the virtual environment.

Once installed, search for **Anaconda Prompt** and open it. In the terminal that opens, type:
```
conda create -n pyniryo_env python=3
```
This creates a virtual environment named 'pyniryo_env' and automatically installs the newest version of Python 3. To activate the new environment:
```
conda activate pyniryo_env
```

### Installing required libraries
**PyNiryo**

Once the environment is created, we need to install PyNiryo. PyNiryo is a package to control the robot using the Python language.

Go to the newly created environment in the Anaconda Prompt, and use the following commands:
```
pip install numpy
pip install pyniryo
```
You can now begin controlling the robot with Python. You can get the full documentation of PyNiryo from here https://docs.niryo.com/dev/pyniryo/v1.1.2/en/index.html.

**Other libraries**

You will also need other libraries to run the code flawlessly. Just run the following commands:
```
pip install socket
pip install websockets
pip install asyncio
pip install opencv-python
```
**socket**: This was originally used for client-server communication, but now only used to automatically get the machine's IP address.
<br>
**websockets**: Used for client-server communication.
<br>
**asyncio**: Used in conjunction with websockets.
<br>
**opencv-python (cv2)**: Used to encode robot stream image to jpg.

### Starting the server
Once set up, you are now ready to start the server. Either run the server.py file from your IDE or start up a terminal, go to the server folder and run the following command:
```
python server.py
```
Be sure to use the correct environment or python interpreter in your IDE/Text Editor.

### Python Web Server
For web applications, you also need to set up the web server to host the website. The files needed for the website is provided by the web developer, you just need to place the files in a folder. To run the web server, open a terminal and redirect to the server directory, then run:
```
python -m http.server
```
Congrats! If you follow all the instructions correctly, the server should now be running and ready to serve both web and mobile applications.
