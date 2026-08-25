# HR Management API

A RESTful HR Management API built with ASP.NET Core and Microsoft SQL Server. The API manages employees and related HR data and uses ASP.NET Core Identity and JWT authentication for user authentication and role-based authorization.

---

## Technologies

- ASP.NET Core Web API
- C#
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Swagger / OpenAPI

---

## Features

- Employee management
- Department management
- Job management
- Location and country management
- Employee authentication
- JWT-based authentication
- Role-based authorization
- User and role management
- Policy-based authorization
- Manager authorization based on employee relationships

---

## Project Structure

```text
MyApp.API/
├── Controllers/
│   ├── AuthController.cs
|   ...
│   └── EmployeeController.cs
│
├── Data/
│   ├── Context/
│   │   ├── HRDbContext.cs
│   │   └── ApplicationIdentityDbContext.cs
│   │
│   ├── Configurations/
|   |   ├── CountryEntityConfiguration.cs
|   |   ...
|   |   └── LocationEntityConfiguration.cs
|   │
│   └── Identity/
|       └── IdentitySeeder.cs
│
├── Migrations/
│
├── Models/
│   ├── Domain/
|   |   ├── Country.cs
|   |   ...
|   |   └── Region.cs
|   |   
│   └── DTOs/
│
├── Services/
│   └── JwtService.cs
│
└── Program.cs
```

---

## Databases

The project uses two SQL Server databases: HR Database and Identity Database

The HR database already exists, while the Identity database schema is managed using EF Core migrations.

### HR Database

The HR database contains the application's existing HR data, including:

- Employees
- Departments
- Jobs
- Locations
- Countries
- Dependents

The HR database is an existing database and is accessed using Entity Framework Core.

### Identity Database

The Identity database is used by ASP.NET Core Identity to manage authentication and authorization data.

It contains tables for:

- Users
- Roles
- User roles
- Claims
- User logins
- User tokens
- Other ASP.NET Core Identity data

The Identity database schema is managed using Entity Framework Core migrations.

To create a new Identity migration using the Package Manager Console:

```text
Add-Migration MigrationName -Context ApplicationIdentityDbContext
```

For example:

```
Add-Migration CreatingIdentityDatabase -Context ApplicationIdentityDbContext
```

To apply the migration:

```
Update-Database -Context ApplicationIdentityDbContext
```

Alternatively, using the .NET CLI:

```
dotnet ef migrations add CreatingIdentityDatabase --context ApplicationIdentityDbContext
```

and:

```
dotnet ef database update --context ApplicationIdentityDbContext
```

---

## Authentication

The API uses ASP.NET Core Identity for user management and authentication and JWT (JSON Web Token) for authenticating API requests.

```text
The login process is:

Email + Password
       ↓
ASP.NET Core Identity
       ↓
Validate credentials
       ↓
Get user's roles
       ↓
Generate JWT
       ↓
Return JWT to client
       ↓
Client sends JWT with API requests
```

The Employee ID allows the authenticated Identity user to be associated with an employee in the HR database.

---

## Authorization

The API uses both role-based authorization and policy-based authorization.

Example roles include:

- Employee
- Manager
- HR
- Admin
- Role-Based Authorization

Endpoints can be restricted based on a user's role.

For example:

```
[Authorize(Roles = "Admin")]
```

Only users with the Admin role can access the endpoint.

Policy-Based Authorization (to be implemented)

Policy-based authorization is used for more complex authorization rules.

For example, a manager should only be allowed to modify employees who directly report to that manager.

The authorization logic can compare:

```text
Authenticated User
       ↓
EmployeeId from JWT
       ↓
Manager's EmployeeId
       ↓
Target Employee.ManagerId
       ↓
Allow / Deny
```

This prevents one manager from modifying another manager's employees.

The first administrator is created through an initial identity seeding/bootstrap process (to be implemented)

---

## Configuration

Application configuration is stored using ASP.NET Core configuration providers.

Example:

```
{
    "Jwt": {
        "Issuer": "https://localhost:7130",
        "Audience": "https://localhost:7130"
    }
}
```

Sensitive values such as the JWT signing key should not be committed to GitHub.

---

## Development

For local development, ASP.NET Core User Secrets can be used:

```
dotnet user-secrets set "Jwt:Key" "your-secret-key"
```

---

## Production

Production secrets should be supplied through environment variables or a secure secret-management system.

Sensitive information should never be hardcoded into the source code or committed to the repository.

---

## Running the Project
1. Clone the repository

```
git clone <repository-url>
```

Navigate to the project directory:

```
cd <project-directory>
```

2. Configure the database

Configure the required SQL Server connection strings for your local environment.

Do not commit database passwords or other sensitive credentials to the repository.

3. Configure User Secrets

Configure sensitive development settings using User Secrets.

For example:

```
dotnet user-secrets set "Jwt:Key" "your-secret-key"
```

4. Apply Identity migrations

Using the Package Manager Console:

```
Update-Database -Context ApplicationIdentityDbContext
```

Or using the .NET CLI:

```
dotnet ef database update --context ApplicationIdentityDbContext
```

5. Run the API

```
dotnet run
Swagger / OpenAPI
```

The API uses Swagger/OpenAPI for API documentation and testing.

Swagger is enabled when the application is running in the Development environment.

After starting the application, open the Swagger UI using the URL configured by the application.

Swagger can be used to:

View available endpoints
View request and response models
Test API endpoints
Test authenticated endpoints using JWT
Security

---


## This project is currently under development.

Future improvements:

- UI Implementation with React
- Additional HR management endpoints
- Automated testing
- Production deployment


