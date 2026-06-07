# Gestión de empleados - Full Stack .NET + React TypeScript

Aplicación Full Stack para la administración de empleados, desarrollada con **ASP.NET Core Web API**, **Entity Framework Core**, **SQL Server**, **JWT Authentication** y **React con TypeScript**.

El sistema permite registrar, consultar, actualizar y eliminar empleados, calcular bonos anuales, gestionar historial de cargos, asociar empleados a proyectos, autenticar usuarios mediante JWT y controlar el acceso según roles.

---

## Tabla de contenido

- [Características principales](#características-principales)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Arquitectura del proyecto](#arquitectura-del-proyecto)
- [Estructura del repositorio](#estructura-del-repositorio)
- [Requisitos previos](#requisitos-previos)
- [Configuración del backend](#configuración-del-backend)
- [Ejecución del backend](#ejecución-del-backend)
- [Configuración del frontend](#configuración-del-frontend)
- [Ejecución del frontend](#ejecución-del-frontend)
- [Autenticación y autorización](#autenticación-y-autorización)
- [Endpoints principales](#endpoints-principales)
- [Modelo de base de datos](#modelo-de-base-de-datos)
- [Patrones implementados](#patrones-implementados)
- [Validaciones](#validaciones)
- [Middleware personalizado](#middleware-personalizado)
- [Consulta LINQ destacada](#consulta-linq-destacada)
- [Comandos rápidos](#comandos-rápidos)
- [Resumen técnico](#resumen-técnico)

---

## Características principales

### Backend

- API REST construida con ASP.NET Core Web API.
- Persistencia con Entity Framework Core y SQL Server.
- Autenticación mediante JWT.
- Autorización basada en roles.
- CRUD completo de empleados.
- Cálculo de bono anual según el cargo del empleado.
- Gestión de historial de cargos.
- Relación entre empleados, departamentos y proyectos.
- Relación muchos a muchos entre empleados y proyectos.
- Middleware personalizado para trazabilidad de solicitudes HTTP.
- Documentación de endpoints con Swagger.
- Migraciones de base de datos con Entity Framework Core.

### Frontend

- Aplicación construida con React y TypeScript.
- Consumo de API mediante Axios.
- Pantalla de login.
- Pantalla de registro.
- Gestión del token JWT.
- Envío automático del token en peticiones protegidas.
- Listado de empleados.
- Creación, edición y eliminación de empleados.
- Visualización de opciones según el rol del usuario.
- Manejo de estados de carga, éxito y error.
- Validaciones básicas en formularios.

---

## Tecnologías utilizadas

### Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / SQL Server LocalDB
- JWT Bearer Authentication
- Swagger / OpenAPI
- C#

### Frontend

- React
- TypeScript
- Vite
- Axios
- CSS

### Herramientas

- Visual Studio / Visual Studio Code
- SQL Server Management Studio
- Entity Framework Core CLI
- npm

---

## Arquitectura del proyecto

El backend está organizado usando una arquitectura por capas, separando responsabilidades entre la API, la lógica de aplicación, el dominio y la infraestructura.

```txt
EmployeeManagement.Api
EmployeeManagement.Application
EmployeeManagement.Domain
EmployeeManagement.Infrastructure
```

### EmployeeManagement.Api

Contiene la capa de entrada de la aplicación.

Responsabilidades principales:

- Controladores HTTP.
- Configuración de Swagger.
- Configuración de CORS.
- Configuración de autenticación JWT.
- Registro de dependencias.
- Middleware personalizado.
- Punto de arranque de la aplicación.

### EmployeeManagement.Application

Contiene la lógica de aplicación y los contratos que coordinan los casos de uso.

Responsabilidades principales:

- DTOs.
- Interfaces de servicios.
- Servicios de aplicación.
- Contratos de repositorios.
- Coordinación entre la API, el dominio y la infraestructura.

### EmployeeManagement.Domain

Contiene las entidades principales y reglas de negocio.

Responsabilidades principales:

- Entidades del dominio.
- Enums.
- Reglas principales del negocio.
- Estrategias para el cálculo de bonos.

### EmployeeManagement.Infrastructure

Contiene la implementación técnica de persistencia y servicios externos.

Responsabilidades principales:

- `AppDbContext`.
- Configuración de entidades con Entity Framework Core.
- Repositorios.
- Generación de tokens JWT.
- Migraciones de base de datos.

---

## Estructura del repositorio

```txt
employee-management
├── backend
│   ├── EmployeeManagement.sln
│   ├── EmployeeManagement.Api
│   ├── EmployeeManagement.Application
│   ├── EmployeeManagement.Domain
│   └── EmployeeManagement.Infrastructure
│
├── frontend
│   ├── src
│   ├── package.json
│   ├── vite.config.ts
│   └── .env.example
│
├── .gitignore
└── README.md
```

---

## Requisitos previos

Antes de ejecutar el proyecto, se debe tener instalado:

### Backend

- .NET 8 SDK
- SQL Server LocalDB o SQL Server Express
- Entity Framework Core CLI

Instalar Entity Framework Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

Actualizar Entity Framework Core CLI si ya está instalado:

```bash
dotnet tool update --global dotnet-ef
```

### Frontend

- Node.js
- npm

---

## Configuración del backend

### Cadena de conexión

La cadena de conexión se configura en el archivo:

```txt
backend/EmployeeManagement.Api/appsettings.json
```

Ejemplo usando SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Ejemplo usando SQL Server Express:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

### Configuración JWT

La configuración JWT también se encuentra en:

```txt
backend/EmployeeManagement.Api/appsettings.json
```

Ejemplo:

```json
{
  "Jwt": {
    "Issuer": "EmployeeManagementApi",
    "Audience": "EmployeeManagementClient",
    "Key": "THIS_IS_A_DEVELOPMENT_SECRET_KEY_CHANGE_IT_IN_PRODUCTION_123456789"
  }
}
```

> La clave JWT incluida en el ejemplo es solo para ambiente local. En producción debe manejarse mediante variables de entorno, Azure Key Vault u otro servicio seguro de secretos.

---

## Ejecución del backend

Ubicarse en la carpeta del backend:

```bash
cd backend
```

Restaurar paquetes:

```bash
dotnet restore
```

Compilar la solución:

```bash
dotnet build
```

Aplicar migraciones de Entity Framework Core:

```bash
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
```

Ejecutar la API:

```bash
dotnet run --project EmployeeManagement.Api
```

La API quedará disponible en una URL similar a:

```txt
https://localhost:7164
```

El puerto puede variar según la configuración local. Se debe usar el puerto mostrado en la terminal.

---

## Swagger

Después de ejecutar el backend, se puede abrir Swagger en el navegador:

```txt
https://localhost:7164/swagger
```

Si la API se ejecuta en otro puerto, se debe reemplazar `7164` por el puerto mostrado en la terminal.

---

## Configuración del frontend

La URL del backend se configura mediante variables de entorno.

Crear el archivo:

```txt
frontend/.env
```

Tomando como referencia el archivo:

```txt
frontend/.env.example
```

Contenido esperado:

```env
VITE_API_BASE_URL=https://localhost:7164/api
```

Si el backend se ejecuta en otro puerto, se debe cambiar `7164` por el puerto real.

Ejemplo:

```env
VITE_API_BASE_URL=https://localhost:7001/api
```

La variable se utiliza en:

```txt
frontend/src/api/apiClient.ts
```

Mediante:

```ts
baseURL: import.meta.env.VITE_API_BASE_URL
```

Esto permite cambiar la URL de la API sin modificar el código fuente.

---

## Ejecución del frontend

Ubicarse en la carpeta del frontend:

```bash
cd frontend
```

Instalar dependencias:

```bash
npm install
```

Ejecutar la aplicación:

```bash
npm run dev
```

Abrir en el navegador:

```txt
http://localhost:5173
```

---

## Compilar frontend

Para validar que el frontend compila correctamente:

```bash
npm run build
```

---

## Autenticación y autorización

El sistema maneja autenticación mediante JWT y autorización basada en roles.

Roles disponibles:

```txt
Admin
User
```

### Rol Admin

El usuario con rol `Admin` puede:

- Ver el listado de empleados.
- Consultar empleados.
- Crear empleados.
- Editar empleados.
- Eliminar empleados.

### Rol User

El usuario con rol `User` puede:

- Ver el listado de empleados.
- Consultar empleados.

El usuario con rol `User` no puede crear, editar ni eliminar empleados.

---

## Registro de usuario Admin

Endpoint:

```http
POST /api/auth/register
```

Body:

```json
{
  "fullName": "Admin User",
  "email": "admin@test.com",
  "password": "Admin123",
  "role": "Admin"
}
```

---

## Registro de usuario User

Endpoint:

```http
POST /api/auth/register
```

Body:

```json
{
  "fullName": "Normal User",
  "email": "user@test.com",
  "password": "User123",
  "role": "User"
}
```

---

## Login

Endpoint:

```http
POST /api/auth/login
```

Body:

```json
{
  "email": "admin@test.com",
  "password": "Admin123"
}
```

Respuesta esperada:

```json
{
  "userId": 1,
  "fullName": "Admin User",
  "email": "admin@test.com",
  "role": "Admin",
  "token": "JWT_TOKEN"
}
```

---

## Autorización en Swagger

Para probar endpoints protegidos desde Swagger:

1. Ejecutar el endpoint de login.
2. Copiar el token JWT recibido.
3. Presionar el botón `Authorize` en Swagger.
4. Pegar el token usando el siguiente formato:

```txt
Bearer JWT_TOKEN
```

Ejemplo:

```txt
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

---

## Endpoints principales

### Autenticación

```http
POST /api/auth/register
POST /api/auth/login
```

### Empleados

```http
GET /api/employees
GET /api/employees/{id}
GET /api/employees/by-department/{departmentId}/with-projects
POST /api/employees
PUT /api/employees/{id}
DELETE /api/employees/{id}
```

---

## Reglas de autorización

```txt
GET    /api/employees                                  -> Admin, User
GET    /api/employees/{id}                             -> Admin, User
GET    /api/employees/by-department/{departmentId}/with-projects -> Admin, User
POST   /api/employees                                  -> Admin
PUT    /api/employees/{id}                             -> Admin
DELETE /api/employees/{id}                             -> Admin
```

---

## Modelo de base de datos

Tablas principales:

```txt
AppUsers
Departments
Employees
Projects
EmployeeProjects
PositionHistory
```

Relaciones principales:

```txt
Department 1 --- N Employees
Employee 1 --- N PositionHistory
Employee N --- N Projects
```

### Descripción general

- `AppUsers`: almacena los usuarios del sistema.
- `Departments`: almacena los departamentos.
- `Employees`: almacena la información principal de los empleados.
- `Projects`: almacena los proyectos disponibles.
- `EmployeeProjects`: tabla intermedia para la relación muchos a muchos entre empleados y proyectos.
- `PositionHistory`: almacena el historial de cargos de cada empleado.

---

## Migraciones EF Core

Las migraciones se encuentran en:

```txt
backend/EmployeeManagement.Infrastructure/Persistence/Migrations
```

Para aplicar las migraciones:

```bash
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
```

---

## Patrones implementados

### Layered Architecture

Se utilizó una arquitectura por capas para separar responsabilidades y mantener una estructura clara.

Distribución general:

- `Api`: expone endpoints HTTP.
- `Application`: coordina los casos de uso.
- `Domain`: contiene entidades y reglas de negocio.
- `Infrastructure`: maneja persistencia y servicios técnicos.

Esta separación facilita el mantenimiento, la extensibilidad y la organización del código.

---

### Strategy Pattern

Se implementó el patrón Strategy para calcular el bono anual de los empleados.

Reglas actuales:

- Empleado regular: 10% del salario.
- Manager: 20% del salario.

Este patrón permite agregar nuevas reglas de cálculo sin modificar directamente la entidad `Employee`.

---

### Repository Pattern

Se implementó un repositorio específico para empleados.

No se utilizó un repositorio genérico porque Entity Framework Core ya cubre gran parte de esa responsabilidad. El repositorio específico permite encapsular consultas relevantes del caso de uso, como obtener empleados por departamento que trabajen en al menos un proyecto.

---

## Estructura del frontend

```txt
frontend
└── src
    ├── api
    │   ├── apiClient.ts
    │   ├── apiError.ts
    │   ├── authService.ts
    │   └── employeeService.ts
    │
    ├── components
    │   ├── Alert.tsx
    │   └── LoadingMessage.tsx
    │
    ├── features
    │   ├── auth
    │   │   ├── LoginForm.tsx
    │   │   └── RegisterForm.tsx
    │   │
    │   └── employees
    │       ├── EmployeeForm.tsx
    │       ├── EmployeeTable.tsx
    │       └── EmployeesPage.tsx
    │
    ├── models
    │   ├── auth.ts
    │   └── employee.ts
    │
    ├── App.css
    ├── App.tsx
    └── main.tsx
```

---

## Descripción de carpetas del frontend

### api

Contiene la lógica de comunicación con el backend.

Archivos principales:

- `apiClient.ts`: configuración base de Axios.
- `authService.ts`: llamadas HTTP relacionadas con autenticación.
- `employeeService.ts`: llamadas HTTP relacionadas con empleados.
- `apiError.ts`: manejo de errores de la API.

### components

Contiene componentes reutilizables.

Ejemplos:

- `Alert.tsx`
- `LoadingMessage.tsx`

### features

Contiene componentes organizados por funcionalidad.

Ejemplos:

- `auth`
- `employees`

### models

Contiene interfaces TypeScript utilizadas para tipar los datos del frontend.

Ejemplos:

- `AuthResponse`
- `LoginRequest`
- `EmployeeResponse`
- `CreateEmployeeRequest`

---

## Seguridad en frontend

El frontend almacena el token JWT en `localStorage`.

El archivo `apiClient.ts` agrega automáticamente el token en cada petición protegida:

```txt
Authorization: Bearer JWT_TOKEN
```

La interfaz oculta acciones administrativas cuando el usuario tiene rol `User`.

Sin embargo, la seguridad real se aplica en el backend mediante JWT y atributos de autorización como `[Authorize]`.

---

## Validaciones

### Backend

- Nombre de empleado requerido.
- Salario mayor a cero.
- Cargo válido.
- Departamento válido.
- Email requerido.
- Email con formato válido.
- Password requerido.
- Password mínimo de 6 caracteres.
- Rol válido: `Admin` o `User`.

### Frontend

- Nombre requerido.
- Salario mayor a cero.
- Email requerido.
- Password requerido.
- Password mínimo de 6 caracteres.
- Manejo de mensajes de error.
- Manejo de mensajes de éxito.
- Manejo de estado de carga.

---

## Middleware personalizado

El backend incluye un middleware para registrar información básica de cada solicitud HTTP.

Información registrada:

- Método HTTP.
- Ruta.
- Código de respuesta.
- Tiempo de ejecución en milisegundos.

Ejemplo de log:

```txt
HTTP GET /api/employees responded 200 in 85 ms
```

No se registra el cuerpo de las peticiones para evitar exponer información sensible como contraseñas o tokens.

---

## Consulta LINQ destacada

Se implementó una consulta para obtener empleados que cumplen las siguientes condiciones:

- Pertenecen a un departamento específico.
- Trabajan en al menos un proyecto.

Ejemplo conceptual:

```csharp
var employees = await _context.Employees
    .Include(employee => employee.Department)
    .Include(employee => employee.Projects)
    .Where(employee => employee.DepartmentId == departmentId)
    .Where(employee => employee.Projects.Any())
    .ToListAsync();
```

Esta consulta se expone mediante el endpoint:

```http
GET /api/employees/by-department/{departmentId}/with-projects
```

---

## Flujo recomendado para probar la aplicación

### 1. Ejecutar backend

```bash
cd backend
dotnet run --project EmployeeManagement.Api
```

### 2. Ejecutar frontend

En otra terminal:

```bash
cd frontend
npm run dev
```

### 3. Abrir Swagger

```txt
https://localhost:7164/swagger
```

### 4. Registrar usuarios

Se pueden registrar usuarios con rol `Admin` o `User` desde Swagger o desde el frontend.

### 5. Iniciar sesión

Luego de iniciar sesión, el sistema guarda el token JWT y lo utiliza para consumir los endpoints protegidos.

---

## Comandos rápidos

### Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
dotnet run --project EmployeeManagement.Api
```

### Frontend

```bash
cd frontend
npm install
npm run dev
```

### Build del frontend

```bash
cd frontend
npm run build
```

---

## Resumen técnico

Este proyecto implementa una aplicación Full Stack para la gestión de empleados.

El backend expone una API REST protegida con JWT y autorización basada en roles. La persistencia se maneja con Entity Framework Core y SQL Server. La lógica de negocio incluye el cálculo de bonos anuales mediante Strategy Pattern, gestión de historial de cargos, departamentos y proyectos.

El frontend consume la API mediante Axios, maneja autenticación, conserva el token JWT y permite ejecutar operaciones según el rol del usuario autenticado.

La solución prioriza una estructura clara, separación de responsabilidades y mantenibilidad, evitando una complejidad innecesaria para el alcance del sistema.
