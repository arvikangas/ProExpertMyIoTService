# MyIoTService

### Structure 

* MyIoTService. .NET core app
* MQTT broker for meditating client and service connection. Mosquitto
* MS SQL for database
* Client emulator.
* All services hosted in docker (todo for MyIoTService and Client emulator)

### TODO
* Client (device) authentication
* Dockerise MyIoTService and client emulator

### To Run
* docker-compose up to run mosquitto and ms sql. Required open ports: 1883 and 1433.
* Start MyIoTService. With dotnet run in Web project folder
* Start Client emulator
* Test MyIoTService with http requests. I have saves some requests in /rest queries folder. I used visual studio code extension Rest Client