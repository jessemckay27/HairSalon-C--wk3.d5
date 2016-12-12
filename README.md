# Hair Salon

#### _A website to add, remove, and edit your clients and stylists_

#### By _**Jesse McKay**_

## Description

_This page allows the user to interact with a database for a Hair Salon, allowing them to add or edit clients and stylists._

## Specifications
| Test                                      | Input     | Output    |
|-------------------------------------------|-----------|-----------|
| Test if stylist database is empty         | Empty     | 0         |
| Test if two equal objects match by name   | "Test"    | "Test"    |
| Test if stylist saves to database         | "Test"    | "Test"    |
| Test if ID assigned when stylist is saved | "Test"    | 1         |
| Test finding a stylist in the database    | "Test"    | 1         |
| Test if stylist update saves to database  | "NewTest" | "NewTest" |
| Test if stylist deleted from database     | Delete    | 0         |
| Test if client database is empty          | Empty     | 0         |
| Test if client saves to database          | "Test"    | "Test"    |
| Test if two equal objects match by name   | "Test"    | "Test"    |
| Test finding a client in the database     | "Test"    | 1         |
| Test if client updated saves to database  | "NewTest" | "NewTest" |
| Test if client deleted from database      | Delete    | 0         |

## Setup Instructions
* User must have access to a Windows operating system
* Visit GitHub and search for the user "jessemckay27"
* Clone the repository "HairSalon-C--wk3.d5" to your computer
* Open Windows PowerShell
* Using Powershell, navigate to the folder where you downloaded the repository
* Run the command "dnu restore" in PowerShell
* Run the command "dnx kestrel" in PowerShell
* Type the following commands to create the databases:
*  _CREATE DATABASE hair_salon;_
*  _GO_
*  _USE hair_salon;_
*  _GO_
*  _CREATE TABLE stylists (id INT IDENTITY(1,1) PRIMARY KEY, name VARCHAR(255));_
*  _GO_
*  _CREATE TABLE clients (id INT IDENTITY(1,1) PRIMARY KEY, name VARCHAR(255), stylistId INT);_
*  _GO_

*  _CREATE DATABASE hair_salon_test;_
*  _GO_
*  _USE hair_salon_test;_
*  _GO_
*  _CREATE TABLE stylists (id INT IDENTITY(1,1) PRIMARY KEY, name VARCHAR(255));_
*  _GO_
*  _CREATE TABLE clients (id INT IDENTITY(1,1) PRIMARY KEY, name VARCHAR(255), stylistId INT);_
*  _GO_
* Open your web browser and enter "localhost:5004" into the web address bar
* After the page loads, use the links to add, edit and delete the stylists and clients

## Support and contact details

_Email rosecity27@comcast.net to contact the site creator._

## Technologies Used

_This project used HTML, Bootstrap, C#, Nancy, Razor, Xunit, Microsoft Powershell, Windows Operating System_

### License

*Please distribute freely!*
