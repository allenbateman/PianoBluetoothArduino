using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading; 

public class Bluetooth : MonoBehaviour
{
    public SerialPort mySerialPort;
    public Thread serialThread;
    int baudRate = 9600;
    int readTimeOut = 200;
    int bufferSize =32;

    char[] recivedChars;
    int[] recivedBytes;

    float frequency;
    [HideInInspector]
    public float incomingDistance;

    public bool Connected;

    public bool sendingData;

    void Start()
    {
        mySerialPort = new SerialPort("COM5", 9600);
        recivedBytes = new int[bufferSize];
        recivedChars = new char[bufferSize];
        sendingData = false;
    }
    private void Update()
    {
        if(mySerialPort != null && !Connected)
        {
            CloseConnection();
        }
        if (Connected)
        {
            if (!sendingData)
            {
               
            }
        }
    }
    public void SetConnection()
    {
        if(mySerialPort != null && Connected)
        {
            CloseConnection();
            Debug.Log("Disconnection Succed");
        }else if(mySerialPort != null && !Connected)
        {
            Connect();
            Debug.Log("Connection Succed");
        }
    }
   public void Connect()
    {
        Debug.Log("Connection started");
        try
        {

            mySerialPort.BaudRate = baudRate;
            mySerialPort.ReadTimeout = readTimeOut;
            mySerialPort.WriteBufferSize = bufferSize;
            mySerialPort.DtrEnable = true;
            mySerialPort.RtsEnable = true;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            mySerialPort.Handshake = Handshake.None;
            Connected = true;
            Debug.Log("Port Opened!");
        }
        catch (SystemException e)
        {
            Debug.Log("Error opening = " + e.Message);
        }
    }
    void Readbuffer()
    {
        if (mySerialPort != null && mySerialPort.IsOpen)
        {
            try
            {
               float.TryParse(mySerialPort.ReadLine(),out incomingDistance);
                Debug.Log("UltraSonic Sensor: " + incomingDistance);

            }
            catch (TimeoutException e)
            {
                Debug.LogWarning(e.ToString());
            }
        }

    }
    public static void DataReceivedHandler(
                    object sender,
                    SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        Debug.Log("Data Received:"+indata);
        // Console.WriteLine("Data Received:");
        // Console.Write(indata);
    }
    //manda un string con el mensaje, con writeLine escribimos en el puerto del arduino,
    //como si lo escribiesemos desde su consola
    public void SendFrequency(string msg)
    {
        if (mySerialPort != null && mySerialPort.IsOpen && sendingData)
        {
            try
            {

                // mySerialPort.Write(msg);
                mySerialPort.WriteLine(msg);
                Debug.Log("Mesaje enviado " + msg);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
        }
    }
    //cierra la conexión del puerto
    public void CloseConnection()
    {
        //control de errores 
        if (mySerialPort != null && mySerialPort.IsOpen)
        {
            try
            {
                mySerialPort.Close();
                Connected = false;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
        }
    }
}
