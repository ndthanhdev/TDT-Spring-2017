/*
 Name:		Servant.ino
 Created:	3/27/2017 8:43:08 PM
 Author:	duyth
*/

#include <SoftwareSerial.h>

#define BAUD (9600)
#define STEP 3
#define DIR 2
#define PHOTO_RESISTOR 7

#define TX 12
#define RX 11


SoftwareSerial command(RX, TX);

bool isPayloadCrossed(int time = 2000);


void setup()
{	
	prepareMotor();
	preparePhotoresistor();
	command.begin(9600);
}

void loop()
{
	if (command.available())
	{
		releasePayload(command.parseInt());
	}
}

void prepareMotor() {
	pinMode(STEP, OUTPUT);
	pinMode(DIR, OUTPUT);
}

void doHalfRevolution() {
	int x;
	for (x = 0; x < 100; x++)
	{
		digitalWrite(3, HIGH);
		delayMicroseconds(500);
		digitalWrite(3, LOW);
		delayMicroseconds(500);
	}
}

void preparePhotoresistor() {
	pinMode(PHOTO_RESISTOR, INPUT);
}

bool isPayloadCrossed(int waitTime = 2000) {
	int debounce = 0;
	long current = millis();
	while (millis() - current < waitTime) {
		if (digitalRead(PHOTO_RESISTOR) == HIGH)
			debounce += 1;
		delay(1);
	}
	return debounce > 3;
}

void releasePayload(int n) {
	while (n > 0)
	{
		while (!isPayloadCrossed()) {
			doHalfRevolution();
		}
		n--;
	}
}


