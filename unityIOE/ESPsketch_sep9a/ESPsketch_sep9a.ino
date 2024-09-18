/* 
  Take on me
  Connect a piezo buzzer or speaker to pin 11 or select a new pin.
  More songs available at https://github.com/robsoncouto/arduino-songs                                            
                                              
                                              Robson Couto, 2019
*/
#define NOTE_B0  31
#define NOTE_C1  33
#define NOTE_CS1 35
#define NOTE_D1  37
#define NOTE_DS1 39
#define NOTE_E1  41
#define NOTE_F1  44
#define NOTE_FS1 46
#define NOTE_G1  49
#define NOTE_GS1 52
#define NOTE_A1  55
#define NOTE_AS1 58
#define NOTE_B1  62
#define NOTE_C2  65
#define NOTE_CS2 69
#define NOTE_D2  73
#define NOTE_DS2 78
#define NOTE_E2  82
#define NOTE_F2  87
#define NOTE_FS2 93
#define NOTE_G2  98
#define NOTE_GS2 104
#define NOTE_A2  110
#define NOTE_AS2 117
#define NOTE_B2  123
#define NOTE_C3  131
#define NOTE_CS3 139
#define NOTE_D3  147
#define NOTE_DS3 156
#define NOTE_E3  165
#define NOTE_F3  175
#define NOTE_FS3 185
#define NOTE_G3  196
#define NOTE_GS3 208
#define NOTE_A3  220
#define NOTE_AS3 233
#define NOTE_B3  247
#define NOTE_C4  262
#define NOTE_CS4 277
#define NOTE_D4  294
#define NOTE_DS4 311
#define NOTE_E4  330
#define NOTE_F4  349
#define NOTE_FS4 370
#define NOTE_G4  392
#define NOTE_GS4 415
#define NOTE_A4  440
#define NOTE_AS4 466
#define NOTE_B4  494
#define NOTE_C5  523
#define NOTE_CS5 554
#define NOTE_D5  587
#define NOTE_DS5 622
#define NOTE_E5  659
#define NOTE_F5  698
#define NOTE_FS5 740
#define NOTE_G5  784
#define NOTE_GS5 831
#define NOTE_A5  880
#define NOTE_AS5 932
#define NOTE_B5  988
#define NOTE_C6  1047
#define NOTE_CS6 1109
#define NOTE_D6  1175
#define NOTE_DS6 1245
#define NOTE_E6  1319
#define NOTE_F6  1397
#define NOTE_FS6 1480
#define NOTE_G6  1568
#define NOTE_GS6 1661
#define NOTE_A6  1760
#define NOTE_AS6 1865
#define NOTE_B6  1976
#define NOTE_C7  2093
#define NOTE_CS7 2217
#define NOTE_D7  2349
#define NOTE_DS7 2489
#define NOTE_E7  2637
#define NOTE_F7  2794
#define NOTE_FS7 2960
#define NOTE_G7  3136
#define NOTE_GS7 3322
#define NOTE_A7  3520
#define NOTE_AS7 3729
#define NOTE_B7  3951
#define NOTE_C8  4186
#define NOTE_CS8 4435
#define NOTE_D8  4699
#define NOTE_DS8 4978
#define REST      0




#include <WiFi.h>


// change this to make the song slower or faster
int tempo = 140;

// change this to whichever pin you want to use
//int buzzer = 11;

// notes of the moledy followed by the duration.
// a 4 means a quarter note, 8 an eighteenth , 16 sixteenth, so on
// !!negative numbers are used to represent dotted notes,
// so -4 means a dotted quarter note, that is, a quarter plus an eighteenth!!
int melody[] = {

  // Take on me, by A-ha
  // Score available at https://musescore.com/user/27103612/scores/4834399
  // Arranged by Edward Truong

  NOTE_FS5,8, NOTE_FS5,8,NOTE_D5,8, NOTE_B4,8, REST,8, NOTE_B4,8, REST,8, NOTE_E5,8, 
  REST,8, NOTE_E5,8, REST,8, NOTE_E5,8, NOTE_GS5,8, NOTE_GS5,8, NOTE_A5,8, NOTE_B5,8,
  NOTE_A5,8, NOTE_A5,8, NOTE_A5,8, NOTE_E5,8, REST,8, NOTE_D5,8, REST,8, NOTE_FS5,8, 
  REST,8, NOTE_FS5,8, REST,8, NOTE_FS5,8, NOTE_E5,8, NOTE_E5,8, NOTE_FS5,8, NOTE_E5,8,
  NOTE_FS5,8, NOTE_FS5,8,NOTE_D5,8, NOTE_B4,8, REST,8, NOTE_B4,8, REST,8, NOTE_E5,8, 
  
  REST,8, NOTE_E5,8, REST,8, NOTE_E5,8, NOTE_GS5,8, NOTE_GS5,8, NOTE_A5,8, NOTE_B5,8,
  NOTE_A5,8, NOTE_A5,8, NOTE_A5,8, NOTE_E5,8, REST,8, NOTE_D5,8, REST,8, NOTE_FS5,8, 
  REST,8, NOTE_FS5,8, REST,8, NOTE_FS5,8, NOTE_E5,8, NOTE_E5,8, NOTE_FS5,8, NOTE_E5,8,
  NOTE_FS5,8, NOTE_FS5,8,NOTE_D5,8, NOTE_B4,8, REST,8, NOTE_B4,8, REST,8, NOTE_E5,8, 
  REST,8, NOTE_E5,8, REST,8, NOTE_E5,8, NOTE_GS5,8, NOTE_GS5,8, NOTE_A5,8, NOTE_B5,8,
  
  NOTE_A5,8, NOTE_A5,8, NOTE_A5,8, NOTE_E5,8, REST,8, NOTE_D5,8, REST,8, NOTE_FS5,8, 
  REST,8, NOTE_FS5,8, REST,8, NOTE_FS5,8, NOTE_E5,8, NOTE_E5,8, NOTE_FS5,8, NOTE_E5,8,
  
};

// sizeof gives the number of bytes, each int value is composed of two bytes (16 bits)
// there are two values per note (pitch and duration), so for each note there are four bytes
int notes = sizeof(melody) / sizeof(melody[0]) / 2;

// this calculates the duration of a whole note in ms
int wholenote = (60000 * 4) / tempo;

int divider = 0, noteDuration = 0;


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
      if(command == "n"){
        playIncorrectInputSound();
      }
      if(command == "g"){
        playUnlockMelody();
      }
      if(command == "s"){
        playSong();
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

void playUnlockMelody() {
  // Melody sequence similar to Zelda key found sound
  int melody[] = {523, 659, 784}; // Frequencies of C5, E5, G5 (a C major chord)
  int noteDurations[] = {200, 200, 400}; // Duration of each note in milliseconds

  // Play each note in the melody
  for (int i = 0; i < 3; i++) {
    tone(buzzerPin, melody[i], noteDurations[i]);  // Play the tone
    delay(noteDurations[i]);                        // Wait for the duration of the note
    noTone(buzzerPin);                             // Stop the tone before the next one
  }
}

void playIncorrectInputSound() {
  int frequency = 100; // Define frequency for the note
  
  tone(buzzerPin, frequency, 200); // Play the note with a frequency of 4978 Hz for 150 milliseconds
  delay(150); // Wait for the sound to play
  noTone(buzzerPin); // Stop the sound after the delay
}


void playSong(){
  // iterate over the notes of the melody.
  // Remember, the array is twice the number of notes (notes + durations)
  for (int thisNote = 0; thisNote < notes * 2; thisNote = thisNote + 2) {

    // calculates the duration of each note
    divider = melody[thisNote + 1];
    if (divider > 0) {
      // regular note, just proceed
      noteDuration = (wholenote) / divider;
    } else if (divider < 0) {
      // dotted notes are represented with negative durations!!
      noteDuration = (wholenote) / abs(divider);
      noteDuration *= 1.5; // increases the duration in half for dotted notes
    }

    // we only play the note for 90% of the duration, leaving 10% as a pause
    tone(buzzerPin, melody[thisNote], noteDuration * 0.9);

    // Wait for the specief duration before playing the next note.
    delay(noteDuration);

    // stop the waveform generation before the next note.
    noTone(buzzerPin);
  }
}
