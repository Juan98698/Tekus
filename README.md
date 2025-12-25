**🧩 Tekus – Providers & Services Management**
Proyecto Full Stack construido con .NET + Blazor WebAssembly + MudBlazor, siguiendo principios de Clean Architecture, con soporte completo de paginación, búsqueda y ordenamiento en backend.

**📌 Descripción general**
Tekus es una aplicación para la gestión de proveedores y servicios, donde:
Un Proveedor puede tener múltiples Servicios
Un Servicio puede estar asociado a múltiples Países
Se ofrece:
Listado global de proveedores
Listado global de servicios
Gestión de servicios por proveedor (modal)
Búsqueda, ordenamiento y paginación server-side
🏗️ Arquitectura
El proyecto sigue Clean Architecture, separando responsabilidades claramente:


Tekus
├── Tekus.Domain          → Entidades, Value Objects, reglas de negocio
├── Tekus.Application     → Casos de uso, DTOs, contratos
├── Tekus.Infrastructure  → EF Core, Repositorios, DbContext
├── Tekus.API             → Controllers (REST API)
└── Tekus.Frontend        → Blazor WASM + MudBlazor


**#Principios aplicados#**
✔ Separation of Concerns
✔ Dependency Inversion
✔ SRP (Single Responsibility)
✔ Backend-driven UI (server-side data handling)
⚙️ Stack tecnológico
Backend
.NET 9
ASP.NET Core Web API
Entity Framework Core
SQL Server
Swagger
Frontend
Blazor WebAssembly
MudBlazor
HTTPClient
Server-side pagination

**📦 Funcionalidades principales**
**👤 Proveedores**

Listado paginado
Búsqueda por nombre o NIT
Ordenamiento por columnas
Crear / editar / eliminar proveedor
Ver servicios del proveedor (modal)
Crear / editar / eliminar servicio(modal)

**🛠️ Servicios (Global)**

Listado global de todos los servicios
Búsqueda por nombre de servicio o proveedor
Ordenamiento por:
Nombre
Proveedor
Valor hora
Paginación server-side
🧩 Servicios por proveedor (Modal)
Listado paginado
Edición y eliminación
Asociación de países

🔁 Paginación, búsqueda y ordenamiento (Server-side)
Toda la lógica de datos se maneja en el backend mediante:
PagedRequest

public class PagedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public string? OrderBy { get; set; }
    public bool OrderAsc { get; set; } = true;
}

PagedResult<T>

public class PagedResult<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public IReadOnlyCollection<T> Items { get; set; } = Array.Empty<T>();
}
Esto permite que MudTable consuma datos de forma eficiente sin cargar todo en memoria.

#🔌 Endpoints principales#

Providers

GET    /api/providers
POST   /api/providers
PUT    /api/providers/{id}
DELETE /api/providers/{id}
GET    /api/providers/{providerId}/services
Services (Global)
Copiar código

GET /api/services
Ejemplo:


/api/services page=1 pageSize=10 search=dev orderBy=provider orderAsc=true
🖥️ Frontend (MudBlazor)
Se usa MudTable con ServerData:

<MudTable T="ServiceDto"
          ServerData="LoadServerData"
          RowsPerPage="10">
Ordenamiento:

<MudTableSortLabel T="ServiceDto" SortLabel="name">
    Servicio
</MudTableSortLabel>

**🧪 Cómo ejecutar el proyecto**

Backend

1. Abra SQL server y copir, pegar y luego ejecute el Script que esta en el archivo tipo text "Script SQL para creacion ..." en el mismo orden alli expuesto:
-> create database tekus -> crear tablas providers, service y ServicesCountries
-> Insertar datos de las tablas
al final tendra 10 registros en Providers, 10 en Service y 20 en ServicesCountries
2. Configura la cadena de conexión en appsettings.json y appsettings.Development.json en el proyecto Tekus.API
3. ConnectionStrings": {
    "Default": "Server={NameServer};Database=Tekus;Encrypt=false;User Id={UserName};Password={your_password};"
  },
4. Ejecuta la API / Frontend apuntando al nombre Tekus

5. click derecho en la solución 
-> Configure Startup Pojects 
-> Multiple startup projects 
-> Tekus.API Start y Tekus.Frontend Start, el resto None 
-> Aplicar, Aceptar

*Otra forma es a traves de migraciones *  

Ejecuta migraciones:

dotnet ef migrations add InitialCreate y luego dotnet ef database update luego dotnet run (para CLI de .NET).
Add-Migration initialCreate y luego Update-Database  (Package manager console, establecer como default project Tekus.Infrastructure)

Correr API Y Frontend 

click derecho en la solución 
-> Configure Startup Pojects 
-> Multiple startup projects 
-> Tekus.API Start y Tekus.Frontend Start, el resto None 
-> Aplicar, Aceptar

Swagger disponible en:
https://localhost:7001/swagger


Frontend

cd Tekus.Frontend
dotnet run
