using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject textForInput;
    [SerializeField] private List<Computer> computers;
    [SerializeField] private List<GameObject> doors;
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
    public static event computerHandler startPc;
    public static event computerHandler stopPc;

    public static event passwordHandler turnLightOn;


    void Start()
    {
        foreach (Computer computer in computers) {
            computer.GetComputerScreen().SetActive(false);
        }
        textForInput.SetActive(false);

        computers[0].GetComponent<Computer>()._code = "c";
        computers[1].GetComponent<Computer>()._code = "gT";
        computers[2].GetComponent<Computer>()._code = "3";
        computers[3].GetComponent<Computer>()._code = "z"; //Contraseña final
        
        computers[0].GetComponent<Computer>()._index = 0;
        computers[1].GetComponent<Computer>()._index = 1;
        computers[2].GetComponent<Computer>()._index = 2;
        computers[3].GetComponent<Computer>()._index = 3;
    }
    
    public void CheckInput(string t_input){
        inputText = t_input;
        if (inputText.Equals(computers[computerIndex].GetComponent<Computer>()._code, StringComparison.OrdinalIgnoreCase))
        {
            turnLightOn?.Invoke(computerIndex);
            OpenDoor();
            inputText = "";
            return;
        }

        inputText = "";
        Debug.Log("Incorrect input.");
        
    }

    private void OpenDoor(){
        Debug.Log("Door openned");
        canOpenDoor = true;
        stopScreen();
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
        computers[computerIndex].GetComputerScreen().SetActive(true);
        textForInput.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;
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
    
    private void Update(){

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
    }
    
    private void OnDisable(){
        Computer.started -= showText;
        Computer.finished -= hideText;
    }
}
