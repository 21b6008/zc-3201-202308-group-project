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
    static WebSocket websocket2;

    // height and width are the values used to create our Texture2D for image streaming,
    // the values are as such as this is the size of the image sent by server and Texture2D
    // can only load byte array that matches it's size
    int height = 640;
    int width = 480;

    // Result is the text that is on the button
    public Text Result;

    // image is where the byte array we receive from server will be loaded into
    public RawImage image;

    // Host and Port is public InputField to allow user to change the
    // Host IP Address and Port number on the interface
    public InputField Host;
    public InputField Port;
    public InputField Port2;

    // RobotControl Script to move digital twin when receiving joint values from server
    public RobotControl RobotControl;
    // RobotController Script to turn learning mode off when disconnecting
    public RobotController RobotController;

    // Update is called once per frame
    async void Update()
    {
        time += Time.deltaTime;
        // checks if user wants to start socket and if socket has already started
        if (startSocket && !socketReady)
        {
            Result.text = "Disconnect from Socket";
            // socketUrl takes the text value of Host and Port InputFields to create the websocket url
            //String socketUrl = "ws://" + Host.text + ":" + Port.text;
            String socketUrl2 = "ws://" + Host.text + ":" + Port2.text;
            //Debug.Log(socketUrl);
            Debug.Log(socketUrl2);
            // Gets the new websocket by calling StartClient()
            //websocket = StartClient(socketUrl);
            websocket2 = StartClient(socketUrl2);
            // declare what code to run on the received message from server
            websocket2.OnMessage += (bytes) =>
            {
                // checks that the length of the message is less than 35, 
                // this is due to the only other message received will be
                // image which is much longer, so we check it is <35
                // to distinguish if it is joint values or image byte array
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
            await websocket2.Connect();
        }
        // else check if startsocket is false meaning we want to close it
        // and that socket is open, so that there is a socket to close
        else if (!startSocket && socketReady)
        {
            // checks if websocket is open
            // we do this because update may call this block of code and try to close it
            // as it has already entered it once but has not updated socketReady
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
            // checks if websocket is open and it has been more than 0.2 seconds since this code has been executed
            if (websocket.State == WebSocketState.Open && time >= 0.2f)
            {
                // sets time back to 0 as time may be a lot more than 0.2 due to existing without
                // entering this block of code for some time
                time = 0.0f;
                SendCommand(".");
                // checks what platform this code is running on
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
    // Function to move the digital twin based on values received from server
    public void moveJointFollowRobot(String message)
    {
        // splits the message into an array
        String[] jointVals = message.Split(" ");
        // calls the method that moves the digital twin with the split message as parameter
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
