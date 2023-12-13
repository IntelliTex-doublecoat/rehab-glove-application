void setup() {
  pinMode(A0, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);
  Serial.begin(115200);
}

void loop() {
  // read the analog value for pin 0/1/3:
  int analogValue1 = analogRead(A0);
  int analogValue2 = analogRead(A2);
  int analogValue3 = analogRead(A3);

  Serial.print(analogValue1);
  Serial.print(" ");
  Serial.print(analogValue2);
  Serial.print(" ");
  Serial.print(analogValue3);
  Serial.println();
  
  delay(100);
}