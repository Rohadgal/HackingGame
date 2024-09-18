using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// public static GameManager instance { get; private set; }
	//
	// private void Awake(){
	// 	if (instance != null) {
	// 		Destroy(gameObject);
	// 		return;
	// 	}
	//
	// 	instance = this;
	// 	DontDestroyOnLoad(gameObject);
	// }

	private void Start(){
		UnityEngine.Cursor.lockState = CursorLockMode.Confined;
		UnityEngine.Cursor.visible = true;
	}

	public void startGame(){
		SceneManager.LoadScene(1);
	}

	public void quitGame(){
		Debug.Log("quit game");
		Application.Quit();
	}

	private void Update(){
		// if (Input.GetKeyDown(KeyCode.Escape)) {
		// 	SceneManager.LoadScene(0);
		// 	UnityEngine.Cursor.lockState = CursorLockMode.Confined;
		// 	UnityEngine.Cursor.visible = true;
		// }
	}
}
