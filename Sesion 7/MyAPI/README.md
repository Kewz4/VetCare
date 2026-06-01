# API de Gestión de Productos - Tutorial Educativo

> Proyecto de ejemplo para aprender conceptos básicos de desarrollo backend en C# con ASP.NET Core

## 📋 Descripción

Esta API RESTful implementa un sistema de gestión de productos siguiendo:
- **Arquitectura MVC**: Separación de responsabilidades en Models, Services/Business Logic, Controllers y DTOs
- **Principios SOLID**: Inyección de Dependencias, Interfaces, Responsabilidad Única
- **Patrones de Diseño**: Repository Pattern (en memoria), Dependency Injection, Data Transfer Objects

## 🏗️ Estructura del Proyecto

```
MakeAPI/
├── Models/
│   └── Producto.cs              # Modelo de datos con lógica de negocio
├── Services/
│   ├── IProductoService.cs      # Interfaz (contrato) del servicio
│   └── ProductoService.cs       # Implementación del servicio
├── DTOs/
│   ├── ProductoDTO.cs           # DTO para respuestas
│   ├── CrearProductoDTO.cs      # DTO para creación
│   └── ActualizarProductoDTO.cs # DTO para actualización
├── Controllers/
│   └── ProductosController.cs   # Controlador con endpoints
├── Program.cs                    # Configuración de la aplicación
└── MakeAPI.csproj              # Archivo de proyecto
```

## 🎓 Conceptos Clave Explicados

### 1. **Modelos (Models)**
- **Producto.cs**: Clase que representa un producto
- ✅ **POO**: Encapsulación con propiedades privadas (`_stock`)
- ✅ **Validaciones**: En el constructor para garantizar datos consistentes
- ✅ **Métodos**: `ActualizarStock()` para mantener la lógica de negocio
- ✅ **Propiedades Calculadas**: `Disponible` se calcula basada en `Stock`

```csharp
// Ejemplo de validación en el constructor
public Producto(string nombre, decimal precio, string categoria, int stock)
{
    if (string.IsNullOrWhiteSpace(nombre))
        throw new ArgumentException("El nombre no puede estar vacío");
    // ... más validaciones
}
```

### 2. **Servicios (Services)**
- **IProductoService.cs**: Interfaz que define el contrato (SOLID - Dependency Inversion Principle)
- **ProductoService.cs**: Implementa la lógica de negocio con List<Producto> en memoria

Ventajas:
- 🔄 Fácil de testear: Se puede crear un mock del servicio
- 🎯 Desacoplamiento: El controlador no conoce los detalles de implementación
- 📚 Reutilizable: Se puede usar el mismo servicio en diferentes contextos

```csharp
// En el controlador, se inyecta la dependencia
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;
    
    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
    }
}
```

### 3. **DTOs (Data Transfer Objects)**
- **ProductoDTO**: Para enviar datos al cliente
- **CrearProductoDTO**: Para recibir datos en POST
- **ActualizarProductoDTO**: Para recibir datos en PUT

Razones para usar DTOs:
- 🔐 Seguridad: No exponemos toda la lógica interna
- 🎯 Flexibilidad: Podemos cambiar el modelo sin afectar la API
- 📦 Claridad: El cliente sabe exactamente qué datos enviar/recibir

### 4. **Controladores (Controllers)**
- **ProductosController.cs**: Maneja las peticiones HTTP
- Implementa operaciones CRUD sobre productos
- Gestiona respuestas HTTP apropiadas (200, 201, 404, 400, etc.)

```csharp
[HttpGet]
[ProduceResponseType(StatusCodes.Status200OK)]
public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerTodos()
{
    var productos = await _productoService.ObtenerTodosAsync();
    var productosDTO = productos.Select(/* conversión a DTO */).ToList();
    return Ok(productosDTO);
}
```

## 📡 Endpoints RESTful

### 1. Obtener todos los productos
```
GET /api/productos
```
**Respuesta (200 OK):**
```json
[
    {
        "id": 1,
        "nombre": "Laptop Dell",
        "precio": 899.99,
        "categoria": "Electrónica",
        "stock": 5,
        "disponible": true,
        "fechaActualizacion": "2026-03-16T10:30:00"
    },
    ...
]
```

### 2. Obtener un producto por ID
```
GET /api/productos/1
```
**Respuesta (200 OK):**
```json
{
    "id": 1,
    "nombre": "Laptop Dell",
    "precio": 899.99,
    "categoria": "Electrónica",
    "stock": 5,
    "disponible": true,
    "fechaActualizacion": "2026-03-16T10:30:00"
}
```

### 3. Crear un nuevo producto
```
POST /api/productos
Content-Type: application/json

{
    "nombre": "Monitor Samsung",
    "precio": 349.99,
    "categoria": "Electrónica",
    "stock": 10
}
```
**Respuesta (201 Created):**
```json
{
    "id": 5,
    "nombre": "Monitor Samsung",
    "precio": 349.99,
    "categoria": "Electrónica",
    "stock": 10,
    "disponible": true,
    "fechaActualizacion": "2026-03-16T10:30:00"
}
```

### 4. Actualizar un producto
```
PUT /api/productos/1
Content-Type: application/json

{
    "nombre": "Laptop ASUS",
    "precio": 1099.99,
    "categoria": "Electrónica"
}
```
**Respuesta (200 OK):**
```json
{
    "id": 1,
    "nombre": "Laptop ASUS",
    "precio": 1099.99,
    "categoria": "Electrónica",
    "stock": 5,
    "disponible": true,
    "fechaActualizacion": "2026-03-16T10:30:00"
}
```

### 5. Eliminar un producto
```
DELETE /api/productos/1
```
**Respuesta (204 No Content)** - Sin cuerpo de respuesta

### 6. Actualizar stock de un producto (PATCH)
```
PATCH /api/productos/1/stock?cantidad=5
```
**Parámetro:**
- `cantidad`: Número a sumar (positivo) o restar (negativo)

**Respuesta (200 OK):**
```json
{
    "id": 1,
    "nombre": "Laptop Dell",
    "precio": 899.99,
    "categoria": "Electrónica",
    "stock": 10,      // Cambió de 5 a 10 (5 + 5)
    "disponible": true,
    "fechaActualizacion": "2026-03-16T10:30:00"
}
```

## 🚀 Cómo Ejecutar

### Prerequisitos
- .NET SDK 6.0 o superior
- Visual Studio Code o Visual Studio

### Pasos
1. **Abrir terminal en la carpeta del proyecto**
```bash
cd MakeAPI
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Ejecutar la API**
```bash
dotnet run
```

4. **Acceder a la API**
- URL base: `https://localhost:7001` (o el puerto que muestre en consola)
- Ver la documentación: `https://localhost:7001/openapi/v1.json`

## 🧪 Ejemplos de Prueba (con cURL)

### Obtener todos los productos
```bash
curl -X GET "https://localhost:7001/api/productos" -H "accept: application/json"
```

### Crear un producto
```bash
curl -X POST "https://localhost:7001/api/productos" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d "{\"nombre\":\"Webcam\",\"precio\":79.99,\"categoria\":\"Accesorios\",\"stock\":20}"
```

### Actualizar stock
```bash
curl -X PATCH "https://localhost:7001/api/productos/1/stock?cantidad=10" \
  -H "accept: application/json"
```

### Eliminar un producto
```bash
curl -X DELETE "https://localhost:7001/api/productos/1" \
  -H "accept: application/json"
```

## 📚 Principios SOLID Implementados

| Principio | Explicación | Implementación |
|-----------|-----------|-----------------|
| **S** - Single Responsibility | Cada clase tiene una única responsabilidad | `Producto` gestiona producto, `ProductoService` gestiona lógica |
| **O** - Open/Closed | Abierto para extensión, cerrado para modificación | Uso de interfaces `IProductoService` |
| **L** - Liskov Substitution | Las clases derivadas pueden reemplazar a las base | `ProductoService` implementa `IProductoService` |
| **I** - Interface Segregation | Interfaces específicas, no "gordas" | `IProductoService` solo métodos de producto |
| **D** - Dependency Inversion | Depender de abstracciones, no de implementaciones | Inyección de `IProductoService` en controlador |

## 🔍 Validaciones Implementadas

### En la Clase Producto
- ✅ Nombre no puede ser vacío
- ✅ Precio debe ser > 0
- ✅ Stock no puede ser negativo
- ✅ Stock solo se modifica mediante `ActualizarStock()`

### En el Servicio
- ✅ No permite productos con nombres duplicados
- ✅ Valida que las operaciones de stock sean válidas
- ✅ No permite stock negativo

### En el Controlador
- ✅ Valida los DTOs antes de procesarlos
- ✅ Retorna códigos HTTP apropiad

os
- ✅ Captura excepciones y devuelve mensajes claros

## 📝 Notas para Alumnos

1. **DTOs**: Siempre conversiona modelos a DTOs en los endpoints
2. **Validaciones**: Repite validaciones en diferentes capas (Modelo, Servicio, Controlador)
3. **Nombres de métodos**: Usa verbos expresivos (ObtenerTodos, CrearAsync, ActualizarStock)
4. **Documentación**: Usa comentarios XML (///) para documentar clases y métodos
5. **Async/Await**: Aunque usamos una lista en memoria, se implementa con async para facilitar migración a BD

## 🎯 Próximos Pasos (Mejoras Futuras)

- [ ] Base de datos real (Entity Framework Core)
- [ ] Autenticación y autorización
- [ ] Logging
- [ ] Paginación de resultados
- [ ] Filtros de búsqueda
- [ ] Unit Tests
- [ ] Docker
- [ ] CI/CD

---

**¡Feliz aprendizaje! 🎓**
