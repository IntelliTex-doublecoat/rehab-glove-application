import processing.serial.*;

Serial arduinoPort;

int check = 0;
String serialData=" ";
float data = 0;

void setup() {
  size(1920, 1080);
  arduinoPort = new Serial(this, "/dev/cu.usbmodem101", 115200);
}

void draw() {
  if (arduinoPort.available() > 0) {
    check = check+1;
    if (check>5) {
      serialData = arduinoPort.readStringUntil('\n');
      ArrayList<Float> sensorValue = new ArrayList<Float>();
      String[] vallist = serialData.trim().split(" ");
      if (vallist != null) {
        for (String s : vallist) {
          float f = Float.parseFloat(s);
          sensorValue.add(f);
        }
      }
      println(sensorValue);
      float metacarpophalangealjoint = sensorValue.get(2);
      float wristjointback = sensorValue.get(0);
      float wristjointside = sensorValue.get(1);

      // Clear the previous frame.
      background(255);
      // Action response.
      float per_metacarpophalangeal = (200-metacarpophalangealjoint)/100;
      if (per_metacarpophalangeal>0 && per_metacarpophalangeal<1) {
        rectMode(CORNER);
        noStroke();
        fill(per_metacarpophalangeal*250, 0, 250-250*per_metacarpophalangeal);
        rect(560, 135, 800*per_metacarpophalangeal, 100, 8);
      }
      // Title - metacarpophalangeal.
      fill(50, 50, 50);
      textSize(30);
      text("Metacarpophalangeal", 200, 175);
      text("joint", 300, 215);
      // Tracing border.
      rectMode(CENTER);
      noFill();
      strokeWeight(4);
      stroke(100, 100, 100);
      rect(960, 185, 800, 100, 8);

      float per_wristjointback = (250-wristjointback)/200;
      if (per_wristjointback>0 && per_wristjointback<1) {
        rectMode(CORNER);
        noStroke();
        fill(per_wristjointback*250, 0, 250-250*per_wristjointback);
        rect(560, 390, 800*per_wristjointback, 100, 8);
      }
      // Title - wrist joint back.
      fill(50, 50, 50);
      textSize(40);
      text("Wrist joint back", 200, 455);
      // Tracing border.
      rectMode(CENTER);
      noFill();
      strokeWeight(4);
      stroke(100, 100, 100);
      rect(960, 440, 800, 100, 8);
      
      //float per_wristjointside = (910-wristjointside)/40;
      //if (per_wristjointside>0 && per_wristjointside<1) {
      //  rectMode(CORNER);
      //  noStroke();
      //  fill(per_wristjointside*250, 0, 250-250*per_wristjointside);
      //  rect(560, 645, 800*per_wristjointside, 100, 8);
      //}
      // Title - wrist joint side.
      fill(50, 50, 50);
      textSize(40);
      text("Wrist joint side", 200, 710);
      // Tracing border.
      rectMode(CENTER);
      noFill();
      strokeWeight(4);
      stroke(100, 100, 100);
      rect(960, 695, 800, 100, 8);
    }
  }
}
