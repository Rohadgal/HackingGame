using UnityEngine;

public class accessComputer : MonoBehaviour{
    //public GameObject screenPanel;

    public delegate void screenHandler();

    public static event screenHandler started;
    public static event screenHandler finished;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) {
           started?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other){
	    finished?.Invoke();
    }
}
