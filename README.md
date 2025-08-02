# ITS Final Backend

This is the backend repository of my final exam of my Cloud Development Course at ITS Alto Adriatico.
This repository and the `ITSFinal.Frontend` have been produced under 4 hours time, there are a few things I'm not proud of in terms of Clean Code or clarity that need some refactoring. 
I do not have access to the original request of the project since it was on paper, but the general functionality was:
- A web platform to handle data acquisition of sensors with a fixed set of data (for a dynamic one check my pinned Mokametrics project in my home ;)).
  - Due to time data acquisition is simplified as an http endpoint called by a postman/insomnia client or could have been a service worker that generated data. It was treated the same way as far as points.
- Ability for a user to list every sensor data in a table
- Bonus points for a distributed architecture
- Bonus points for making a scalable adn consistent architecture for data acquisition
- Points for code quality and architecture

This project satisfied every point and is composed of:

- ASP.NET core 9 minimal API project for interacting with the frontend
- A GetSensorData Azure function to simulate data acquisition, in a production environment it depends on the method and global architecture of the ecosystem.
- A dequeuer function which is an Azure Service Bus trigger to dequeue sensor data and write it to the DB
- For the frontend check my other repository `ITSFinal.Frontend`

- EF Core and Azure SQL.
- Unit of Work pattern.
- Github actions for the CD pipeline and Azure App Service for deployment. 


## Utility

database commands:

from the API project folder of the project:
```
dotnet ef migrations add <migration name> -p ..\TrivillinRaffaele.DataAccess -o Migrations -c ApplicationDbContext

dotnet ef database update
```
