# 🔐 DevVault

> A modern, production-ready Project & Task Management API built with .NET 9

DevVault is a robust backend API showcasing real-world engineering practices including JWT authentication, clean architecture, Docker containerization, and comprehensive API documentation.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?style=flat-square&logo=postgresql)
![Redis](https://img.shields.io/badge/Redis-7-DC382D?style=flat-square&logo=redis)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat-square&logo=docker)

## ✨ Features

- **🔒 JWT Authentication** - Secure user registration, login, and token refresh
- **👥 User Management** - Complete user CRUD with bcrypt password hashing
- **📁 Project Management** - Full CRUD operations for project organization
- **✅ Task Management** - Create, update, and track tasks within projects
- **🐳 Docker Environment** - One-command setup with PostgreSQL, Redis, and API
- **📚 Auto Documentation** - Beautiful Scalar UI for API testing
- **🏗️ Clean Architecture** - CQRS-style separation of concerns
- **🔄 Database Migrations** - Automated schema management with EF Core

## 🛠️ Tech Stack

| Category | Technology |
|----------|-----------|
| **Language** | C# (.NET 9) |
| **Framework** | ASP.NET Core Web API |
| **ORM** | Entity Framework Core |
| **Database** | PostgreSQL 16 |
| **Caching** | Redis 7 |
| **Authentication** | JWT Bearer Tokens |
| **Containerization** | Docker & Docker Compose |
| **Documentation** | Scalar API Docs |

## 📂 Project Structure

```
DevVault/
├── src/
│   ├── DevVault.Api/            # Controllers & API entry point
│   ├── DevVault.Application/    # Business logic & services
│   ├── DevVault.Domain/         # Core entities & domain models
│   └── DevVault.Infrastructure/ # Data access & external services
├── docker-compose.yml
└── README.md
```

## 🚀 Quick Start

### Prerequisites

- [.NET SDK 9](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### 1. Clone & Setup

```bash
git clone https://github.com/Alameen17/devvault.git
cd devvault
```

### 2. Start Services

```bash
docker compose up -d --build
```

This launches:
- **PostgreSQL** on `localhost:5432`
- **Redis** on `localhost:6379`
- **API** on `localhost:5057`

### 3. Apply Database Migrations

```bash
dotnet ef database update -p src/DevVault.Infrastructure -s src/DevVault.Api
```

### 4. Explore the API

Open your browser to:
```
http://localhost:5057/scalar/v1
```

## 🔑 Authentication Flow

### Register a New User

```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "expiresAt": "2025-10-23T12:00:00Z"
}
```

### Authenticate Requests

Add the token to all protected endpoints:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

## 📋 API Examples

### Create Project

```http
POST /api/projects
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Mobile App Redesign",
  "description": "Complete UI/UX overhaul for iOS and Android"
}
```

### Create Task

```http
POST /api/tasks/{projectId}
Authorization: Bearer <token>
Content-Type: application/json

{
  "title": "Design authentication flow",
  "description": "Create wireframes for login and signup screens"
}
```

### Update Task

```http
PUT /api/tasks/{taskId}
Authorization: Bearer <token>
Content-Type: application/json

{
  "title": "Design authentication flow",
  "description": "Wireframes completed and approved",
  "isCompleted": true
}
```

### Delete Task

```http
DELETE /api/tasks/{taskId}
Authorization: Bearer <token>
```

## 🧪 Testing with Scalar

1. Navigate to `http://localhost:5057/scalar/v1`
2. Click **Authorize** and paste your JWT token
3. Select any endpoint to test
4. View real-time request/response examples

![Scalar UI Preview](https://scalar.com/images/scalar-preview.png)

## 💻 Development Commands

| Command | Description |
|---------|-------------|
| `dotnet restore` | Restore NuGet packages |
| `dotnet build` | Build all projects |
| `dotnet run --project src/DevVault.Api` | Run API locally |
| `dotnet ef migrations add <Name> -p src/DevVault.Infrastructure -s src/DevVault.Api` | Create migration |
| `dotnet ef database update -p src/DevVault.Infrastructure -s src/DevVault.Api` | Apply migrations |
| `docker compose down` | Stop all services |
| `docker compose logs -f devvault.api` | View API logs |

## 🎯 Roadmap

- [ ] **Role-Based Authorization** (Admin/User/Guest)
- [ ] **Real-time Notifications** with SignalR
- [ ] **File Attachments** for tasks
- [ ] **Activity Logs** for audit trails
- [ ] **React + TypeScript Frontend**
- [ ] **CI/CD Pipeline** with GitHub Actions
- [ ] **Cloud Deployment** (Azure/AWS/Fly.io)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Alameen Adekunle**

- GitHub: [@Alameen17](https://github.com/Alameen17)
- LinkedIn: [Al-ameen Adekunle](https://www.linkedin.com/in/al-ameen-adekunle-2a085a1b5/)

---

<p align="center">
  ⭐ Star this repo if you found it helpful!
</p>
