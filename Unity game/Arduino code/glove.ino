void setup() {
  pinMode(A0, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);
  Serial.begin(9600);
}

void loop() {
  // read the analog value for pin 0/1/3:
  int analogValue1 = analogRead(A0);
  int analogValue2 = analogRead(A2);
  int analogValue3 = analogRead(A3);

  if (analogValue2 > 900) // left rotate threshold, needs to be calibrated for case to case
  {
    Serial.flush();
    Serial.println(1);
  }
  else if (analogValue2 < 650) // right rotate threshold, needs to be calibrated for case to case
  {
    Serial.flush();
    Serial.println(2);
  }

  if (analogValue3 > 180 && analogValue3 <= 200 ) // short jump threshold, needs to be calibrated for case to case
  {
    Serial.flush();
    Serial.println(3);
  }
  else if (analogValue2 > 200) // long jump threshold, needs to be calibrated for case to case
  {
    Serial.flush();
    Serial.println(4);
  }

  Serial.print(analogValue1);
  Serial.print(" ");
  Serial.print(analogValue2);
  Serial.print(" ");
  Serial.print(analogValue3);
  Serial.println();
  
  delay(1000);
}