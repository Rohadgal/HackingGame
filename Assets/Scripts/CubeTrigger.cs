using System;
using UnityEngine;

public class CubeTrigger : MonoBehaviour{
	[SerializeField] private int indexCube;
	private bool isCorrect;

	public bool getIsCorrect(){
		return isCorrect;
	}

	private void OnTriggerEnter(Collider other){
		if (other.CompareTag("Cube")) {
			if (other.gameObject.GetComponent<RoomCube>().getCubeNumber() == indexCube) {
				isCorrect = true;
				Debug.Log("CORRECT CUBE");
			}
		}
	}

	private void OnTriggerExit(Collider other){
		if (other.CompareTag("Cube")) {
			// if (other.gameObject.GetComponent<RoomCube>().getCubeNumber() == indexCube) {
			// 	isCorrect = false;
			// }
			isCorrect = false;
			Debug.Log("CUBE EXIT");
		}
	}
}
