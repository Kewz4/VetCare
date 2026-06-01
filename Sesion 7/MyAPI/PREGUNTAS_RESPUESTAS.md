# ❓ Preguntas y Respuestas Frecuentes

## Objeto Producto

### P1: ¿Por qué Stock es privado?
```csharp
private int _stock;
public int Stock
{
    get { return _stock; }
    private set { _stock = value; }
}
```

**R:** Para garantizar que el stock SOLO se modifique a través del método `ActualizarStock()`, que contiene validaciones importantes. Esto es **Encapsulación**.

```csharp
// ✅ Correcto: Usa validaciones
producto.ActualizarStock(10); // Valida

// ❌ Incorrecto: Si Stock fuera público
producto.Stock = -100; // ¡Sin validaciones!
```

---

### P2: ¿Qué es una propiedad calculada?
```csharp
public bool Disponible
{
    get { return _stock > 0; }
}
```

**R:** Es una propiedad que se calcula en tiempo real basada en otros datos. No se almacena, se genera cuando se accede.

```csharp
var producto = new Producto("Laptop", 999, "Electrónica", 0);
Console.WriteLine(producto.Disponible); // false (se calcula porque Stock = 0)

producto.ActualizarStock(5);
Console.WriteLine(producto.Disponible); // true (se recalcula porque Stock = 5)
```

---

### P3: ¿Por qué validar en el constructor?
```csharp
public Producto(string nombre, decimal precio, ...)
{
    if (precio <= 0)
        throw new ArgumentException("El precio debe ser > 0");
    // ...
}
```

**R:** Para garantizar que NUNCA habrá un Producto inválido en el sistema. Falla rápido.

```csharp
// ❌ Intento falla
var productoMalo = new Producto("Test", -50, "Cat", 10); // Excepción

// ✅ No puedes crear objetos inválidos
```

---

## Servicios (IProductoService y ProductoService)

### P4: ¿Por qué usar una Interfaz?
```csharp
public interface IProductoService { ... }
public class ProductoService : IProductoService { ... }
```

**R:** Para poder cambiar la implementación fácilmente:

```csharp
// Hoy: En memoria
builder.Services.AddSingleton<IProductoService, ProductoService>();

// Mañana: Con base de datos (sin cambiar el controlador)
builder.Services.AddScoped<IProductoService, ProductoServiceBD>();

// El controlador siempre usa IProductoService
public ProductosController(IProductoService service) { ... }
```

---

### P5: ¿Por qué Task<...> AsyncTask?

```csharp
public async Task<List<Producto>> ObtenerTodosAsync() { ... }
```

**R:** Aunque ahora usamos lista en memoria (rápido), así preparamos el código para una BD real (lento). Las operaciones en BD son asíncronas.

```csharp
// Hoy: Rápido (pero async hace que sea escalable)
var productos = await _service.ObtenerTodosAsync();

// Mañana: Si la BD es lenta, el resto de solicitudes no se bloquean
// ASP.NET puede atender otras solicitudes mientras espera
```

---

### P6: ¿Qué hace `_productos.FirstOrDefault()`?

```csharp
var producto = _productos.FirstOrDefault(p => p.Id == id);
```

**R:** Busca el primer elemento que cumpe la condición.
- Si lo encuentra: retorna el elemento
- Si no lo encuentra: retorna null (para tipos referencia) o valor default

```csharp
var p1 = _productos.FirstOrDefault(p => p.Id == 1); // Producto si existe
var p2 = _productos.FirstOrDefault(p => p.Id == 999); // null
```

---

## DTOs

### P7: ¿Por qué tenemos 3 DTOs diferentes?

**R:** Cada operación necesita datos diferentes:

```csharp
// ProductoDTO: Para leer (GET)
// - No envía ID (se genera automáticamente)
// - Todos los campos
public class ProductoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    // Incluye Stock, Disponible, etc.
}

// CrearProductoDTO: Para crear (POST)
// - NO tiene ID (se asigna automáticamente)
// - Campos requeridos/opcionales según negocio
public class CrearProductoDTO
{
    public string Nombre { get; set; }     // Requerido
    public decimal Precio { get; set; }    // Requerido  
    public string? Categoria { get; set; } // Opcional
    public int Stock { get; set; }         // Requerido
}

// ActualizarProductoDTO: Para actualizar (PUT)
// - NO tiene ID (va en la URL)
// - Menos campos que CrearProductoDTO
public class ActualizarProductoDTO
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public string? Categoria { get; set; }
    // NO tiene Stock (se usa PATCH para eso)
}
```

---

### P8: ¿Por qué no usamos el modelo Producto directamente?

**Malo:**
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<Producto>> ObtenerPorId(int id)
{
    return Ok(await _service.ObtenerPorIdAsync(id)); // ¡Expone el modelo!
}
```

**Problemas:**
1. Exponemos toda la clase (incluyendo cosas privadas)
2. Si modificamos Producto, la API cambia
3. No podemos ocultar información sensible

**Bien:**
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<ProductoDTO>> ObtenerPorId(int id)
{
    var producto = await _service.ObtenerPorIdAsync(id);
    var dto = new ProductoDTO { /* mapeo */ };
    return Ok(dto); // Solo exponemos lo necesario
}
```

---

## Controlador

### P9: ¿Qué significa [HttpGet]?

**R:** Es un atributo que le dice a ASP.NET que este método maneja peticiones GET HTTP.

```csharp
[HttpGet]               // GET /api/productos
public IActionResult ObtenerTodos() { }

[HttpGet("{id}")]       // GET /api/productos/123
public IActionResult ObtenerPorId(int id) { }

[HttpPost]              // POST /api/productos
public IActionResult Crear(...) { }

[HttpPut("{id}")]       // PUT /api/productos/123
public IActionResult Actualizar(int id, ...) { }

[HttpDelete("{id}")]    // DELETE /api/productos/123
public IActionResult Eliminar(int id) { }

[HttpPatch("{id}/stock")] // PATCH /api/productos/123/stock
public IActionResult ActualizarStock(int id) { }
```

---

### P10: ¿Qué es ActionResult?

```csharp
public ActionResult<ProductoDTO> ObtenerPorId(int id)
{
    return Ok(producto);      // 200 OK
    return NotFound();         // 404 Not Found
    return BadRequest();       // 400 Bad Request
    return NoContent();        // 204 No Content
}
```

**R:** Clase base para todos los posibles resultados de una acción.Permite retornar diferentes códigos HTTP.

---

### P11: ¿Qué es CreatedAtAction?

```csharp
[HttpPost]
public async Task<ActionResult<ProductoDTO>> Crear(CrearProductoDTO dto)
{
    var producto = await _service.CrearAsync(...);
    
    return CreatedAtAction(
        nameof(ObtenerPorId),      // Nombre del método para obtener
        new { id = producto.Id },   // Parámetro de ruta
        producto                    // Cuerpo de respuesta
    );
}
```

**R:** Retorna:
- Código HTTP 201 (Created)
- Header `Location`: `/api/productos/5` (URL del nuevo recurso)
- Cuerpo: El producto creado

```
HTTP/1.1 201 Created
Location: /api/productos/5

{
    "id": 5,
    "nombre": "Monitor",
    ...
}
```

---

### P12: ¿Por qué validar en el controlador?

```csharp
[HttpPost]
public async Task<IActionResult> Crear(CrearProductoDTO dto)
{
    // Validación 1: Estructura del DTO
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    
    // Validación 2: Reglas de negocio específicas
    if (dto.Precio <= 0)
        return BadRequest("Precio debe ser > 0");
    
    // ...
}
```

**R:** Para retornar errores HTTP apropiados al cliente.

```csharp
// Si no validamos en el controlador:
// - El DTO llega vacío/inválido al servicio
// - El servicio lanza excepción
// - El cliente recibe 500 Internal Server Error (culpa nuestra)

// Validando en el controlador:
// - El cliente recibe 400 Bad Request (culpa del cliente)
// - Mensaje claro de qué está mal
```

---

### P13: ¿Qué es [FromBody]?

```csharp
[HttpPost]
public async Task<IActionResult> Crear([FromBody] CrearProductoDTO dto)
{
    // dto contiene los datos del cuerpo JSON
}
```

**R:** Le dice a ASP.NET que deserialice el cuerpo JSON a un objeto C#.

```json
// Solicitud
POST /api/productos
{
    "nombre": "Laptop",
    "precio": 999.99,
    "categoria": "Electrónica",
    "stock": 5
}
```

Se convierte automáticamente a:
```csharp
var dto = new CrearProductoDTO
{
    Nombre = "Laptop",
    Precio = 999.99,
    Categoria = "Electrónica",
    Stock = 5
};
```

---

## Program.cs y Configuración

### P14: ¿Qué es AddSingleton vs AddScoped vs AddTransient?

```csharp
// SINGLETON: Una instancia para toda la aplicación
builder.Services.AddSingleton<IProductoService, ProductoService>();
// ✅ Cuando: Servicio sin estado (no guarda datos entre solicitudes)
// ❌ Cuándo no: Servicio que accede a BD (problemas concurrentes)

// SCOPED: Nueva instancia por solicitud HTTP
builder.Services.AddScoped<IProductoService, ProductoService>();
// ✅ Cuando: Servicio que accede a BD (Entity Framework lo usa)
// ✅ Cuando: Servicio que tiene estado por solicitud

// TRANSIENT: Nueva instancia cada vez
builder.Services.AddTransient<IProductoService, ProductoService>();
// ✅ Cuando: Servicio sin estado pero que se crea frecuentemente
// ❌ Cuando: Desconoces cómo usar los otros dos
```

Comparación visual:
```
Solicitud HTTP 1:
  ProductoService instancia A ────┐
                                  ├─ Singleton (siempre es A)
Solicitud HTTP 2:                 │
  ProductoService instancia A ────┘

Solicitud HTTP 1:
  ProductoService instancia A
                                  
Solicitud HTTP 2:
  ProductoService instancia B ──── Scoped (diferente por solicitud)

Solicitud HTTP 1:
  ProductoService instancia A
  ProductoService instancia A' ─── Transient (nueva cada uso)
```

---

### P15: ¿Por qué usar async/await en Program.cs?

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
```

**R:** En este caso NO se usa porque es la configuración inicial. Pero cuando accedemos a recursos:

```csharp
// En un servicio (sí es async)
public async Task<List<Producto>> ObtenerTodosAsync()
{
    // Simula operación lenta (BD)
    await Task.Delay(1000);
    return _productos;
}

// En el controlador (sí es async)
public async Task<IActionResult> Obtener()
{
    var productos = await _productoService.ObtenerTodosAsync();
    return Ok(productos);
}
```

---

## Mejores Prácticas

### P16: ¿Qué debería cambiar al implementar una BD real?

**Hoy (en memoria):**
```csharp
public class ProductoService : IProductoService
{
    private List<Producto> _productos;
}
```

**Mañana (con BD):**
```csharp
public class ProductoServiceBD : IProductoService
{
    private readonly DbContext _context;
    
    public async Task<List<Producto>> ObtenerTodosAsync()
    {
        return await _context.Productos.ToListAsync();
    }
}
```

**En Program.cs:**
```csharp
// Cambiar esta línea
builder.Services.AddScoped<IProductoService, ProductoServiceBD>();

// El controlador NO cambia
// Los DTOs NO cambian
// Los modelos NO cambian (mucho)
// Solo cambia la implementación del servicio
```

---

### P17: ¿Cómo agregaría paginación?

```csharp
// Agregar parámetros opcionales
[HttpGet]
public async Task<IActionResult> ObtenerTodos(
    [FromQuery] int pagina = 1,
    [FromQuery] int tamaño = 10
)
{
    var productos = await _service.ObtenerTodosAsync();
    
    var paginados = productos
        .Skip((pagina - 1) * tamaño)
        .Take(tamaño)
        .ToList();
    
    return Ok(new
    {
        total = productos.Count,
        pagina,
        tamaño,
        productos = paginados
    });
}
```

**Uso:**
```
GET /api/productos?pagina=1&tamaño=10
GET /api/productos?pagina=2&tamaño=10
```

---

## Debugging

### P18: ¿Mi API no arranca. Qué hago?

**Verificar:**

1. ¿Hay errores de compilación?
   ```bash
   dotnet build
   ```

2. ¿El puerto está disponible?
   ```bash
   # Ver puerto en launchSettings.json
   cat Properties/launchSettings.json
   ```

3. ¿Las dependencias están registradas?
   ```csharp
   // En Program.cs
   builder.Services.AddSingleton<IProductoService, ProductoService>();
   ```

4. ¿Los controladores se mapean?
   ```csharp
   app.MapControllers();
   ```

---

### P19: Obtengo 404 cuando llamo a `/api/productos`

**Verificar:**

1. ¿El controlador tiene el atributo correcto?
   ```csharp
   [ApiController]
   [Route("api/[controller]")]  // Importante
   public class ProductosController { }
   ```

2. ¿El método tiene el atributo HTTP?
   ```csharp
   [HttpGet]  // Importante
   public IActionResult ObtenerTodos() { }
   ```

3. ¿MapControllers está en Program.cs?
   ```csharp
   app.MapControllers();
   ```

---

### P20: Obtengo erro de serialización JSON

**Problema:**
```
System.Text.Json.JsonException: 'El tipo no puede ser null'
```

**Causa:** Una propiedad es null pero el type es non-nullable

**Soluciones:**

```csharp
// Opción 1: Hacer la propiedad nullable
public class ProductoDTO
{
    public string? Nombre { get; set; }
}

// Opción 2: Dar valor default
public class ProductoDTO
{
    public string Nombre { get; set; } = string.Empty;
}

// Opción 3: Usar [Required] para validar
public class CrearProductoDTO
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
}

// En Program.cs, permitir null en JSON:
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = 
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
```

---

**¡Mucho éxito con el proyecto! Si tienes más preguntas, repasa los archivos de documentación. 🎓**
