using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject textForInput;
    [SerializeField] private List<Computer> computers;
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private string _url = "";
    [SerializeField] GameObject gameFinishedScreen;
    private int computerIndex;
    private bool canStartScreen = false;
    private string inputText;
    private bool canOpenDoor;
    private float lerpTime;

    private bool hasSetInitPos;
    private bool isMoving;

    private Vector3 initPos;
    private Vector3 endPos;
    public delegate void computerHandler();
    public delegate void passwordHandler(int value);
    public delegate void buzzerHandler(string sound);
    public static event computerHandler startPc;
    public static event computerHandler stopPc;
    public static event passwordHandler turnLightOn;

    public static event buzzerHandler playSound;


    void Start()
    {
        foreach (Computer computer in computers) {
            computer.GetComputerScreen().SetActive(false);
        }
        textForInput.SetActive(false);
        gameFinishedScreen.SetActive(false);

        computers[0].GetComponent<Computer>()._code = "c";
        computers[1].GetComponent<Computer>()._code = "gT";
        computers[2].GetComponent<Computer>()._code = "3";
        computers[3].GetComponent<Computer>()._code = "Ciber"; //Contraseña final
        
        computers[0].GetComponent<Computer>()._index = 0;
        computers[1].GetComponent<Computer>()._index = 1;
        computers[2].GetComponent<Computer>()._index = 2;
        computers[3].GetComponent<Computer>()._index = 3;
    }

    public void gameFinished(){
        gameFinishedScreen.SetActive(true);
    }

    public void returnToMenu(){
        SceneManager.LoadScene(0);
    }
    
    public void CheckInput(string t_input){
        inputText = t_input;
        if (inputText.Equals(computers[computerIndex].GetComponent<Computer>()._code, StringComparison.OrdinalIgnoreCase)) {
            if (computerIndex != 3) {
                turnLightOn?.Invoke(computerIndex);
                OpenDoor();
                stopScreen();
            }
            else {
                stopScreen();
                playBuzzer("s");
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                UnityEngine.Cursor.visible = true;
                gameFinished();
            }
            inputText = "";
            return;
        }

        inputText = "";
        Debug.Log("Incorrect input.");
        playBuzzer("n");
        
    }

    void playBuzzer(string value){
        playSound?.Invoke(value);
    }

    void playCodeActivationSound(){
        playSound?.Invoke("g");
    }

    private void OpenDoor(){
        Debug.Log("Door openned");
        canOpenDoor = true;
        
    }

    private void showText(int value){
        computerIndex = value;
        textForInput.SetActive(true);
        canStartScreen = true;
    }

    private void hideText(int value){
        computerIndex = value;
        textForInput.SetActive(false);
        canStartScreen = false;
    }
    
    private void startScreen(){
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;
        textForInput.SetActive(false);
        computers[computerIndex].GetComputerScreen().SetActive(true);

        if (computerIndex == 3) {
            StartCoroutine(waitToOpenUrl());
        }
    }

    IEnumerator waitToOpenUrl(){
        yield return new WaitForSeconds(5f);
        OpenWebsite();
    }
    
    private void stopScreen(){
        computers[computerIndex].GetComputerScreen().SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        stopPc?.Invoke();
    }

    public void StopScreenAndMove(){
        stopScreen();
    }

    void moveDoor(){

        if (!hasSetInitPos && computerIndex != 3) {
            hasSetInitPos = true;
            isMoving = true;
            initPos = doors[computerIndex].transform.position;
            endPos = initPos + new Vector3(0, 2, 0);
        }

        if (isMoving) {
            lerpTime += Time.deltaTime; 
            lerpTime = Mathf.Clamp01(lerpTime); // Ensure lerpTime is between 0 and 1
            doors[computerIndex].transform.position = Vector3.Lerp(initPos, endPos,  lerpTime);
        }

        if (lerpTime >= 1) {
            isMoving = false;
            hasSetInitPos = false;
            canOpenDoor = false;
            lerpTime = 0;
        }
    }
    
    public void OpenWebsite(){
        if (_url == "") {
            Debug.Log("No valid url");
            return;
        }
        Application.OpenURL(_url);
    }
    
    private void Update(){
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
            
        }
        
        if (canOpenDoor) {
            moveDoor();
        }
        
        if (!canStartScreen) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            startPc?.Invoke();
            startScreen();
        }
    }
    
    private void OnEnable(){
        Computer.started += showText;
        Computer.finished += hideText;
        TriggerVerification.notifyCodeOn += playCodeActivationSound;
    }
    
    private void OnDisable(){
        Computer.started -= showText;
        Computer.finished -= hideText;
        TriggerVerification.notifyCodeOn -= playCodeActivationSound;
    }
}
