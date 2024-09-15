using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour{
	
	public delegate void screenHandler(int value);

	public static event screenHandler started;
	public static event screenHandler finished;
	
    public string _code { get; set; }
    public int _index { get; set; }
    [SerializeField] private GameObject computerScreen;

    Computer(string t_code){
	    _code = t_code;
    }

    public GameObject GetComputerScreen(){
	    return computerScreen;
    } 
    
    
    private void OnTriggerEnter(Collider other){
	    if (other.CompareTag("Player")) {
		    started?.Invoke(_index);
	    }
    }

    private void OnTriggerExit(Collider other){
	    finished?.Invoke(_index);
    }
    
}
