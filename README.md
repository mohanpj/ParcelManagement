# ParcelManagement
Package Management Application

Application is built using .Net Core 2.2. Application uses message queueing concept using RabbitMQ. Application has been developed using Mac and Visual Studio Code.


**Pre-requisites: **
1. Ensure .net core sdk or runtime 2.2 is installed in the machine
2. Ensure RabbitMQ is installed and running on default port 
3. Incase of using docker for RabbitMQ run this command to spin-off a new RabbitMQ instance
   "*docker container run -d --name package_rabbit --rm -p 5672:5672 -p 15673:15672 rabbitmq:3-management*"

**To run application:**
  
  Extract zip ParcelManagement.zip to some desired folder.
  
To parse the parcels and publish all parcels to RabbitMQ queue:
1. navigate to ParcelManagement.Distribution project folder in command prompt "*cd ParcelManagement.Distribution*" 
2. run this command *dotnet run*

To consume the parcels
1. navigate to ParcelManagement.Departments.Console project folder in a new command prompt window "*cd ParcelManagement.Departments.Console*"
2. run the command "*dotnet run*"
