using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothTest : MonoBehaviour
{
    public static String deviceName = "HC-06-00";
    //public Text dataToSend;
    private static bool IsConnected;
    public static string dataRecived = "";
    // Start is called before the first frame update
    void Start()
    {
        IsConnected = false;
        BluetoothService.CreateBluetoothObject();
        StartConnection();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected) {
            try
            {
               string datain =  BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                }

            }
            catch (Exception e)
            {

            }
        }
        
    }

    public static void StartConnection()
    {
        if (!IsConnected)
        {
            //print(deviceName);
            IsConnected =  BluetoothService.StartBluetoothConnection(deviceName);
        }
    }

    public static void SendValue(String value)
    {
        if (IsConnected)
        {
            BluetoothService.WritetoBluetooth(value);
        }
    }

    //public static void RestartConnection()
    //{
    //    if (IsConnected)
    //    {
    //        BluetoothService.StopBluetoothConnection();
    //    }
    //    IsConnected = false;
    //    StartConnection();
    //}

    //public void SendButton()
    //{
    //    if (IsConnected && (dataToSend.ToString() != "" || dataToSend.ToString() != null))
    //    {
    //        BluetoothService.WritetoBluetooth(dataToSend.text.ToString());
    //    }
    //}


    public static void StopConnection()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        //Application.Quit();
    }
}
