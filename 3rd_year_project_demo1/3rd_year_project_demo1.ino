#include <SoftwareSerial.h>


//define pin functions on red board
#define smDirectionPin 2 //Direction pin
#define smStepPin 3 //Stepper pin

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(smDirectionPin, OUTPUT);
  pinMode(smStepPin, OUTPUT);

  while(!Serial);
  Serial.println("found serial");

  //resetPins();
}


void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0)
  {
    char incomingByte = Serial.read();
    if(incomingByte == 'R')
    {
      MoveRight();
    }
    else if(incomingByte == 'L')
    {
      MoveLeft();
    }
    else if(incomingByte == 'd')
    {
      delay(1);
    }
    else
    {
      delay(1);
    }
  }
}

void MoveLeft()
{
  digitalWrite(smDirectionPin, HIGH); //Writes the direction to the EasyDriver DIR pin. (HIGH is clockwise).
  /*Slowly turns the motor 1600 steps*/
  for (int i = 0; i < 60; i++){
    digitalWrite(smStepPin, HIGH);
    delayMicroseconds(100);
    digitalWrite(smStepPin, LOW);
    delayMicroseconds(100);
  }
}

void MoveRight()
{
  digitalWrite(smDirectionPin, LOW); //Writes the direction to the EasyDriver DIR pin. (LOW is counter clockwise).
  /*Turns the motor fast 1600 steps*/
  for (int i = 0; i < 60; i++){
    digitalWrite(smStepPin, HIGH);
    delayMicroseconds(100);
    digitalWrite(smStepPin, LOW);
    delayMicroseconds(100);
  }
}
