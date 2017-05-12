/*
 Name:		Master.ino
 Created:	3/27/2017 8:46:06 PM
 Author:	duyth
*/

#include <SoftwareSerial.h>

#define BAUD (9600)

#define RX 8
#define TX 9


#include <SPI.h>
#include <Ethernet.h>
#include <PubSubClient.h>

byte mac[] = { 0xDE, 0xED, 0xBA, 0xFE, 0xFE, 0xED };
IPAddress server(10, 35, 19, 22);

SoftwareSerial commandTransporter(RX, TX);
EthernetClient ethClient;
PubSubClient client(ethClient);

void setup() {

	Serial.begin(BAUD);
	Serial.println("initializing...");

	setupEthernetShield();

	commandTransporter.begin(BAUD);	

	////client.setServer("broker.hivemq.com", 1883);
	client.setServer("test.mosquitto.org", 1883);
	//client.setServer(server, 1883);	

	client.setCallback(callback);

	
	
	// Allow the hardware to sort itself out
	delay(1500);
	Serial.println("initialized.");
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
		if (client.connect("arduinoClient321")) {
			Serial.println("connected");
			// Once connected, publish an announcement...
			client.publish("a", "hello server");
			// ... and resubscribe
			client.subscribe("s");
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

void setupEthernetShield() {
	// start the Ethernet connection:
	if (Ethernet.begin(mac) == 0) {
		Serial.println("Failed to configure Ethernet using DHCP");
		// no point in carrying on, so do nothing forevermore:
		for (;;)
			;
	}
	// print your local IP address:
	Serial.println(Ethernet.localIP());
}