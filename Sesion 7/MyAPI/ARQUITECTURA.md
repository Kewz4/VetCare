# 🏗️ Estructura del Proyecto - Guía Visual

## 📁 Árbol de Carpetas

```
MyAPI/
│
├── README.md                           # Documentación principal
├── CONCEPTOS.md                        # Explicación de conceptos SOLID y MVC
├── PREGUNTAS_RESPUESTAS.md            # FAQ y preguntas frecuentes
├── MyAPI.slnx                          # Archivo de la solución
│
└── MakeAPI/                            # Carpeta principal del proyecto
    ├── Program.cs                      # Configuración de la aplicación
    ├── MakeAPI.csproj                  # Archivo de proyecto
    ├── MakeAPI.http                    # Ejemplos de peticiones HTTP
    ├── appsettings.json                # Configuración
    ├── appsettings.Development.json    # Configuración de desarrollo
    │
    ├── Properties/
    │   └── launchSettings.json         # Configuración de lanzamiento
    │
    ├── Models/                         # 📊 MODELOS (Entidades de Negocio)
    │   └── Producto.cs                 # Clase del producto con lógica OOP
    │
    ├── Services/                       # ⚙️ SERVICIOS (Lógica de Negocio)
    │   ├── IProductoService.cs         # Interfaz (contrato) del servicio
    │   └── ProductoService.cs          # Implementación en memoria
    │
    ├── DTOs/                           # 📦 DATA TRANSFER OBJECTS
    │   ├── ProductoDTO.cs              # Para leer/respuestas GET
    │   ├── CrearProductoDTO.cs         # Para crear POST
    │   └── ActualizarProductoDTO.cs    # Para actualizar PUT
    │
    └── Controllers/                    # 🎮 CONTROLADORES (Endpoints)
        └── ProductosController.cs      # API REST de productos
```

---

## 🔄 Flujo de una Solicitud HTTP

```
┌─────────────────────────────────────────────────────────────────┐
│  1. CLIENTE (Navegador/Postman)                                 │
│     Envía: GET /api/productos/{id}                              │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. ROUTING (Program.cs)                                        │
│     app.MapControllers()                                        │
│     Encuentra: ProductosController.ObtenerPorId(int id)         │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. CONTROLADOR (ProductosController.cs)                        │
│     [HttpGet("{id}")]                                           │
│     public async Task<...> ObtenerPorId(int id)                 │
│     ✅ Valida entrada (id > 0)                                  │
│     ✅ Llama al servicio                                        │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼ Inyección de Dependencias
┌─────────────────────────────────────────────────────────────────┐
│  4. SERVICIO (ProductoService.cs)                               │
│     public async Task<Producto?> ObtenerPorIdAsync(int id)      │
│     ✅ Implementa lógica de negocio                             │
│     ✅ Busca en la lista                                        │
│     ✅ Retorna Producto o null                                  │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  5. MODELO (Producto.cs)                                        │
│     public class Producto { ... }                               │
│     ✅ Datos con validaciones                                   │
│     ✅ Lógica de dominio (ActualizarStock)                      │
│     ✅ Propiedades calculadas (Disponible)                      │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  6. CONVERSIÓN A DTO (ProductoDTO)                              │
│     var dto = new ProductoDTO {                                 │
│         Id = producto.Id,                                       │
│         Nombre = producto.Nombre,                               │
│         ...                                                     │
│     };                                                          │
│     ✅ Seguridad: Solo expone datos necesarios                  │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  7. RESPUESTA HTTP (Controlador)                                │
│     return Ok(productoDTO);  // 200 OK                          │
│     Content-Type: application/json                              │
│     Body: {                                                     │
│         "id": 1,                                                │
│         "nombre": "Laptop",                                     │
│         "precio": 899.99,                                       │
│         ...                                                     │
│     }                                                           │
└──────────────────────┬──────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────────┐
│  8. CLIENTE RECIBE RESPUESTA                                    │
│     HTTP/1.1 200 OK                                             │
│     { producto JSON }                                           │
└─────────────────────────────────────────────────────────────────┘
```

---

## 📊 Capas de la Arquitectura

```
                    ┌─────────────────────┐
                    │  CLIENTE HTTP       │
                    │ (Navegador/Postman) │
                    └──────────┬──────────┘
                               │
                               ▼
     ┌─────────────────────────────────────────────────────┐
     │              CAPA DE PRESENTACIÓN                   │
     │  ProductosController.cs                            │
     │  - Recibe peticiones HTTP                          │
     │  - Valida entrada                                  │
     │  - Convierte Modelo a DTO                          │
     │  - Retorna respuestas JSON                         │
     └──────────────┬──────────────────────────────────────┘
                    │
                    ▼
     ┌─────────────────────────────────────────────────────┐
     │           CAPA DE LÓGICA DE NEGOCIO                │
     │  ProductoService.cs / IProductoService.cs          │
     │  - CRUD operations                                 │
     │  - Validaciones de negocio                         │
     │  - Acceso a datos (en memoria)                     │
     │  - Implementa reglas del dominio                   │
     └──────────┬──────────────────────────────────────────┘
                │
                ▼
     ┌─────────────────────────────────────────────────────┐
     │          CAPA DE MODELOS DE DOMINIO                │
     │  Producto.cs                                       │
     │  - Clase con datos y validaciones                  │
     │  - Métodos de negocio                              │
     │  - Propiedades calculadas                          │
     │  - Encapsulación                                   │
     └──────────┬──────────────────────────────────────────┘
                │
                ▼
     ┌─────────────────────────────────────────────────────┐
     │           CAPA DE DATOS/PERSISTENCIA               │
     │  List<Producto> (en memoria)                       │
     │  Hoy: Lista en memoria                             │
     │  Mañana: Entity Framework + SQL Server             │
     └─────────────────────────────────────────────────────┘

     ┌─────────────────────────────────────────────────────┐
     │     OBJETOS DE TRANSFERENCIA DE DATOS (DTOs)       │
     │  ProductoDTO, CrearProductoDTO, ActualizarDTO      │
     │  - Transportan datos entre capas                   │
     │  - Validan estructura                              │
     │  - Protegen el modelo interno                      │
     └─────────────────────────────────────────────────────┘
```

---

## 🔗 Relaciones entre Clases

```
                    ┌──────────────────────┐
                    │ ProductosController  │
                    │      (REST)          │
                    │                      │
                    │  [HttpGet]           │
                    │  [HttpPost]          │
                    │  [HttpPut]           │
                    │  [HttpDelete]        │
                    │  [HttpPatch]         │
                    └──────────┬───────────┘
                               │
                               │ usa (inyección)
                               ▼
                    ┌──────────────────────┐
                    │IProductoService     │
                    │     (interfaz)       │
                    │                      │
                    │ ObtenerTodosAsync   │
                    │ ObtenerPorIdAsync   │
                    │ CrearAsync          │
                    │ ActualizarAsync     │
                    │ EliminarAsync       │
                    │ActualizarStockAsync │
                    └──────────┬───────────┘
                               △
                               │ implementa
                               │
                    ┌──────────┴───────────┐
                    │                      │
          ┌─────────────────────┐  ┌─────────────────────┐
          │ ProductoService     │  │ProductoServiceBD    │
          │  (en memoria)       │  │  (con Entity        │
          │                     │  │   Framework)        │
          │ - _productos:List   │  │                     │
          │ - ObtenerTodosAsync │  │ - _context:DbContext│
          │ - ObtenerPorIdAsync │  │ - ObtenerTodosAsync │
          │ - CrearAsync        │  │ - ObtenerPorIdAsync │
          │ - etc...            │  │ - etc...            │
          └─────────┬───────────┘  └─────────────────────┘
                    │
                    │ gestiona
                    ▼
          ┌─────────────────────┐
          │    Producto         │
          │    (Modelo)         │
          │                     │
          │ Id: int             │
          │ Nombre: string      │
          │ Precio: decimal     │
          │ Categoria: string   │
          │ _stock: int (privado)
          │ Disponible: bool (calc)
          │                     │
          │ ActualizarStock()   │
          └─────────────────────┘

         Los DTOs convierten Producto en:
         
         ┌──────────────────┐  ┌──────────────────────┐  ┌──────────────────────┐
         │  ProductoDTO     │  │CrearProductoDTO      │  │ActualizarProductoDTO │
         │  (para leer)      │  │  (para crear)        │  │  (para actualizar)   │
         │                  │  │                      │  │                      │
         │ Id: int          │  │ Nombre: string       │  │ Nombre: string       │
         │ Nombre: string   │  │ Precio: decimal      │  │ Precio: decimal      │
         │ Precio: decimal  │  │ Categoria: string?   │  │ Categoria: string?   │
         │ Categoria: string│  │ Stock: int           │  │                      │
         │ Stock: int       │  │                      │  │ (NO ID, va en URL)   │
         │ Disponible: bool │  │ (NO ID, se genera)   │  │ (NO Stock, usa PATCH)│
         │                  │  │                      │  │                      │
         └──────────────────┘  └──────────────────────┘  └──────────────────────┘
```

---

## 🎯 Flujo Específico: GET /api/productos/1

### Paso a Paso

```
1️⃣  Cliente:
    GET https://localhost:7001/api/productos/1

2️⃣  ProductosController.ObtenerPorId(1):
    ├─ Valida: id > 0? ✓
    └─ Llama: var producto = await _productoService.ObtenerPorIdAsync(1)

3️⃣  ProductoService.ObtenerPorIdAsync(1):
    ├─ Busca: _productos.FirstOrDefault(p => p.Id == 1)
    ├─ Encuentra: Producto { Id=1, Nombre="Laptop", Precio=899.99, Stock=5 }
    └─ Retorna: Producto (no null)

4️⃣  ProductosController (continuación):
    ├─ Conversión a DTO:
    │  ProductoDTO {
    │      Id = 1,
    │      Nombre = "Laptop",
    │      Precio = 899.99,
    │      Categoria = "Electrónica",
    │      Stock = 5,
    │      Disponible = true (calculado: Stock > 0)
    │  }
    ├─ return Ok(productoDTO)
    └─ Respuesta HTTP: 200 OK

5️⃣  Respuesta al Cliente:
    HTTP/1.1 200 OK
    Content-Type: application/json
    
    {
        "id": 1,
        "nombre": "Laptop",
        "precio": 899.99,
        "categoria": "Electrónica",
        "stock": 5,
        "disponible": true,
        "fechaActualizacion": "2026-03-16T10:30:00"
    }
```

---

## 🔐 Principios SOLID en la Arquitectura

```
┌─────────────────────────────────────────────────────────────────┐
│ S - SINGLE RESPONSIBILITY                                       │
│ ┌─────────────────────────────────────────────────────────────┐ │
│ │ • Producto.cs: Representa datos y lógica del producto       │ │
│ │ • ProductoService.cs: Implementa CRUD                       │ │
│ │ • ProductosController.cs: Maneja HTTP                       │ │
│ └─────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│ O - OPEN/CLOSED                                                 │
│ ┌─────────────────────────────────────────────────────────────┐ │
│ │ • Interfaz: IProductoService                               │ │
│ │ • Abierto: Se pueden agregar nuevas implementaciones        │ │
│ │   - ProductoService (en memoria)                            │ │
│ │   - ProductoServiceBD (Entity Framework)                    │ │
│ │ • Cerrado: No modificamos implementaciones existentes       │ │
│ └─────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│ L - LISKOV SUBSTITUTION                                         │
│ ┌─────────────────────────────────────────────────────────────┐ │
│ │ ProductoService y ProductoServiceBD implementan             │ │
│ │ IProductoService y son intercambiables:                     │ │
│ │                                                             │ │
│ │ // Hoy                                                      │ │
│ │ builder.Services.AddSingleton<IProductoService,            │ │
│ │     ProductoService>();                                   │ │
│ │                                                             │ │
│ │ // Mañana (cambio sin afectar usarios de la interfaz)      │ │
│ │ builder.Services.AddScoped<IProductoService,               │ │
│ │     ProductoServiceBD>();                                 │ │
│ └─────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│ I - INTERFACE SEGREGATION                                       │
│ ┌─────────────────────────────────────────────────────────────┐ │
│ │ IProductoService solo define operaciones de producto:      │ │
│ │ • ObtenerTodosAsync()                                      │ │
│ │ • ObtenerPorIdAsync()                                      │ │
│ │ • CrearAsync()                                             │ │
│ │ • etc...                                                   │ │
│ │                                                             │ │
│ │ No las mezcla con operaciones de otras entidades           │ │
│ │ (usuarios, pedidos, etc.)                                  │ │
│ └─────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│ D - DEPENDENCY INVERSION                                        │
│ ┌─────────────────────────────────────────────────────────────┐ │
│ │ Controlador NO depende de ProductoService concreto:         │ │
│ │                                                             │ │
│ │ // ❌ Malo: Dependencia directa                            │ │
│ │ var service = new ProductoService();                       │ │
│ │                                                             │ │
│ │ // ✅ Bien: Depende de abstracción (interfaz)             │ │
│ │ public ProductosController(IProductoService service)       │ │
│ │ {                                                          │ │
│ │     _service = service;                                    │ │
│ │ }                                                          │ │
│ │                                                             │ │
│ │ La inyección de dependencias se configura en Program.cs    │ │
│ └─────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
```

---

## ✅ Checklist de Funcionalidades

```
✅ Modelos (POO)
   ├─ ✅ Clase Producto con todas las propiedades
   ├─ ✅ Id (solo lectura después creación)
   ├─ ✅ Nombre (requerido)
   ├─ ✅ Precio (> 0)
   ├─ ✅ Categoría
   ├─ ✅ Stock (privado)
   ├─ ✅ Disponible (calculada)
   └─ ✅ Método ActualizarStock con validación

✅ Servicios
   ├─ ✅ Interfaz IProductoService
   ├─ ✅ Implementación ProductoService
   └─ ✅ Validaciones de negocio

✅ DTOs
   ├─ ✅ ProductoDTO
   ├─ ✅ CrearProductoDTO
   └─ ✅ ActualizarProductoDTO

✅ Controlador
   ├─ ✅ GET  /api/productos
   ├─ ✅ GET  /api/productos/{id}
   ├─ ✅ POST /api/productos
   ├─ ✅ PUT  /api/productos/{id}
   ├─ ✅ DELETE /api/productos/{id}
   └─ ✅ PATCH /api/productos/{id}/stock

✅ Configuración
   ├─ ✅ Inyección de dependencias en Program.cs
   ├─ ✅ MapControllers
   ├─ ✅ CORS configurado
   └─ ✅ Comentarios XML

✅ Documentación
   ├─ ✅ README.md (principal)
   ├─ ✅ CONCEPTOS.md (teórico)
   ├─ ✅ PREGUNTAS_RESPUESTAS.md (FAQ)
   ├─ ✅ ARQUITECTURA.md (este archivo)
   └─ ✅ MakeAPI.http (ejemplos de prueba)
```

---

## 🚀 Próximos Pasos para Mejora

```
Nivel 1 - Básico (Fácil):
┌─────────────────────────────────────┐
│ • Agregar más propiedades           │
│ • Agregar más validaciones          │
│ • Crear más DTOs específicos        │
│ • Agregar buscadores (por nombre)   │
└─────────────────────────────────────┘

Nivel 2 - Intermedio (Medio):
┌─────────────────────────────────────┐
│ • Agregar paginación                │
│ • Agregar filtros                   │
│ • Agregar ordenamiento              │
│ • Agregar logging                   │
│ • Agregar excepciones personalizadas│
└─────────────────────────────────────┘

Nivel 3 - Avanzado (Difícil):
┌─────────────────────────────────────┐
│ • Entity Framework Core              │
│ • SQL Server database                │
│ • Unit Tests                         │
│ • Autenticación JWT                 │
│ • Autorización por roles             │
│ • Caché (Redis)                     │
│ • Versionado de API                  │
│ • Documentación Swagger              │
└─────────────────────────────────────┘
```

---

**¡Ahora entiendes la arquitectura completa de tu API! 🎓**
