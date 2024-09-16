using System;
using System.IO.Ports;
using Unity.VisualScripting;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 9600); // Change COM3 to your Arduino's port
    int computers = 3;

    void Start()
    {
        sp.Open(); // Open the serial connection
        sp.ReadTimeout = 50;
    }

    void Update(){
        // if (Input.GetKeyDown(KeyCode.L)) {
        //     LoseLife();
        //     Debug.Log("LIFE LOST");
        // }
    }

    public void LoseLife()
    {
        sp.Write("L");  // Send 'L' to Arduino to indicate a life loss
        // if (lives > 0)
        // {
        //     lives--;
        //     sp.Write("L");  // Send 'L' to Arduino to indicate a life loss
        // }
    }

    void OnApplicationQuit()
    {
        if (sp.IsOpen)
        {
            sp.Close();
        }
    }
    private void TurnLightOn(int value){
        Debug.Log("lED: " + value);
        string ledPin = value.ToString();
        sp.Write(ledPin);
    }

    private void OnEnable(){
        LevelManager.turnLightOn += TurnLightOn;
    }


    private void OnDisable(){
        LevelManager.turnLightOn -= TurnLightOn;
    }
}
