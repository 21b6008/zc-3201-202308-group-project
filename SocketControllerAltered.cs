using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using NativeWebSocket;

public class SocketController : MonoBehaviour
{
    // time is used as a timer
    private float time = 0.0f;

    // We use static so that other functions and script can access our variables easily
    // These bool variables are used to control socket connect and disconnect
    static bool socketReady = false, startSocket = false;
    static WebSocket websocket;

    static int height = 640; //412
    static int width = 480;  //612

    // Result is the text that is on the button
    public Text Result;

    // image is where the byte array we receive from server will be loaded into
    public RawImage image;

    // Host and Port is public InputField to allow user to change the
    // Host IP Address and Port number on the interface
    public InputField Host;
    public InputField Port;

    // RobotControl Script to move digital twin when receiving joint values from server
    public RobotControl RobotControl;
    // RobotController Script to turn learning mode off when disconnecting
    public RobotController RobotController;

    private void Start()
    {
    }
    // Update is called once per frame
    async void Update()
    {
        time += Time.deltaTime;
        
        byte[] newbytes = GetCameraFrame(); // Call the GetCameraFrame() function. -wajihah

        // checks if user wants to start socket and if socket has already started
        if (startSocket && !socketReady)
        {
            Result.text = "Disconnect from Socket";
            //int PortNumber = int.Parse(Port.text) + 1;
            // socketUrl takes the text value of Host and Port InputFields to create the websocket url
            String socketUrl = "ws://" + Host.text + ":" + Port.text;
            // Gets the websocket by calling StartClient()
            websocket = StartClient(socketUrl);
            websocket.OnMessage += (bytes) =>
            {
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
            // Connects the created websocket to server
            await websocket.Connect();
        }
        else if (!startSocket && socketReady)
        {

            if (websocket.State == WebSocketState.Open)
            {
                // Closest the existing websocket connection
                CloseSocket();
                RobotController.learningMode = true;
                Result.text = "Connect to Socket";
            }
        }
        else if (socketReady)
        {
            if (websocket.State == WebSocketState.Open && time >= 0.1f)
            {
                time = time - 0.2f;
                SendCommand(".");
                #if !UNITY_WEBGL || UNITY_EDITOR
                    websocket.DispatchMessageQueue();
                #endif
            }
        }
        else
        {
            return;
        }
    }

    byte[] GetCameraFrame() //new function added -wajihah
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


    // Function to move the digital twin based on values received from server
    public void moveJointFollowRobot(String message)
    {
        // splits the message into an array
        String[] jointVals = message.Split(" ");
        // calls the method that moves the digital twin with the String[] as parameter
        RobotControl.UpdateJointValuesFromRobot(jointVals);
    }

    // inverts startSocket value
    public void controlSocket()
    {
        startSocket = !startSocket;
    }

    // StartClient creates the websocket that will be used for connection and returns it
    // Changes socketReady to true indicating that the socket is ready to be used
    public static WebSocket StartClient(String socketUrl)
    {
        websocket = new WebSocket(socketUrl);
        websocket.OnOpen += () =>
        {
            Debug.Log("WS connected");
        };
        socketReady = true;
        return websocket;
    }

    // CloseSocket sends the disconnect message to server and closes connection
    // Changes socketReady to false indicating that the socket is no longer ready to be used
    public async static void CloseSocket()
    {
        SendCommand("DISCONNECT");
        await websocket.Close();
        Debug.Log("WS disconnected");
        socketReady = false;
    }

    // SendCommand gets the byte version of message to be sent and sends the byte version to server
    public async static void SendCommand(String message)
    {
        byte[] msg = Encoding.ASCII.GetBytes(message);
        await websocket.Send(msg);
    }

    // When the application is closed this function is called
    void OnApplicationQuit()
    {
        // Check if websocket currently exists
        if (websocket != null && websocket.State != WebSocketState.Closed)
        {
            CloseSocket();
            RobotController.learningMode = true;
        }
        else
        {
            Debug.Log("Socket is already closed");
        }
    }
}