using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothUI : MonoBehaviour
{
    Bluetooth bluetooth;

    public Text connectionState, deviceConnected; 
   


    void Start()
    {
        bluetooth = FindObjectOfType<Bluetooth>();
    }


    void Update()
    {
        if(bluetooth.mySerialPort.IsOpen)
        {
            connectionState.text = "Connected";
            deviceConnected.text = bluetooth.mySerialPort.PortName;
        }
        else
        {
            connectionState.text = "Disconected";
            deviceConnected.text = "No device";
        }
    }
}
