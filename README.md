## PetScanner

A webapplication connecting to a an Arduino Uno R4 Wifi with a circuit consisting of a sensor which prints the time of the scanning.

## Features
- Fetch data from Arduino to website
- Save data to website after a fetch
- Display either data directly from Arduino or from local storage ScanHistory

## Goals
- Connect Arduino Uno R4 Wifi which read ID of subdermal tags using WL-134. 

## Set-up
1. Upload code to Arduino Uno R4 Wifi through the Arduino IDE and connect to power source
2. Read the printed IP address
3. Put the IP-address in Services-> FetchFromArduino-> IPAdress variable
4. Run project
