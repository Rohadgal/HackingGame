int ledPin = 0;  // PWM pin for LED
int lives = 3;   // Total lives

void setup() {
  pinMode(ledPin, OUTPUT);
  Serial.begin(9600);  // Start serial communication
  //updateLed();
  digitalWrite(ledPin, HIGH);
  delay(1000);
  digitalWrite(ledPin, LOW);
}

void loop() {
  // Check if there's data from Unity
  if (Serial.available() > 0) {
    //char command = Serial.read();

    char command = Serial.read();

    // If a life is lost
    // if (command == 'L' && lives > 0) {
    //   lives--;
    //   updateLed();
    // }
    if (command == '0') {
      ledPin = 0;
      updateLed();
    }
  }
}

void updateLed() {
  // Calculate brightness based on lives remaining
  //int brightness = map(lives, 0, 3, 0, 255);
  digitalWrite(ledPin, HIGH);
  Serial.println("LED toggled");
}