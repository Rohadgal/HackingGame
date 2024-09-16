using System.Net.Sockets;
using UnityEngine;
using System.IO;

public class ESP32WifiController : MonoBehaviour {
	TcpClient tcpClient;
	StreamReader reader;
	StreamWriter writer;

	void Start() {
		Debug.LogWarning("Starting connection to tcp client");
		tcpClient = new TcpClient("192.168.1.69", 8080); // IP de la ESP32 y el puerto
		reader = new StreamReader(tcpClient.GetStream());
		writer = new StreamWriter(tcpClient.GetStream());
		writer.AutoFlush = true; // Asegura que los datos se envíen inmediatamente
		Debug.LogWarning("Connected to tcp client");
	}

	void Update() {
		if (tcpClient.Connected) {
			try {
				NetworkStream stream = tcpClient.GetStream();
				if (stream.DataAvailable) {  // Verifica si hay datos disponibles
					string data = reader.ReadLine(); // Lee los datos enviados por la ESP32
					Debug.Log("Button state: " + data);
				} else {
					Debug.LogWarning("No data available yet.");
				}
				if (Input.GetKeyDown(KeyCode.Space)) { // Al presionar la barra espaciadora
					SendDataToESP32("toggle_led");
				}
			} catch (IOException e) {
				Debug.LogError("Error de lectura o tiempo de espera agotado: " + e.Message);
			}
		}
	}

	void SendDataToESP32(string message) {
		if (!tcpClient.Connected) {
			return;
		}
		writer.WriteLine(message);
		Debug.Log("Data sent to ESP32: " + message);
	}

	void OnApplicationQuit() {
		reader.Close();
		writer.Close();
		tcpClient.Close();
	}
	
	private void OnEnable(){
		LevelManager.turnLightOn += TurnLightOn;
	}


	private void OnDisable(){
		SendDataToESP32("5");
		LevelManager.turnLightOn -= TurnLightOn;
	}

	private void TurnLightOn(int value){
		SendDataToESP32(value.ToString());
	}
}