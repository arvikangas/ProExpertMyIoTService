# MyIoTService

### Overview 

* MyIoTService. .NET core app
* MQTT broker for meditating client and service connection. Hivemq
* MS SQL for database
* Client emulator.
* Authentication for MyIoTservice with jwt
* Authentication for devices with name and password. Device is authenticatied for mqtt broker, not MyIoTService. Names and passwords are stored in xml.
* No Tls

![Overview](https://github.com/arvikangas/ProExpertMyIoTService/blob/master/docs/Overview.png)

### Why I added mqtt broker
According to assignment main problem is solving high connection count and growth of devices. Mqtt has low overhead and built specifically for IoT kind of solution. Assignment did not mention specifically any other component between devices and MyIotService, but i decided to add mqtt broker between them. I spent some time when choosing protocol for device connection, but not for other components. I think that in real life situation all other components can be switched out, like database and MyIoTService. But nobody is going to update thousands of devices in the field. 

### What i did not do, but wanted to, and should exist in real life situation
* Better authentication for mqtt broker. Ideally device authentication data would be stored in database, ie. name and hashed password. Implementing that would have required building some plugins for existing mqtt broker. In Java for Hivemq or in C for mosquitto. It can be done, but i ran out of steam at that point and decided to stop. Passwords are also stored as plaintext, not hashes right now. They can be stored as hashes, but that requires you to have java installed and i did not want to add that requirement.
* Dockerize ClientEmulator and MyIoTService. 
* Tls for mqtt.
* Tls for MyIotService. Could be added trivially.

### To Run
* docker-compose up to run Hivemq and ms sql. Required open ports: 1883 and 1433.
* Start MyIoTService. With dotnet run in MyIoTService\src\Web project folder. Required port 5001. App runs in http://localhost:5001.
* Start Client emulator. With dotnet run in ClientEmulator\ClientEmulator folder.  Required port 5010. App runs in http://localhost:5010.
* Login to MyIotService or create new account. Use jwt to make requests ie. in http header add Authentication: Bearer myjwttoken
* Open ClientEmulator UI and connect your device to mqtt broker
* All ports can be changed, but then you have to make changes in docker-compose file and in app appsettings.json file

### MyIotService
* no ui
* authentication with jwt
* preseeded account: username: user password: secret. Preseeded device: name: device1, password: device1
* rest queries are in /rest queries folder. I use visual studio code - rest client extension myself so they are in tha format, but they should be pretty explanatory for any other way to send queries
* swagger docs are in https://localhost:5001/swagger/index.html

### ClientEmulator
* ui. Updates with signalr
* device authentication with name and password
