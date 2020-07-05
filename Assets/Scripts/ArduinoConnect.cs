using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoConnect : MonoBehaviour
{
    //public static SerialPort sp = new SerialPort("/dev/cu.usbmodem14101", 9600);
    public static SerialPort sp = new SerialPort("/dev/cu.HC-06-00-DevB", 9600);
    //public static SerialPort sp = new SerialPort("/dev/cu.Bluetooth-Incoming-Port", 9600);
    string incomingData;

    // Start is called before the first frame update
    void Start()
    {
        OpenConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void OpenConnection()
    {
        if(sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                Debug.Log("Closing port, because it was already open");
                OpenConnection();
            }
            else
            {
                sp.Open();
                sp.ReadTimeout = 50;
                Debug.Log("Port opened");
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();
    }

    public static void SendValue(string message)
    {
        sp.Write(message);
    }

    public static void restartPort()
    {
        OpenConnection();
    }
}
