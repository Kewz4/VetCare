# Instrucciones de Configuración - Aplicación de Gestión de Alumnos

## 📋 Requisitos Previos
- Visual Studio o Visual Studio Code
- .NET 10.0 SDK instalado
- PostgreSQL instalado y ejecutándose
- Base de datos PostgreSQL creada

## 🗄️ Paso 1: Crear la Tabla en PostgreSQL

Ejecuta este script en tu base de datos PostgreSQL:

```sql
CREATE TABLE IF NOT EXISTS alumnos (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    fecha_nacimiento DATE,
    telefono VARCHAR(20),
    direccion TEXT,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE
);
```

## 🔧 Paso 2: Configurar la Cadena de Conexión

Abre el archivo `appsettings.json` y reemplaza los datos de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=nombreBD;Username=usuario;Password=contraseña"
  },
  ...
}
```

**Reemplaza:**
- `Host`: IP o nombre del servidor PostgreSQL (ejemplo: localhost, 192.168.1.100)
- `Port`: Puerto de PostgreSQL (por defecto: 5432)
- `Database`: Nombre de tu base de datos
- `Username`: Usuario de PostgreSQL (por defecto: postgres)
- `Password`: Contraseña del usuario

### Ejemplo completo:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=escuela;Username=postgres;Password=micontraseña123"
  },
  ...
}
```

## 📦 Paso 3: Restaurar Paquetes NuGet

En la consola del administrador de paquetes o terminal:

```bash
dotnet restore
```

## 🚀 Paso 4: Ejecutar la Aplicación

```bash
dotnet run
```

La aplicación estará disponible en: `https://localhost:7000` (o el puerto que configure)

## 🎯 Paso 5: Acceder a la Interfaz

Una vez iniciada la aplicación, navega a:
- **Listado de alumnos**: `/Estudiantes` o haz clic en "Estudiantes" en el menú

## 📝 Funcionalidades Implementadas

- ✅ **Listar** alumnos (tabla con datos completos)
- ✅ **Ver** detalles de un alumno
- ✅ **Crear** nuevo alumno
- ✅ **Editar** información del alumno
- ✅ **Eliminar** alumno con confirmación

## 🛠️ Estructura de Archivos

```
WebAppEscuela/
├── Controllers/
│   ├── HomeController.cs
│   └── EstudiantesController.cs (NUEVO)
├── Models/
│   ├── ErrorViewModel.cs
│   └── Estudiante.cs (NUEVO)
├── Data/
│   └── AppDbContext.cs (NUEVO)
├── Views/
│   ├── Home/
│   ├── Estudiantes/ (NUEVO)
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
├── appsettings.json (MODIFICADO)
├── WebAppEscuela.csproj (MODIFICADO)
└── Program.cs (MODIFICADO)
```

## 🔗 Rutas Disponibles

| Ruta | Método | Descripción |
|------|--------|-------------|
| `/Estudiantes` | GET | Listar todos los alumnos |
| `/Estudiantes/Create` | GET | Mostrar formulario de creación |
| `/Estudiantes/Create` | POST | Guardar nuevo alumno |
| `/Estudiantes/Details/{id}` | GET | Ver detalles del alumno |
| `/Estudiantes/Edit/{id}` | GET | Mostrar formulario de edición |
| `/Estudiantes/Edit/{id}` | POST | Guardar cambios |
| `/Estudiantes/Delete/{id}` | GET | Confirmar eliminación |
| `/Estudiantes/Delete/{id}` | POST | Eliminar alumno |

## ⚠️ Solución de Problemas

### Error: "Connection string 'DefaultConnection' not found"
- Verifica que `appsettings.json` tenga la configuración correcta

### Error: "Database not found"
- Asegúrate de crear la base de datos en PostgreSQL antes

### Error: "Invalid connection"
- Verifica que PostgreSQL esté ejecutándose
- Revisa los datos de conexión (Host, Port, Usuario, Contraseña)

### Error: "nuget packages not found"
- Ejecuta: `dotnet restore`

## 📚 Tecnologías Utilizadas

- ASP.NET Core 10.0
- Entity Framework Core 8.0
- Npgsql (PostgreSQL driver)
- Bootstrap 5
- Razor Pages

¡La aplicación está lista para usar! 🎉
