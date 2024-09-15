using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private float moveSpeed = 5f;
    private float rotationSpeed = 70f;

    private float xRotatin = 0f;

    private bool canPlayerMove = true;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        transform.Rotate(Vector3.up * 90f);
     
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0) {
        //     return;
        // }
        //
        // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 5f;
        // transform.position += movement;
        if (!canPlayerMove) {
            return;
        }
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    private void HandleRotation(){
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        
        transform.Rotate(Vector3.up * mouseX);

        xRotatin -= mouseY;
        xRotatin = Math.Clamp(xRotatin, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(xRotatin,0f,0f);
    }

    private void OnEnable(){
        ScreenActions.startPc += stopPlayerMovement;
        ScreenActions.stopPc += startPlayerMovement;
    }

    private void OnDisable(){
        ScreenActions.startPc -= stopPlayerMovement;
        ScreenActions.stopPc -= startPlayerMovement;
    }

    void stopPlayerMovement(){
        canPlayerMove = false;
    }

    void startPlayerMovement(){
        canPlayerMove = true;
    }
}
