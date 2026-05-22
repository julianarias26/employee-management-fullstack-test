# Employee Management - Prueba Técnica Full Stack .NET + React TypeScript

Solución desarrollada para una prueba técnica Full Stack usando **ASP.NET Core Web API**, **Entity Framework Core**, **JWT Authentication** y **React con TypeScript**.

El sistema permite gestionar empleados, calcular su bono anual, consultar historial de cargos, manejar autenticación con JWT y aplicar autorización basada en roles.

---

## Tecnologías utilizadas

### Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- JWT Bearer Authentication
- Swagger
- C#

### Frontend

- React
- TypeScript
- Vite
- Axios
- CSS

---

## Funcionalidades principales

### Backend

- CRUD completo de empleados.
- Cálculo de bono anual:
  - Empleado regular: 10% del salario.
  - Manager: 20% del salario.
- Manejo de historial de cargos por empleado.
- Modelo de departamentos.
- Modelo de proyectos.
- Relación muchos a muchos entre empleados y proyectos.
- Autenticación con JWT.
- Registro de usuarios.
- Login de usuarios.
- Autorización basada en roles.
- Middleware personalizado para registrar solicitudes HTTP.
- Documentación de endpoints con Swagger.
- Migraciones con Entity Framework Core.

### Frontend

- Pantalla de login.
- Pantalla de registro de usuario.
- Almacenamiento del token JWT.
- Envío automático del token en peticiones protegidas.
- Listado de empleados.
- Creación de empleados.
- Edición de empleados.
- Eliminación de empleados.
- Validaciones básicas en formularios.
- Mensajes de carga, éxito y error.
- Visualización de acciones según rol del usuario.

---

## Roles del sistema

### Admin

El usuario con rol `Admin` puede:

- Listar empleados.
- Consultar empleados por ID.
- Crear empleados.
- Editar empleados.
- Eliminar empleados.

### User

El usuario con rol `User` puede:

- Listar empleados.
- Consultar empleados por ID.

No puede crear, editar ni eliminar empleados.

---

## Estructura del repositorio

```txt
employee-management-test
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

# Backend

## Arquitectura del backend

El backend usa una arquitectura por capas simple:

```txt
EmployeeManagement.Api
EmployeeManagement.Application
EmployeeManagement.Domain
EmployeeManagement.Infrastructure
```

### EmployeeManagement.Api

Contiene:

- Controladores.
- Configuración de Swagger.
- Configuración de CORS.
- Configuración de JWT.
- Middleware personalizado.
- Punto de entrada de la aplicación.

### EmployeeManagement.Application

Contiene:

- DTOs.
- Interfaces de servicios.
- Servicios de aplicación.
- Contratos de repositorios.

### EmployeeManagement.Domain

Contiene:

- Entidades del dominio.
- Enums.
- Reglas principales de negocio.
- Estrategias para cálculo de bono.

### EmployeeManagement.Infrastructure

Contiene:

- `AppDbContext`.
- Configuración de entidades con EF Core.
- Repositorios.
- Generación de tokens JWT.
- Migraciones de base de datos.

---

## Patrones utilizados

### Patrón arquitectónico

#### Layered Architecture

Se usó una arquitectura por capas para separar responsabilidades:

- La API expone endpoints HTTP.
- Application coordina los casos de uso.
- Domain contiene reglas de negocio.
- Infrastructure maneja persistencia y servicios externos.

Esto ayuda a mantener el código organizado, claro y fácil de extender.

---

### Patrones de diseño

#### Strategy Pattern

Se usó para calcular el bono anual de empleados.

Actualmente existen dos reglas:

- Empleado regular: 10%.
- Manager: 20%.

La ventaja es que se pueden agregar nuevas reglas de cálculo sin modificar directamente la entidad `Employee`.

---

#### Repository Pattern

Se implementó un repositorio específico para empleados.

No se usó un repositorio genérico porque Entity Framework Core ya cubre gran parte de esa responsabilidad. El repositorio específico se usa para encapsular consultas relevantes del caso de uso, como obtener empleados por departamento que trabajen en al menos un proyecto.

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

---

## Configuración de base de datos

La cadena de conexión se encuentra en:

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

Si se usa SQL Server Express, se puede ajustar así:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## Configuración JWT

La configuración JWT se encuentra en:

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

Esta clave es solo para ambiente local o de prueba. En producción debería manejarse mediante variables de entorno o un servicio seguro de secretos.

---

## Ejecutar backend

Ubicarse en la carpeta del backend:

```bash
cd backend
```

Restaurar paquetes:

```bash
dotnet restore
```

Compilar:

```bash
dotnet build
```

Aplicar migraciones:

```bash
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
```

Ejecutar API:

```bash
dotnet run --project EmployeeManagement.Api
```

La API quedará disponible en una URL similar a:

```txt
https://localhost:7164
```

El puerto puede variar según la configuración local.

---

## Swagger

Después de ejecutar el backend, abrir Swagger en el navegador:

```txt
https://localhost:7164/swagger
```

Si la API se ejecuta en otro puerto, usar el puerto mostrado en la terminal.

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

Para probar endpoints protegidos:

1. Ejecutar login.
2. Copiar el token JWT.
3. Presionar el botón `Authorize` en Swagger.
4. Pegar el token con el formato:

```txt
Bearer JWT_TOKEN
```

Ejemplo:

```txt
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

---

## Reglas de autorización

```txt
GET /api/employees                         -> Admin, User
GET /api/employees/{id}                    -> Admin, User
GET /api/employees/by-department/...       -> Admin, User
POST /api/employees                        -> Admin
PUT /api/employees/{id}                    -> Admin
DELETE /api/employees/{id}                 -> Admin
```

---

## Middleware personalizado

El backend incluye un middleware que registra información de cada solicitud HTTP:

- Método HTTP.
- Ruta.
- Código de respuesta.
- Tiempo de ejecución en milisegundos.

Ejemplo de log:

```txt
HTTP GET /api/employees responded 200 in 85 ms
```

No se registra el body de las peticiones para evitar exponer información sensible como contraseñas o tokens.

---

# Frontend

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
- `authService.ts`: llamadas HTTP de autenticación.
- `employeeService.ts`: llamadas HTTP de empleados.
- `apiError.ts`: manejo de errores de la API.

---

### components

Contiene componentes reutilizables.

Ejemplos:

- `Alert.tsx`
- `LoadingMessage.tsx`

---

### features

Contiene componentes organizados por funcionalidad.

Ejemplos:

- `auth`
- `employees`

---

### models

Contiene interfaces TypeScript usadas para tipar los datos del frontend.

Ejemplos:

- `AuthResponse`
- `LoginRequest`
- `EmployeeResponse`
- `CreateEmployeeRequest`

---

## Configuración de URL de API

La URL del backend se configura mediante un archivo `.env`.

Crear el archivo:

```txt
frontend/.env
```

Basándose en:

```txt
frontend/.env.example
```

Contenido esperado:

```env
VITE_API_BASE_URL=https://localhost:7164/api
```

Si el backend corre en otro puerto, cambiar `7164` por el puerto real.

Ejemplo:

```env
VITE_API_BASE_URL=https://localhost:7001/api
```

---

## Ejecutar frontend

Ubicarse en la carpeta del frontend:

```bash
cd frontend
```

Instalar dependencias:

```bash
npm install
```

Ejecutar aplicación:

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

## Flujo de uso

1. Ejecutar backend.
2. Abrir Swagger.
3. Registrar un usuario Admin o User.
4. Ejecutar frontend.
5. Iniciar sesión desde el frontend.
6. Gestionar empleados según el rol.

---

## Flujo como Admin

El usuario Admin puede:

- Ver listado de empleados.
- Crear empleados.
- Editar empleados.
- Eliminar empleados.

---

## Flujo como User

El usuario User puede:

- Ver listado de empleados.

No puede crear, editar ni eliminar empleados.

---

## Seguridad en frontend

El frontend guarda el token JWT en `localStorage`.

El archivo `apiClient.ts` agrega automáticamente el token en cada petición:

```txt
Authorization: Bearer JWT_TOKEN
```

La interfaz oculta acciones de administración cuando el usuario tiene rol `User`.

Sin embargo, la seguridad real está en el backend mediante JWT y `[Authorize]`.

---

# Validaciones implementadas

## Backend

- Nombre de empleado requerido.
- Salario mayor a cero.
- Cargo válido.
- Departamento válido.
- Email requerido.
- Email con formato válido.
- Password requerido.
- Password mínimo de 6 caracteres.
- Rol válido: Admin o User.

## Frontend

- Nombre requerido.
- Salario mayor a cero.
- Email requerido.
- Password requerido.
- Password mínimo de 6 caracteres.
- Manejo de mensajes de error.
- Manejo de mensajes de éxito.
- Manejo de estado de carga.

---

# Consulta LINQ requerida

Se implementó una consulta para obtener empleados que:

- pertenecen a un departamento específico;
- trabajan en al menos un proyecto.

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

# Migraciones EF Core

Las migraciones se encuentran en:

```txt
backend/EmployeeManagement.Infrastructure/Persistence/Migrations
```

Para aplicarlas:

```bash
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
```

---

# Checklist de entrega

## Backend

- Código fuente completo incluido.
- Configuración de base de datos incluida.
- Migraciones EF Core incluidas.
- Archivo README incluido.
- Swagger configurado.
- JWT configurado.
- Roles implementados.
- Middleware personalizado implementado.

## Frontend

- Código fuente completo incluido.
- Configuración para ejecutar la aplicación incluida.
- URL configurable para consumir la API mediante `.env.example`.
- Documentación básica de estructura incluida.
- Login implementado.
- Registro implementado.
- CRUD de empleados implementado.
- Manejo de estados implementado.
- Validaciones básicas implementadas.

---

# Comandos rápidos de ejecución

## Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Api
dotnet run --project EmployeeManagement.Api
```

## Frontend

```bash
cd frontend
npm install
npm run dev
```
