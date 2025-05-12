
# CyrusTask API

A clean and structured **ASP.NET 8 Web API** project developed as part of a technical task, focusing on **Projects, Tasks, and User Management** with secure authentication, modular architecture, and best practices.

---

## 🚀 Features

- **User Registration & Login**
- **JWT Authentication**
- **CRUD Operations for Projects & Tasks**
- **Task Status Management**
- **Pagination Support**
- **DTO and Extension-based Data Mapping**

---

## 🛠️ Technologies Used

- ASP.NET 8 Web API
- SQL Server
- Entity Framework Core 
- 3-Tiers Architecture
- JWT Authentication 
- C#
- RESTful API principles
- Specification Design Pattern (To apply Pagination and sorting)
- Swagger Documentation

---

## 📂 Project Structure

```
CyrusTask/
├── Controllers/
│   ├── ProjectsController.cs
│   ├── TasksController.cs
│   └── UsersController.cs
├── DTOs/
│   ├── Projects/
│   ├── TaskItems/
│   └── Users/
├── Extensions/
├── Helpers/
|── Migrations
|── Models
|── Repositories
|── Services
|── Specifications
├── Program.cs
└── appsettings.json
```

---

## 🔑 Authentication

- **JWT Bearer Token** is used to secure endpoints.
- Endpoints under `UsersController` provide:
  - `POST /api/users/register`
  - `POST /api/users/login`

---

## 📡 API Endpoints Overview

## 🔐 User Management

| Endpoint                                                     | Method | Description                                         |
|--------------------------------------------------------------|--------|-----------------------------------------------------|
| `/api/users/register`                                        | POST   | User Registration                                   |
| `/api/users/login`                                           | POST   | User Login                                          |
| `/api/Users`                                                 | GET    | Get all Users                                       |

## 📁 Project Management

| Endpoint                                                     | Method | Description                                         |
|--------------------------------------------------------------|--------|-----------------------------------------------------|
| `/api/projects?PageSize=11&PageIndex=1&Sort=StartDateDesc`   | GET    | Get all projects                                    |
| `/api/Projects/id`                                           | GET    | Get Project by Id                                   |
| `/api/projects`                                              | POST   | Create a new project                                |
| `/api/Projects/id`                                           | PUT    | Update Existed project                              |
| `/api/Projects/id`                                           | Delete | Delete Existed project (Soft Delete)                |
| `/api/Projects/id/hard`                                      | Delete | Delete Existed project (Hard Delete)                |

## ✅ Task Management

| Endpoint                                                     | Method | Description                                         |
|--------------------------------------------------------------|--------|-----------------------------------------------------|
| `/api/Tasks?projectId=1&userId=1&PageIndex=2&Sort=StatusAsc` | GET    | Get tasks for a specific user at a specific project |
| `/api/tasks`                                                 | POST   | Create a new task                                   |
| `/api/Tasks/id/assign`                                       | POST   | Assign/Unassign task to user                        |
| `/api/Tasks/id/status`                                       | POST   | Change status of a specific task                    |
| `/api/Tasks/id`                                              | DELETE | Delete Existed Task (Soft Delete)                   |
| `/api/Tasks/id/hard`                                         | DELETE | Delete Existed Task (Hard Delete)                   |

> **Note:** Detailed API documentation can be added via Swagger.

> **Note:** Check Postman Collection to test Endpoint - And see additional parameters for pagination and sorting.

> **Note:** Types of sort in project Module: StartDateDesc , StartDateAsc, (default: sort by name Ascending).

> **Note:** Types of sort in Task Module: CreatedAtDesc, StatusAsc, (default: sort by Tile Ascending).

---

## 🔧 Setup Instructions

1. Clone the repository:
    ```bash
    git clone https://github.com/Ibrahimelsayed60/CyrusTask.git
    ```

2. Navigate to the project folder:
    ```bash
    cd CyrusTask
    ```

3. Restore NuGet packages:
    ```bash
    dotnet restore
    ```

4. Run the project:
    ```bash
    dotnet run
    ```

---

## 📄 Notes

- For testing APIs, use tools like **Postman** or **Swagger UI**.
- Ensure the database connection string is configured in `appsettings.json`.

---

## 👨‍💻 Author

Prepared by **Ibrahim El-Sayed**  
As part of a task for applying to **Cyrus Technology**.
