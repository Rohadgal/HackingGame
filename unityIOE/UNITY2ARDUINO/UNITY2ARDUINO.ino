int ledPin1 = 2;  // PWM pin for LED
int ledPin2 = 3;  // PWM pin for LED
int ledPin3 = 4;  // PWM pin for LED
int buzzer = 7;

void setup() {
  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);
  pinMode(buzzer, OUTPUT);
  Serial.begin(9600);  // Start serial communication
  //updateLed();
  digitalWrite(ledPin1, HIGH);
  digitalWrite(ledPin2, HIGH);
  digitalWrite(ledPin3, HIGH);
  digitalWrite(buzzer, HIGH);
 // tone(buzzer, 261.63, 500);
  delay(1500);
  digitalWrite(ledPin1, LOW);
  digitalWrite(ledPin2, LOW);
  digitalWrite(ledPin3, LOW);
  digitalWrite(buzzer, LOW);
 // noTone(buzzer);
}

void loop() {
  // Check if there's data from Unity
  if (Serial.available() > 0) {
    char command = Serial.read();

    switch(command){
      case '0':
        digitalWrite(ledPin1, HIGH);
        Serial.println("LED toggled");
        break;
      case '1':
        digitalWrite(ledPin2, HIGH);
        Serial.println("LED toggled");
        break;
      case '2':
        digitalWrite(ledPin3, HIGH);
        Serial.println("LED toggled");
        break;
      default: break;
    }
    return;
  }
  // digitalWrite(ledPin1, LOW);
  // digitalWrite(ledPin2, LOW);
  // digitalWrite(ledPin3, LOW);
  // Serial.println("LED toggled OFF");
}

void updateLed() {
  // Calculate brightness based on lives remaining
  //int brightness = map(lives, 0, 3, 0, 255);
  digitalWrite(ledPin1, HIGH);
  Serial.println("LED toggled");
}