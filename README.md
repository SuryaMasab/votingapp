# Voting App

This is a Voting App built using the following technologies:

- C#
- .NET 8
- REST APIs
- Entity Framework Core with Migrations
- SQL Server Developer Edition
- Blazor Server SPA

## Project Structure

The project is divided into several layers to ensure separation of concerns and maintainability:

1. **Domain**: Contains the domain models/entities.
2. **Repositories**: Contains repository classes for accessing the data.
3. **Services**: Contains service classes for business logic.
4. **Blazor Components**: Contains the UI components built with Blazor.
5. **API**: Contains the REST API controllers.

## Tech Stack

### Backend

- **.NET 8**: Used for building the backend services and APIs.
- **Entity Framework Core**: Used for database interactions and migrations.
- **SQL Server Developer Edition**: Used as the database server.
- **Repositories and Dependency Injection**: For managing data access and dependencies.

### Frontend

- **Blazor Server SPA**: Used for building the single-page application (SPA) frontend.
- **HttpClient**: Used in services to communicate with the backend APIs.

## Backend Structure

### Repositories

The repositories are used to access the data for the following entities:
- Candidate
- Voter
- Vote

Each repository implements an interface and is injected using dependency injection.

### Services

The services contain business logic and are used to interact with the repositories. For instance, the `VoterService` communicates with Blazor components and uses `HttpClient` to interact with the backend APIs.

### Domain

The domain project contains the entities/models used in the application. Currently, the same entities are used for both the backend and the frontend. In a larger application, separate DTOs (Data Transfer Objects) would be created for the frontend to reduce response size.

## Frontend Structure

### Blazor Components

The Blazor components are used to build the UI of the application. Components include forms for adding candidates and voters, and a component for casting votes.

### VoterService

The `VoterService` is used to manage communication between the Blazor components and the backend APIs using `HttpClient`.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server Developer Edition

### Setting Up the Database

1. Update the connection string in `appsettings.json` to point to your SQL Server instance.
2. Run the following command to apply the EF Core migrations and create the database:

   ```bash
   dotnet ef database update
Adding screenshots here.. ![image](https://github.com/SuryaMasab/votingapp/assets/114293640/05fd5df1-4550-4dab-8b4c-c91c3f866005)
![image](https://github.com/SuryaMasab/votingapp/assets/114293640/1f447972-ed1d-49d4-9ad9-5172952fa083)
![image](https://github.com/SuryaMasab/votingapp/assets/114293640/57c63733-c8da-42eb-a888-834eb545d15e) 
![image](https://github.com/SuryaMasab/votingapp/assets/114293640/1c5c04d9-a968-4662-96ff-de78a70439ce)


