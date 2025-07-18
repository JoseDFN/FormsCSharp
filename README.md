# Forms Management System

![Database Schema](/Forms-public.png)

## Descripción General

**Forms Management System** es un proyecto desarrollado en C# sobre .NET Core que facilita la creación, gestión y almacenamiento de encuestas (surveys) de manera modular y escalable. El sistema implementa **Entity Framework Core** (EF Core) en un enfoque **Code First** y sigue los principios de la **arquitectura hexagonal** (Ports & Adapters), lo cual garantiza separación de responsabilidades, testabilidad y fácil mantenimiento.

---

## Características Principales

- **Gestión de Encuestas**: Definición de encuestas (*Surveys*), capítulos (*Chapters*), preguntas (*Questions*), subpreguntas (*SubQuestions*) y opciones de respuesta (*OptionResponses*).  
- **Catalogación de Categorías y Opciones**: Módulo para definir catálogos de categorías (*CategoryCatalog*) y sus opciones asociadas (*CategoryOption*).  
- **Agrupación de Opciones**: Soporte para mapear opciones de preguntas a categorías específicas mediante la entidad *OptionQuestion*.  
- **Opciones de Resumen**: *SummaryOptions* que agrupan y resumen respuestas específicas por pregunta.  
- **Enfoque Code First con EF Core**: Todas las entidades se mapean al esquema de PostgreSQL a través de clases de configuración dentro de la capa de Infrastructure.  
- **Arquitectura Hexagonal**: Separación clara entre el dominio, la lógica de aplicación, la infraestructura (acceso a datos y configuraciones EF) y la capa de presentación (API Rest).

---

## Arquitectura Hexagonal

La arquitectura hexagonal o Ports & Adapters busca aislar el **dominio de negocio** de los detalles externos (base de datos, frameworks, UI), favoreciendo:

- **Capa de Dominio (Domain)**  
  - Contiene las **entidades de negocio** sin dependencias directas a EF, frameworks o detalles de infraestructura.  
  - Cada clase en `Domain/Entities` representa un agregado o entidad de la lógica de encuestas:  
    - `Survey`  
    - `Chapter`  
    - `Question`  
    - `SubQuestion`  
    - `OptionResponse`  
    - `OptionQuestion`  
    - `CategoryCatalog`  
    - `CategoryOption`  
    - `SummaryOption`  

- **Capa de Aplicación (Application)**  
  - Define interfaces, servicios de aplicación, casos de uso y contratos que orquestan operaciones de negocio (por ejemplo, creación de encuestas, consulta de preguntas, asignación de opciones a categorías, etc.).  
  - Es independiente de EF Core y se apoya únicamente en los contratos (interfaces) para repositorios o puertos de persistencia.

- **Capa de Infraestructura (Infrastructure)**  
  - Implementa las interfaces definidas en la capa de Aplicación.  
  - Incluye la configuración de Entity Framework Core (*Fluent API*) para cada entidad en la carpeta `Infrastructure/Configuration`.  
  - Contiene el contexto `FormsContext` en `Infrastructure/Data/FormsContext.cs`, que hereda de `DbContext` e incorpora los `DbSet<T>` correspondientes a cada entidad.  
  - Gestiona las migraciones de EF Core en `Infrastructure/Data/Migrations`.

- **Capa de Presentación / API (APIForms)**  
  - Proyecto ASP.NET Core Web API (`APIForms/APIForms.csproj`).  
  - Define controladores REST para exponer endpoints que permitan operaciones CRUD sobre las encuestas, capítulos, preguntas y demás entidades.  
  - Configura inyección de dependencias (DI) para los servicios de aplicación y repositorios al iniciarse la aplicación (en `Program.cs`).  
  - Utiliza `appsettings.json` para almacenar la cadena de conexión a la base de datos (PostgreSQL) y otros ajustes de configuración.

---

## Tecnologías y Herramientas

- **Lenguaje**: C# (.NET Core 9.0.204 o superior)  
- **ORM**: Entity Framework Core (Code First)  
- **Base de Datos**: PostgreSQL (versión 11+ recomendada)  
- **Arquitectura**: Hexagonal (Ports & Adapters)  
- **Dependencias**:  
  - `Microsoft.EntityFrameworkCore`  
  - `Microsoft.EntityFrameworkCore.Design`  
  - `Microsoft.EntityFrameworkCore.Tools`  
  - `Npgsql.EntityFrameworkCore.PostgreSQL`  
- **Control de Versiones**: Git  
- **IDE**: Visual Studio 2022 / Visual Studio Code  

---

## Instalación y Configuración

### Clonar el Repositorio

```bash
git clone https://github.com/JoseDFN/FormsCSharp
cd FormsCSharp
```

### Configurar Cadena de Conexión

En el proyecto APIForms, abre el archivo appsettings.Development.json y modifica la sección ConnectionStrings para apuntar a tu instancia de PostgreSQL.

### Migraciones y Creación de la Base de Datos

1. Abre una terminal en la carpeta raíz de la solución (donde se encuentra `Forms.sln`).

2. Desde la carpeta raíz (donde está Forms.sln), ejecutas:

   ```bash
   dotnet restore
   ```
   Esto descarga e instala todas las dependencias que estén listadas en tus archivos .csproj.

3. Luego puedes hacer:

   ```bash
   dotnet build
   ```

4. Crea y/o aplica la migración inicial para construir el esquema de la base de datos:

   ```bash
   dotnet ef migrations add InitialCreate -p Infrastructure/ -s APIForms/ -o Data/Migrations/
   dotnet ef database update -p Infrastructure/ -s APIForms/
   ```

Al finalizar estos comandos, EF Core habrá generado todas las tablas (surveys, chapters, questions, sub_questions, option_questions, category_catalogs, category_options, summary_options, option_responses) dentro de la base de datos definida en la cadena de conexión.

------

> Una vez completados estos pasos, habrás preparado tu entorno y creado la base de datos necesaria para ejecutar la aplicación.
