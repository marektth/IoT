import paho.mqtt.client as paho
import ssl
from time import sleep
from random import uniform
import requests
import json

connflag = False

url_address_temp = "URL FOR TEMP HERE"
url_address_pres = "URL FOR PRESSURE HERE"
url_address_hum = "URL FOR HUMIDITY HERE"

def on_connect(client, userdata, flags, rc):  # func for making connection
    global connflag
    print("Connected to AWS")
    connflag = True
    print("Connection returned result: " + str(rc))


def on_message(client, userdata, msg):  # Func for Sending msg
    print(msg.topic + " " + str(msg.payload))

mqttc = paho.Client()

mqttc.on_connect = on_connect

mqttc.on_message = on_message

awshost = "host url here"
awsport = 8883  # Port no.
clientId = "ATS_IoT"
thingName = "ATS_IoT"
caPath = "AmazonRootCA1.pem.crt"
certPath = "aecb235c68-certificate.pem.crt"
keyPath = "aecb235c68-private.pem.key"

mqttc.tls_set(caPath, certfile=certPath, keyfile=keyPath, cert_reqs=ssl.CERT_REQUIRED, tls_version=ssl.PROTOCOL_TLSv1_2,
              ciphers=None)

mqttc.connect(awshost, awsport, keepalive=60)

mqttc.loop_start()

while 1:
    sleep(5)
    if connflag == True:
        res = requests.get(url_address_temp)
	res2 = requests.get(url_address_pres)
	res3 = requests.get(url_address_hum)
	data = res.json()
	data2 = res2.json()
	data3 = res3.json()
	print (data['state'])
	print (data2['state'])
	print (data3['state'])
	tempreading = float(data['state'])
	presreading = float(data2['state'])
	humreading = float(data3['state'])
        message = '{"temperature":' + str(tempreading) + ',' + '"pressure":' + str(presreading) + ',' + '"humidity":' + str(humreading) + '}'
        mqttc.publish("messageTopic", message, 1)
        print("msg sent: temperature " + "%.2f" + "pressure" + "%.2f" + "humidity" + "%.2f" % tempreading, presreading, humreading)
    else:
        print("waiting for connection...")
