using System.Net.Sockets;
using UnityEngine;
using System.IO;

public class ESP32WifiController : MonoBehaviour {
	TcpClient tcpClient;
	StreamReader reader;
	StreamWriter writer;

	void Start() {
		Debug.LogWarning("Starting connection to tcp client");
		tcpClient = new TcpClient("192.168.1.69", 8080); // IP of ESP and Port
		reader = new StreamReader(tcpClient.GetStream());
		writer = new StreamWriter(tcpClient.GetStream());
		writer.AutoFlush = true; // Makes sure data is sent inmediately
		Debug.LogWarning("Connected to tcp client");
	}

	// void Update() {
	// 	if (tcpClient.Connected) {
	// 		try {
	// 			NetworkStream stream = tcpClient.GetStream();
	// 			if (stream.DataAvailable) {  // Checks for available data
	// 				string data = reader.ReadLine(); // Reads data sent from ESP32
	// 				//Debug.Log("Button state: " + data);
	// 			}// else {
	// 				//Debug.LogWarning("No data available yet.");
	// 			//}
	// 			// if (Input.GetKeyDown(KeyCode.Space)) { // 
	// 			// 	SendDataToESP32("toggle_led");
	// 			// }
	// 		} catch (IOException e) {
	// 			Debug.LogError("Error de lectura o tiempo de espera agotado: " + e.Message);
	// 		}
	// 	}
	// }

	void SendDataToESP32(string message) {
		if (!tcpClient.Connected) {
			return;
		}
		writer.WriteLine(message);
		//Debug.Log("Data sent to ESP32: " + message);
	}

	void OnApplicationQuit() {
		reader.Close();
		writer.Close();
		tcpClient.Close();
	}
	
	private void OnEnable(){
		LevelManager.turnLightOn += TurnLightOn;
		LevelManager.playSound += playBuzzer;
	}


	private void OnDisable(){
		//SendDataToESP32("5");
		LevelManager.turnLightOn -= TurnLightOn;
		LevelManager.playSound -= playBuzzer;
	}

	private void TurnLightOn(int value){
		SendDataToESP32(value.ToString());
	}

	private void playBuzzer(string value){
		SendDataToESP32(value);
	}
}