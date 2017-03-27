/*
 Name:		Master.ino
 Created:	3/27/2017 8:46:06 PM
 Author:	duyth
*/

#include <SoftwareSerial.h>

#define BAUD (9600)

#define TX 9
#define RX 8

#include <SPI.h>
#include <Ethernet.h>
#include <PubSubClient.h>

byte mac[] = { 0xDE, 0xED, 0xBA, 0xFE, 0xFE, 0xED };
IPAddress ip(192, 168, 137, 177);
IPAddress server(10, 35, 57, 208);

SoftwareSerial commandTransporter(RX, TX);
EthernetClient ethClient;
PubSubClient client(ethClient);

void setup() {
	commandTransporter.begin(BAUD);
	Serial.begin(BAUD);

	client.setServer("broker.hivemq.com", 1883);
	client.setCallback(callback);

	Ethernet.begin(mac, ip);
	// Allow the hardware to sort itself out
	delay(1500);
}

void loop() {
	if (!client.connected()) {
		reconnect();
	}
	client.loop();
}

void callback(char* topic, byte* payload, unsigned int length) {
	Serial.print("Message arrived [");
	Serial.print(topic);
	Serial.print("] ");
	String inString((char*)payload);
	Serial.println(inString);
	commandTransporter.println(inString.toInt());
}

void reconnect() {
	// Loop until we're reconnected
	while (!client.connected()) {
		Serial.print("Attempting MQTT connection...");
		// Attempt to connect
		if (client.connect("arduinoClient")) {
			Serial.println("connected");
			// Once connected, publish an announcement...
			client.publish("a2s", "hello server");
			// ... and resubscribe
			client.subscribe("s2a");
		}
		else {
			Serial.print("failed, rc=");
			Serial.print(client.state());
			Serial.println(" try again in 5 seconds");
			// Wait 5 seconds before retrying
			delay(5000);
		}
	}
}