using System;
using UnityEngine;

public class ScreenActions : MonoBehaviour{
    public GameObject screenPanel;
    public GameObject textForInput;
    private bool canStartScreen = false;

    //public InputField _userInput;
    private string inputText;
    private string expectedText = "C";
    
    public delegate void computerHandler();

    public static event computerHandler startPc;
    public static event computerHandler stopPc;

    public void CheckInput(string t_input){
        inputText = t_input;
        if (inputText.Equals(expectedText, StringComparison.OrdinalIgnoreCase))
        {
            OpenDoor();
            inputText = "";
            return;
        }

        inputText = "";
        Debug.Log("Incorrect input.");
        
    }

    private void OpenDoor(){
        Debug.Log("Door openned");
        stopScreen();
    }


    void Start()
    {
        screenPanel.SetActive(false);
        textForInput.SetActive(false);
    }

    private void OnEnable(){
        accessComputer.started += showText;
        accessComputer.finished += hideText;
    }

    private void showText(){
        textForInput.SetActive(true);
        canStartScreen = true;
    }

    private void hideText(){
        textForInput.SetActive(false);
        canStartScreen = false;
    }
    private void startScreen(){
        screenPanel.SetActive(true);
        textForInput.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;

    }
    
    private void stopScreen(){
        screenPanel.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        stopPc?.Invoke();
    }

    public void StopScreenAndMove(){
        stopScreen();
    }

    private void OnDisable(){
        accessComputer.started -= showText;
        accessComputer.finished -= hideText;
    }

    private void Update(){
        if (!canStartScreen) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            startPc?.Invoke();
            startScreen();
        }

        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     stopPc?.Invoke();
        //     stopScreen();
        // }
    }
}
