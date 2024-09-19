using System.IO.Ports;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 9600); // Change COM3 to your Arduino's port
    //int computers = 3;

    void Start()
    {
        sp.Open(); // Open the serial connection
        sp.ReadTimeout = 50;
        sp.Write("k");
    }
    

    void OnApplicationQuit()
    {
        if (sp.IsOpen)
        {
            sp.Close();
        }
    }
    private void TurnLightOn(int value){
        //Debug.Log("lED: " + value);
        string ledPin = value.ToString();
        sp.Write(ledPin);
    }

    private void PlaySound(string sound){
        sp.Write(sound);
    }

    private void OnEnable(){
        LevelManager.turnLightOn += TurnLightOn;
        LevelManager.playSound += PlaySound;
    }


    private void OnDisable(){
        LevelManager.turnLightOn -= TurnLightOn;
        LevelManager.playSound -= PlaySound;
    }
}
