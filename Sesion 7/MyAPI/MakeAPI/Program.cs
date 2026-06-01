using MakeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ========== CONFIGURACIÓN DE SERVICIOS ==========
// Se agregan los servicios a contenedor de inyección de dependencias

// Se registra el controlador
builder.Services.AddControllers();

// Se registra el servicio de productos con ciclo de vida Singleton
// En un proyecto real, se usaría Scoped o Transient
builder.Services.AddSingleton<IProductoService, ProductoService>();

// Se agrega documentación OpenAPI/Swagger
builder.Services.AddOpenApi();

// Se agrega CORS si es necesario (para acceso desde diferentes orígenes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// ========== CONFIGURACIÓN DEL PIPELINE HTTP ==========

// Middleware para manejar errores globales
app.UseExceptionHandler("/error");

// Redirigir HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowAll");

// Agregar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // En desarrollo, mostrar documentación OpenAPI
    app.MapOpenApi();
    
    // Agregar interfaz de Swagger UI (parámetro ?p=/ al final de swagger/index.html)
    // Esta línea requiere instalar: dotnet add package Swashbuckle.AspNetCore
    // app.UseSwaggerUI();
}

// Mensaje de bienvenida
Console.WriteLine("==========================================================");
Console.WriteLine("API de Gestión de Productos");
Console.WriteLine("==========================================================");
Console.WriteLine("Endpoints disponibles:");
Console.WriteLine("  GET    /api/productos              - Obtener todos los productos");
Console.WriteLine("  GET    /api/productos/{id}         - Obtener un producto por ID");
Console.WriteLine("  POST   /api/productos              - Crear un nuevo producto");
Console.WriteLine("  PUT    /api/productos/{id}         - Actualizar un producto");
Console.WriteLine("  DELETE /api/productos/{id}         - Eliminar un producto");
Console.WriteLine("  PATCH  /api/productos/{id}/stock   - Actualizar stock");
Console.WriteLine("==========================================================");

app.Run();

