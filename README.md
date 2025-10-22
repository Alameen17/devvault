DevVault — Project & Task Management API

DevVault is a .NET 9-based backend API for managing projects, tasks, and users. It’s designed to demonstrate real-world backend engineering skills — including authentication, database migrations, dependency injection, Dockerized services, and clean architecture principles.

Tech Stack

Language	C# (.NET 9)
Framework	ASP.NET Core Web API
ORM	Entity Framework Core (PostgreSQL provider)
Database	PostgreSQL 16
Caching	Redis 7
Containerization	Docker & Docker Compose
Documentation / Testing	Scalar API Docs
Authentication	JWT Bearer Tokens
Architecture	Clean Architecture (CQRS-style separation)

Features

JWT Authentication (Register, Login, Token refresh)

Users — secure user management with hashed passwords

Projects — full CRUD endpoints (create, read, update, delete)

Tasks — CRUD endpoints linked to Projects

Dockerized Environment — PostgreSQL + Redis + API

EF Core Migrations — automated schema management

Scalar UI — beautiful, interactive API testing interface

Project Structure
DevVault/
├── src/
│   ├── DevVault.Api/               # ASP.NET Core entry point (controllers)
│   ├── DevVault.Application/       # Business logic, service interfaces
│   ├── DevVault.Domain/            # Core entities and domain models
│   └── DevVault.Infrastructure/    # Persistence (EF Core), JWT, data access
├── docker-compose.yml              # Full environment (Postgres + Redis + API)
└── README.md

Running the Project with Docker

Make sure you have Docker and .NET SDK 9 installed.

1️⃣ Build & Start Services
docker compose up -d --build


This will spin up:

postgres (on port 5432)

redis (on port 6379)

devvault.api (on port 5057)

2️⃣ Apply EF Core Migrations
dotnet ef database update -p src/DevVault.Infrastructure -s src/DevVault.Api

3️⃣ Open API Docs

Visit the port hosting when you run the project

http://localhost:5057/scalar/v1

🔑 Authentication Workflow

Register

POST /api/auth/register


Login

POST /api/auth/login


Returns a JWT token.

Use the token in the Authorization header:

Authorization: Bearer <your_token>

Enpoints Examples
Create Project
POST /api/projects

Content-Type: application/json
Authorization: Bearer <token>

{
  "name": "My Project",
  "description": "Test project"
}

Create Task
POST /api/tasks/{projectId}
Content-Type: application/json
Authorization: Bearer <token>

{
  "title": "Setup backend",
  "description": "Implement API endpoints"
}

Update Task
PUT /api/tasks/{taskId}
Content-Type: application/json
Authorization: Bearer <token>

{
  "title": "Setup backend core",
  "description": "Add EF migrations and Docker support",
  "isCompleted": true
}

Delete Task
DELETE /api/tasks/{taskId}
Authorization: Bearer <token>

Developer Commands
Command	Description
dotnet restore	Restore NuGet packages
dotnet build	Build all projects
dotnet run --project src/DevVault.Api	Run API locally
dotnet ef migrations add <Name> -p src/DevVault.Infrastructure -s src/DevVault.Api	Add migration
dotnet ef database update	Apply migrations


Testing with Scalar

Navigate to:

http://localhost:5057/scalar/v1


Expand an endpoint (e.g. POST /api/projects)


Enter payloads and JWT token — test endpoints directly in the UI.

Next Steps on this project

To Add Role-Based Authorization (Admin / User)

Add Comments, Issues, and Activity Logs

Build a React + TypeScript frontend

Deploy to Render / Azure / Fly.io

🧑‍💻 Author

Alameen17
