/*
 Name:		Master.ino
 Created:	3/27/2017 8:46:06 PM
 Author:	duyth
*/

#include <SoftwareSerial.h>

#define TX 9
#define RX 8

SoftwareSerial command(RX, TX);

void setup() {
	command.begin(9600);
	Serial.begin(9600);
}

void loop() {
	while (Serial.available()) {
		command.println(Serial.parseInt());
	}	
}
