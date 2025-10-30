# Organic EIRL - Order Management System

## Description

The application is built using .NET for the backend and React with TypeScript for the frontend.

## Architecture

The project architecture follows the **Clean Architecture** pattern, separating the different layers of the application to improve maintainability and scalability. The main layers are:

- **Domain**: Contains domain entities and business logic.
- **Application**: Contains use cases and application logic, using CQRS (Command Query Responsibility Segregation) with MediatR.
- **Infrastructure**: Contains data access implementation, including Entity Framework Core and database configuration.
- **Web/API**: Contains the REST API that exposes endpoints for frontend communication.

### Base Structure

- **Backend**: Uses ASP.NET Core 8.0 as the framework to build the API.
- **Frontend**: Uses React with TypeScript for an interactive and typed user experience.
- **Database**: SQL Server Express, using Entity Framework Core as ORM.
- **Validations**: Uses FluentValidation for business validations.
- **Docker**: Uses Docker to containerize the application, facilitating orchestration and deployment.

## Project Setup Instructions

### Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express 2022](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [Node.js](https://nodejs.org/) version 18.x or higher
- [Yarn](https://yarnpkg.com/) version 1.22.x or higher

### Clone Repository

```bash
git clone https://github.com/ronaldidro/organic-eirl.git
```

### Build and Run Containers

From the project root, run the following command to build and start the containers:

```bash
docker-compose up --build
```

### Access the Application

- **Client**: [http://localhost:3000](http://localhost:3000)

### Stop Containers

To stop the containers, run:

```bash
docker-compose down
```

### Clean Volumes

To remove volumes and clean up completely, run:

```bash
docker-compose down -v
```

### Development Mode

#### Server

1. Go to the server folder:

```bash
cd server
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Run migrations to create the database:

```bash
dotnet ef database update
```

4. Start the API:

```bash
dotnet run
```

The API will be available at `http://localhost:5056`.

#### Client

1. Go to the client folder:

```bash
cd client
```

2. Install dependencies:

```bash
yarn install
```

3. Start the application:

```bash
yarn dev
```

The application will be available at `http://localhost:5173`.

## Tech Stack

### Backend

- **ASP.NET Core**: Main framework for building the API.
- **Entity Framework Core**: ORM for database management.
- **FluentValidation**: For business validations.
- **MediatR**: To implement the CQRS pattern.

### Frontend

- **React**: Library for building user interfaces.
- **TypeScript**: JavaScript superset that adds static typing.
- **Vite**: Build and development tool for modern applications.
- **Axios**: For making HTTP requests to the API.
