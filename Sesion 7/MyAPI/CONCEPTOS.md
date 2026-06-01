# 📚 Guía de Conceptos - Arquitectura MVC y SOLID

## 1. ¿Qué es la Arquitectura MVC?

MVC divide la aplicación en tres capas:

```
┌─────────────────────────────────────────────┐
│              CLIENTE (Navegador)             │
└────────────┬────────────────────────────────┘
             │ (solicitudes HTTP)
┌────────────▼────────────────────────────────┐
│   CONTROLADOR (ProductosController)          │
│  - Recibe peticiones HTTP                   │
│  - Llama al servicio                        │
│  - Retorna respuestas JSON                  │
└────────────┬────────────────────────────────┘
             │ (objetos de dominio)
┌────────────▼────────────────────────────────┐
│   SERVICIOS (ProductoService)                │
│  - Lógica de negocio                        │
│  - Utiliza modelos                          │
│  - Acceso a datos                           │
└────────────┬────────────────────────────────┘
             │ (datos)
┌────────────▼────────────────────────────────┐
│   MODELOS (Producto)                         │
│  - Representa datos                         │
│  - Validacione básicas                      │
│  - Métodos de dominio                       │
└─────────────────────────────────────────────┘
```

### M - Model (Modelo)
- **Qué es**: Clase que representa un entidad del negocio
- **Responsabilidad**: Almacenar datos y validaciones básicas
- **Ejemplo**: Clase `Producto` con propiedades y métodos

```csharp
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    
    // Método de negocio
    public void ActualizarStock(int cantidad) { }
}
```

### V - View (Vista)
- **Qué es**: Representación de datos para el cliente
- **En una API**: JSON/XML
- **Nota**: Usamos DTOs como "vistas"

```json
{
    "id": 1,
    "nombre": "Laptop",
    "precio": 899.99,
    "disponible": true
}
```

### C - Controller (Controlador)
- **Qué es**: Intermediario entre cliente y servicios
- **Responsabilidades**:
  - Recibir peticiones HTTP
  - Validar entrada
  - Llamar al servicio
  - Retornar respuesta HTTP

```csharp
[HttpGet("{id}")]
public async Task<ActionResult<ProductoDTO>> ObtenerPorId(int id)
{
    var producto = await _productoService.ObtenerPorIdAsync(id);
    if (producto == null)
        return NotFound();
    return Ok(ConvertirADTO(producto));
}
```

---

## 2. Principios SOLID Explicados

### 📌 S - Single Responsibility Principle (SRP)
**"Una clase debe tener una única razón para cambiar"**

❌ **Malo**: Una clase que gestiona productos Y accede a base datos Y envía emails
```csharp
public class ProductoManager
{
    public void CrearProducto(...) { /* crear */ }
    public void GuardarEnBD(...) { /* BD */ }
    public void EnviarEmail(...) { /* email */ }
}
```

✅ **Bien**: Cada clase tiene una responsabilidad
```csharp
public class Producto { /* datos */ }
public class ProductoService { /* lógica */ }
public class BDProductoRepository { /* acceso datos */ }
public class EmailService { /* emails */ }
```

### 📌 O - Open/Closed Principle (OCP)
**"Abierto para extensión, cerrado para modificación"**

❌ **Malo**: Cambiar el código existente
```csharp
public class ProductoService
{
    if (tipoProducto == "Electrónico") { }
    else if (tipoProducto == "Alimento") { }
    else if (tipoProducto == "Nuevo") { } // Hay que modificar
}
```

✅ **Bien**: Usar interfaces para extender
```csharp
public interface IProductoStrategy
{
    void Procesar(Producto p);
}

public class ElectrónicoStrategy : IProductoStrategy { }
public class AlimentoStrategy : IProductoStrategy { }
// Se pueden agregar nuevas estrategias sin modificar existentes
```

### 📌 L - Liskov Substitution Principle (LSP)
**"Los objetos de una clase derivada deben poder reemplazar a los de su clase base"**

❌ **Malo**: Implementación que no cumple el contrato
```csharp
public interface IMascota
{
    void Hablar();
}

public class Gato : IMascota
{
    public void Hablar() { /* miaúa */ }
}

public class Pez : IMascota
{
    public void Hablar() { throw new NotImplementedException(); } // ¡Peces no hablan!
}
```

✅ **Bien**: Las implementaciones cumplen el contrato
```csharp
public class Perro : IMascota
{
    public void Hablar() { Console.WriteLine("Guau!"); }
}

public class Gato : IMascota
{
    public void Hablar() { Console.WriteLine("Miaú!"); }
}
```

### 📌 I - Interface Segregation Principle (ISP)
**"Muchas interfaces específicas son mejores que pocas genéricas"**

❌ **Malo**: Interfaz "gorda" que obliga a implementar métodos innecesarios
```csharp
public interface ITransportista
{
    void Conducir();
    void Volar();
    void Nadar();
}

public class Conductor : ITransportista
{
    public void Conducir() { /* ok */ }
    public void Volar() { throw new NotImplementedException(); } // No vuela
    public void Nadar() { throw new NotImplementedException(); } // No nada
}
```

✅ **Bien**: Interfaces específicas y pequeñas
```csharp
public interface ITerrestre { void Conducir(); }
public interface IAerea { void Volar(); }
public interface IAcuatica { void Nadar(); }

public class Conductor : ITerrestre
{
    public void Conducir() { /* ok */ }
}

public class Avion : ITerrestre, IAerea
{
    public void Conducir() { /* ok */ }
    public void Volar() { /* ok */ }
}
```

### 📌 D - Dependency Inversion Principle (DIP)
**"Depender de abstracciones, no de implementaciones concretas"**

❌ **Malo**: Dependencia directa de clases concretas
```csharp
public class ProductosController
{
    private readonly ProductoService _service = new ProductoService();
    
    public async Task<IActionResult> Obtener()
    {
        var productos = _service.ObtenerTodos();
        return Ok(productos);
    }
}
```

✅ **Bien**: Inyección de interfaz
```csharp
public class ProductosController
{
    private readonly IProductoService _service;
    
    public ProductosController(IProductoService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task<IActionResult> Obtener()
    {
        var productos = await _service.ObtenerTodosAsync();
        return Ok(productos);
    }
}

// En Program.cs:
builder.Services.AddSingleton<IProductoService, ProductoService>();
```

---

## 3. ¿Qué son los DTOs?

**DTO = Data Transfer Object (Objeto de Transferencia de Datos)**

### ¿Por qué necesitamos DTOs?

1. **Seguridad**: No exponemos toda la lógica interna
   ```csharp
   // ❌ Malo: Exponer el modelo directamente
   public class Producto
   {
       public int Id { get; set; }
       private string _claveEncriptacion; // ¡Expuesto accidentalmente!
   }
   
   // ✅ Bien: Usar DTO que solo expone lo necesario
   public class ProductoDTO
   {
       public int Id { get; set; }
       public string Nombre { get; set; }
       // ¡La clave no está expuesta!
   }
   ```

2. **Flexibilidad**: Podemos cambiar el modelo sin afectar la API
   ```csharp
   // Si queremos agregar más propiedades al modelo:
   public class Producto
   {
       // ... propiedades existentes ...
       public string ProveedorID { get; set; } // Nueva prop
       public DateTime FechaCaracteres { get; set; } // Nueva prop
   }
   
   // El DTO sigue siendo el mismo:
   public class ProductoDTO
   {
       public int Id { get; set; }
       public string Nombre { get; set; }
       // Los clientes no se enteran de los cambios internos
   }
   ```

3. **Validación**: Podemos validar según el tipo de operación
   ```csharp
   // Para crear: nombre requerido
   public class CrearProductoDTO
   {
       [Required]
       public string Nombre { get; set; }
   }
   
   // Para actualizar: nombre puede ser opcional
   public class ActualizarProductoDTO
   {
       public string? Nombre { get; set; } // Opcional
   }
   ```

---

## 4. Inyección de Dependencias

### ¿Qué es?
Es un patrón que permite que una clase reciba sus dependencias en lugar de crearlas.

### Ciclos de Vida

```csharp
// En Program.cs:

// 1. SINGLETON: Misma instancia para toda la aplicación
builder.Services.AddSingleton<IProductoService, ProductoService>();
// Uso: Servicios "sin estado" (stateless)

// 2. TRANSIENT: Nueva instancia cada vez
builder.Services.AddTransient<IProductoService, ProductoService>();
// Uso: Servicios con estado (stateful)

// 3. SCOPED: Nueva instancia por solicitud HTTP
builder.Services.AddScoped<IProductoService, ProductoService>();
// Uso: Servicios que acceden a BD (Entity Framework)
```

### Cómo funciona

```
1. La aplicación inicia
2. Se registran los servicios: builder.Services.AddSingleton<IProductoService, ProductoService>();
3. Se crea el APP
4. Cliente hace una solicitud HTTP
5. ASP.NET ve que ProductosController necesita IProductoService
6. ASP.NET busca IProductoService en el contenedor
7. Encuentra que debe instanciar ProductoService
8. Crea una instancia y la inyecta en el constructor
9. Se ejecuta el controlador
```

---

## 5. Métodos HTTP en REST

| Método | Operación | Idempotente | Seguro |
|--------|-----------|-------------|--------|
| **GET** | Obtener (Read) | ✅ Sí | ✅ Sí |
| **POST** | Crear (Create) | ❌ No | ❌ No |
| **PUT** | Actualizar completo (Update) | ✅ Sí | ❌ No |
| **PATCH** | Actualizar parcial | ⚠️ Puede variar | ❌ No |
| **DELETE** | Eliminar (Delete) | ✅ Sí | ❌ No |

### ¿Idempotente?
Que repetir la operación da el mismo resultado que hacerla una sola vez.

```csharp
// GET es idempotente: no importa cuántas veces lo hagas
GET /api/productos/1  // Respuesta: Producto 1
GET /api/productos/1  // Respuesta: Producto 1 (igual)
GET /api/productos/1  // Respuesta: Producto 1 (igual)

// POST no es idempotente: cada vez crea uno nuevo
POST /api/productos   // Crea: Producto A (ID: 1)
POST /api/productos   // Crea: Producto B (ID: 2) ← Diferente!
POST /api/productos   // Crea: Producto C (ID: 3) ← Diferente!

// PUT es idempotente
PUT /api/productos/1 { nombre: "Laptop" }   // Actualiza a "Laptop"
PUT /api/productos/1 { nombre: "Laptop" }   // Sigue siendo "Laptop"
PUT /api/productos/1 { nombre: "Laptop" }   // Sigue siendo "Laptop"
```

---

## 6. Códigos de Estado HTTP

| Código | Significado | Uso |
|--------|------------|-----|
| **200** | OK | La solicitud fue exitosa |
| **201** | Created | Recurso creado exitosamente |
| **204** | No Content | Recurso eliminado/actualizado (sin respuesta) |
| **400** | Bad Request | Datos inválidos |
| **401** | Unauthorized | No autenticado |
| **403** | Forbidden | Autenticado pero sin permiso |
| **404** | Not Found | Recurso no existe |
| **409** | Conflict | Conflicto (ej: nombre duplicado) |
| **500** | Internal Server Error | Error del servidor |

### Ejemplo en el controlador
```csharp
[HttpPost]
public async Task<ActionResult<ProductoDTO>> Crear([FromBody] CrearProductoDTO dto)
{
    try
    {
        if (dto.Precio <= 0)
            return BadRequest(new { mensaje = "Precio inválido" }); // 400
        
        var producto = await _service.CrearAsync(...);
        
        return CreatedAtAction(nameof(ObtenerPorId), 
            new { id = producto.Id }, producto); // 201
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensaje = "Error" }); // 500
    }
}
```

---

## 7. Validaciones en Capas

Es buena práctica validar en múltiples capas:

```csharp
// CAPA 1: Controlador (validación de entrada)
[HttpPost]
public async Task<IActionResult> Crear(CrearProductoDTO dto)
{
    if (string.IsNullOrWhiteSpace(dto.Nombre))
        return BadRequest("Nombre inválido");
    
    // CAPA 2: Servicio (lógica de negocio)
    var producto = await _service.CrearAsync(dto.Nombre, ...);
    
    return Ok(producto);
}

// En ProductoService:
public async Task<Producto> CrearAsync(string nombre, ...)
{
    if (_productos.Any(p => p.Nombre == nombre))
        throw new ArgumentException("Nombre duplicado");
    
    // CAPA 3: Modelo (validaciones de dominio)
    var producto = new Producto(nombre, precio, ...);
    // El constructor valida internamente
    
    _productos.Add(producto);
    return producto;
}
```

---

## 📝 Resumen

| Concepto | Definición | Beneficio |
|----------|-----------|----------|
| **MVC** | Separación de Models, Views, Controllers | Código organizado y mantenible |
| **SOLID** | 5 principios de diseño | Código flexible y escalable |
| **DTOs** | Objetos para transferir datos | Seguridad y flexibilidad |
| **Inyección** | Pasar donde se necesita | Bajo acoplamiento, fácil de testear |
| **REST** | Convenciones para APIs | Consistencia y estándar |

---

¡Espero que esta guía te ayude a entender mejor la arquitectura! 🚀
