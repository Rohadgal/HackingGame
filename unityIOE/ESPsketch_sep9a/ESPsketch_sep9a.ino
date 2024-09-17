#include <WiFi.h>

const char* ssid = "xxxxxxxxx";
const char* password = "xxxxxxxx";
WiFiServer server(8080); // Puerto 8080, puedes cambiarlo si lo prefieres
//int boton = 2;
int ledPin1 = 4; // Pin para el LED
int ledPin2 = 5; // Pin para el LED
int ledPin3 = 2; // Pin para el LED
int buzzerPin = 15; // Pin para el buzzer

void setup() {
 // pinMode(boton, INPUT);
  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);
  pinMode(buzzerPin, OUTPUT);  // Set buzzer pin as output
  digitalWrite(ledPin1, HIGH);
  digitalWrite(ledPin2, HIGH);
  digitalWrite(ledPin3, HIGH);
  delay(1000);
  digitalWrite(ledPin1, LOW);
  digitalWrite(ledPin2, LOW);
  digitalWrite(ledPin3, LOW);
  Serial.begin(115200);
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
      delay(1000);
      Serial.println("Conectando a WiFi...");
  }
  
  Serial.println("Conectado a WiFi");
  Serial.println(WiFi.localIP());
  server.begin(); // Inicia el servidor
}

void playTune() {
  //Play a simple tune using tone() function
  //digitalWrite(buzzerPin, HIGH);
  tone(buzzerPin, 261.63, 500);  // Play 1000Hz for 500ms
  delay(500);                  // Wait for the note to finish
  tone(buzzerPin, 1200, 500);  // Play 1200Hz for 500ms
  delay(500);
  Serial.println("BUZZER PLAYED");
 // digitalWrite(buzzerPin, LOW);
  noTone(buzzerPin);           // Stop the buzzer
}

void loop() {
  WiFiClient client = server.available(); // Espera por un cliente

  if (client) {
    Serial.println("Cliente conectado");
    while (client.connected()) {
      String command = client.readStringUntil('\n'); // Lee el comando enviado desde Unity
      command.trim(); // Elimina espacios en blanco y caracteres de nueva línea
      Serial.println("Comando recibido: " + command);
      // Procesar comando
      if (command == "0") {
        //ledPin1 = 4;
        digitalWrite(ledPin1, HIGH); // Cambia el estado del LED
        playTune();
        Serial.println("LED toggled");
      }
      if (command == "1") {
        //ledPin2 = 5;
        digitalWrite(ledPin2, HIGH); // Cambia el estado del LED
        Serial.println("LED toggled");
      }
      if (command == "2") {
        //ledPin2 = 2;
        digitalWrite(ledPin3, HIGH); // Cambia el estado del LED
        Serial.println("LED toggled");
      }

      // int buttonState = digitalRead(boton); // Lee el estado del botón
      // Serial.println("Enviando estado del botón: " + String(buttonState)); // Log para depuración
      // client.println(buttonState); // Envía el estado del botón
    }
    client.stop();
    digitalWrite(ledPin1, LOW); // Cambia el estado del LED
    digitalWrite(ledPin2, LOW); // Cambia el estado del LED
    digitalWrite(ledPin3, LOW); // Cambia el estado del LED
    Serial.println("LED toggled off");
    Serial.println("Cliente desconectado");
  }
}
