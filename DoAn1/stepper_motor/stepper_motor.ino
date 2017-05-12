/*
 Name:		stepper_motor.ino
 Created:	5/12/2017 10:49:17 AM
 Author:	duyth
*/

#define ENABLE 4
#define STEP 3
#define DIR 2

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(9600);
	setupMotor();
}

// the loop function runs over and over again until power down or reset
void loop() {
	doQuarterRevolution();
	//delay(1000);
}

void setupMotor() {
	pinMode(STEP, OUTPUT);
	pinMode(DIR, OUTPUT);
	digitalWrite(DIR, LOW);
	pinMode(ENABLE, OUTPUT);
	digitalWrite(ENABLE, LOW);
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