using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScreenActions : MonoBehaviour{
    public GameObject screenPanel;
    public GameObject textForInput;
    private bool canStartScreen = false;
    
    public delegate void computerHandler();

    public static event computerHandler startPc;
    public static event computerHandler stopPc;
    
    // Start is called before the first frame update
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
    }
    
    private void stopScreen(){
        screenPanel.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Q)) {
            stopPc?.Invoke();
            stopScreen();
        }
    }
}
