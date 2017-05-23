/*
 Name:		Servant.ino
 Created:	3/27/2017 8:43:08 PM
 Author:	duyth
*/

#include <SoftwareSerial.h>

#define BAUD (9600)	

//	DRV8825
#define ENABLE 4	
#define STEP 3	
#define DIR 2	

#define PHOTO_RESISTOR 7	//	chân DO của quang trở

#define RX 8	//
#define TX 9	//



SoftwareSerial commandTransporter(RX, TX);

Stream *inputStream;

void setup()
{
	setupMotor();
	setupPhotoresistor();

	Serial.begin(BAUD);

	// if release
	commandTransporter.begin(BAUD);
	inputStream = &commandTransporter;

	// if debug	
	//inputStream = &Serial;
	
	
}

void loop()
{
	if (inputStream->available())
	{
		Serial.println("Recieved command");
		releasePayload(inputStream->parseInt());
	}
}

void setupMotor() {
	pinMode(STEP, OUTPUT);
	pinMode(DIR, OUTPUT);
	pinMode(ENABLE, OUTPUT);

	disableMotor();
}

void doQuarterRevolution() {
	int x;
	for (x = 0; x < 50; x++)
	{
		digitalWrite(STEP, HIGH);
		delayMicroseconds(1000);
		digitalWrite(STEP, LOW);
		delayMicroseconds(1000);
	}
}

void setupPhotoresistor() {
	pinMode(PHOTO_RESISTOR, INPUT);
}

bool isPayloadCrossed(int waitTime = 250) {
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
	enableMotor();
	while (n > 0)
	{
		while (!isPayloadCrossed()) {
			doQuarterRevolution();
		}
		n--;
		Serial.println("Released 1 package");
		delay(1000); // wait for package pass
	}
	disableMotor();
}

void enableMotor() {
	digitalWrite(ENABLE, LOW);
}

void disableMotor() {
	digitalWrite(ENABLE, HIGH);
}


