# 🧭 Employee Management System

A full-stack, modular, and scalable **employee management platform** built with **ASP.NET Core**, **Entity Framework Core**, and **Blazor WebAssembly**.  
The system provides a modern interface and backend for managing employees, departments, roles, attendance, and HR operations with secure authentication and RESTful APIs.

---

## 📘 Table of Contents
- [🎯 Project Overview](#-project-overview)
- [⚙️ Technologies Used](#️-technologies-used)
- [🏗️ Architecture Overview](#️-architecture-overview)
- [📁 Project Structure](#-project-structure)
- [🧩 Layer Deep Dive](#-layer-deep-dive)
  - [1️⃣ BaseLibrary](#1️⃣-baselibrary)
  - [2️⃣ Server.Library](#2️⃣-serverlibrary)
  - [3️⃣ Server (API Host)](#3️⃣-server-api-host)
  - [4️⃣ Client.Library](#4️⃣-clientlibrary)
  - [5️⃣ Client (Blazor UI)](#5️⃣-client-blazor-ui)
- [💡 Features](#-features)
- [🧠 How It Works — Example Scenario](#-how-it-works--example-scenario)
- [🛠️ Design Patterns & Principles](#️-design-patterns--principles)
- [🚀 Getting Started](#-getting-started)
- [🔐 Authentication & Security](#-authentication--security)
- [🧾 API Endpoints (Examples)](#-api-endpoints-examples)
- [🧪 Testing](#-testing)
- [📦 Deployment](#-deployment)
- [🌱 Environment Variables](#-environment-variables)
- [🎨 UI Overview](#-ui-overview)
- [📈 Future Improvements](#-future-improvements)
- [🤝 Contributing](#-contributing)
- [📜 License](#-license)

---

## 🎯 Project Overview

The **Employee Management System** provides a centralized HR platform that simplifies employee tracking, attendance management, leave handling, and departmental organization.  

**Key Goals:**
- Centralize employee records and HR workflows.  
- Provide a clean separation of responsibilities using multi-layer architecture.  
- Securely manage access and user roles with JWT authentication.  
- Enable modular development through shared libraries.  
- Deliver a responsive, interactive web experience via Blazor.

---

## ⚙️ Technologies Used

### 🖥 Backend
- **ASP.NET Core Web API 8.0** – REST API layer.
- **Entity Framework Core 8** – ORM for data access.
- **SQL Server** – Relational database.
- **AutoMapper** – Object-mapping between Entities and DTOs.
- **JWT Bearer Authentication** – Stateless auth & role management.
- **FluentValidation** – Input validation layer.
- **Swagger / Swashbuckle** – API documentation and testing.

### 💻 Frontend
- **Blazor WebAssembly (WASM)** – C# client app running in browser.
- **MudBlazor** – Material-styled UI components.
- **Blazored.LocalStorage** – Client-side JWT token storage.
- **HttpClientFactory** – Typed HTTP clients for API calls.

### 🔗 Shared & Tools
- **Dependency Injection (DI)** – For inversion of control.
- **Repository & Unit of Work Patterns** – Data consistency abstraction.
- **LINQ** – Efficient data queries.
- **Serilog** – Logging (optional).
- **Docker** – Containerization (optional).

---

## 🏗️ Architecture Overview

The project follows **Clean Architecture / N-Tier Architecture**:

```
Presentation (Client - Blazor)
        ↓
Application (Client.Library)
        ↓
Domain (BaseLibrary)
        ↓
Infrastructure (Server.Library)
        ↓
API Host (Server)
```

This layered pattern ensures:
- **Loose coupling**
- **High cohesion**
- **Independent testing**
- **Ease of extension**

---

## 📁 Project Structure

```
EmployeeManagment/
│
├── BaseLibrary/            # Domain entities, DTOs, Enums, shared logic
│   ├── Entities/
│   ├── DTOs/
│   └── Enums/
│
├── Client/                 # Blazor WebAssembly frontend
│   ├── Pages/
│   ├── Components/
│   ├── Shared/
│   ├── Program.cs
│   └── wwwroot/
│
├── Client.Library/         # Client-side services, interfaces, API helpers
│   ├── Services/
│   ├── Interfaces/
│   ├── Models/
│   └── Helpers/
│
├── Server/                 # ASP.NET Core API host (controllers, config)
│   ├── Controllers/
│   ├── appsettings.json
│   ├── Program.cs
│   ├── Startup.cs
│   └── Middleware/
│
├── Server.Library/         # Business logic, EF context, repositories
│   ├── Data/
│   ├── Repositories/
│   ├── Services/
│   ├── Interfaces/
│   └── Mapping/
│
├── EmployeeManagment.sln   # Visual Studio solution file
└── README.md
```

---

## 🧩 Layer Deep Dive

### 1️⃣ **BaseLibrary**
Defines **domain models** and shared **data contracts** between client and server.

Includes:
- Entities: `Employee`, `Department`, `Role`, `Country`, `City`, `Vacation`, `Sanction`.
- DTOs for safe data transfer.
- Enumerations for `RoleType`, `Gender`, `EmployeeStatus`.

**Purpose:**  
Maintain a single source of truth for all domain entities.

---

### 2️⃣ **Server.Library**
Implements business logic, repositories, and EF data context.

Contains:
- `ApplicationDbContext` – EF Core DB context.
- Repositories – Generic + specific (e.g. `EmployeeRepository`).
- Services – Handles business logic (validation, rules, transformations).
- Interfaces – Contracts for DI.
- Mappings – AutoMapper profiles.

**Purpose:**  
Encapsulate all data access and business logic separate from presentation.

---

### 3️⃣ **Server (API Host)**
Hosts the REST API using ASP.NET Core.

Contains:
- Controllers (e.g. `EmployeesController`, `AuthController`).
- Swagger setup.
- JWT Authentication configuration.
- Dependency injection container.
- Middleware for error handling, logging, and CORS.

**Purpose:**  
Expose clean API endpoints and manage middleware pipelines.

---

### 4️⃣ **Client.Library**
Manages client-side API integration and business logic.

Contains:
- HTTP Clients (e.g. `EmployeeApiClient`, `AuthApiClient`).
- Service Interfaces (`IEmployeeService`, `IAuthService`).
- Helpers (token storage, API response wrapper).
- DTO mappings for UI components.

**Purpose:**  
Abstract API calls from the UI — easy to replace or mock.

---

### 5️⃣ **Client (Blazor UI)**
Front-end built in Blazor WebAssembly.

Contains:
- Pages: `Employees.razor`, `Departments.razor`, `Login.razor`.
- Components: `EmployeeTable.razor`, `ModalForm.razor`.
- Authentication State Provider.
- UI built with MudBlazor components.

**Purpose:**  
Provide modern, responsive UI that communicates directly with the API.

---

## 💡 Features

- ✅ Employee CRUD operations  
- ✅ Department and location management  
- ✅ Authentication & authorization (JWT)  
- ✅ Role-based access control (Admin, HR, Employee)  
- ✅ Attendance and leave management  
- ✅ Doctor recommendations and sanctions  
- ✅ Validation and exception handling middleware  
- ✅ Swagger for API testing  
- ✅ Modular, layered architecture  
- ✅ Responsive Blazor UI  

---

## 🧠 How It Works — Example Scenario

### ➕ Add a New Employee
1. User (Admin) opens **Add Employee** form in Blazor UI.  
2. `EmployeeService` (Client.Library) sends POST `/api/employees` with form data.  
3. API `EmployeesController` (Server) receives it, validates via `EmployeeService` (Server.Library).  
4. Data is saved through EF Core.  
5. API returns success response.  
6. Blazor UI updates employee table in real time.

---

## 🛠️ Design Patterns & Principles
- **Clean Architecture / N-Tier**
- **Repository Pattern**
- **Service Layer Pattern**
- **Dependency Injection**
- **DTO Mapping (AutoMapper)**
- **JWT Middleware**
- **SOLID Principles**
- **Async/Await throughout data access**

---

## 🚀 Getting Started

### Prerequisites
- .NET SDK 8.0+
- Visual Studio 2022 or VS Code
- SQL Server
- Node.js (optional for build tools)

### Setup Steps
```bash
git clone https://github.com/sadeg-2/EmployeeManagment.git
cd EmployeeManagment
```

1. Open solution in Visual Studio.  
2. Update `Server/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=EmployeeDB;Trusted_Connection=True;"
   }
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update --project Server.Library
   ```
4. Run the backend:
   ```bash
   dotnet run --project Server
   ```
5. Run the frontend:
   ```bash
   cd Client
   dotnet run
   ```
6. Visit **https://localhost:5001**.

---

## 🔐 Authentication & Security
- JWT-based authentication.
- `Authorization` header required for protected endpoints.
- Roles: `Admin`, `HR`, `Employee`.
- Password hashing using ASP.NET Identity.
- Token stored securely in browser local storage.

---

## 🧾 API Endpoints (Examples)

| Method | Endpoint | Description |
|--------|-----------|-------------|
| `POST` | `/api/auth/login` | Authenticate user |
| `GET` | `/api/employees` | List all employees |
| `POST` | `/api/employees` | Create employee |
| `PUT` | `/api/employees/{id}` | Update employee |
| `DELETE` | `/api/employees/{id}` | Delete employee |
| `GET` | `/api/departments` | List departments |

---

## 🧪 Testing
- Unit tests can be added using **xUnit** or **NUnit**.
- Mock repositories for service testing.
- Integration tests using **WebApplicationFactory**.

---

## 📦 Deployment
Options:
- Host on **IIS**, **Azure App Service**, or **Docker**.
- Build client & server together:
  ```bash
  dotnet publish -c Release
  ```
- Deploy published output to your web host.

---

## 🌱 Environment Variables
```
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection="Server=.;Database=EmployeeDB;Trusted_Connection=True;"
JWT__Key="your-secret-key"
JWT__Issuer="EmployeeAPI"
JWT__Audience="EmployeeClient"
```

---

## 🎨 UI Overview
- Responsive dashboard using MudBlazor.
- Dark/light mode.
- Employee tables, search & pagination.
- Modal dialogs for add/edit actions.
- Notifications and form validation.

---

## 📈 Future Improvements
- Add file upload for employee documents.
- Add email notifications (SMTP).
- Implement audit logs.
- Add unit/integration tests.
- Add multilingual UI support.
- Improve role-based permissions granularity.
- Integrate SignalR for real-time updates.

---

## 🤝 Contributing
1. Fork the repository.
2. Create a new branch (`feature/new-feature`).
3. Commit and push your changes.
4. Submit a Pull Request.

Please follow conventional commit messages and code formatting standards.

---

## 📜 License
This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

**Author:** [Sadeg-2](https://github.com/sadeg-2)  
**Version:** 1.0.0  
**Last Updated:** November 2025
