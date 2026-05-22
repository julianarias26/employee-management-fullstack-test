# Employee Management - Full Stack Technical Test

Full Stack solution built with ASP.NET Core Web API, Entity Framework Core, JWT Authentication and React TypeScript.

## Technologies

### Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- JWT Bearer Authentication
- Swagger

### Frontend

- React
- TypeScript
- Vite
- Axios
- CSS

## Main Features

- Employee CRUD
- Annual bonus calculation
- Position history model
- Departments and projects model
- JWT authentication
- Role-based authorization
- Admin and User roles
- Custom HTTP request logging middleware
- React frontend consuming protected API endpoints
- Loading, success and error states
- Basic form validations

## Roles

### Admin

Can access all employee endpoints:

- List employees
- View employee details
- Create employees
- Update employees
- Delete employees

### User

Can only access read endpoints:

- List employees
- View employee details

## Architecture

The backend uses a simple layered architecture:

```txt
backend
├── EmployeeManagement.Api
├── EmployeeManagement.Application
├── EmployeeManagement.Domain
└── EmployeeManagement.Infrastructure