using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = ResolveConnectionString();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IVeterinariaService, VeterinariaService>();
builder.Services.AddScoped<IHistorialService, HistorialService>();
builder.Services.AddScoped<IComercioService, ComercioService>();

var app = builder.Build();

// Crear tablas y datos semilla al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Veterinarias.Any())
    {
        db.Veterinarias.AddRange(
            new VetCareSV.Models.Veterinaria { Nombre = "Clínica Veterinaria San Salvador Centro", Direccion = "Calle Arce, San Salvador", Departamento = "San Salvador", Telefono = "2222-1111", Email = "info@vetsancen.sv", Horario = "Lun-Vie 8am-6pm" },
            new VetCareSV.Models.Veterinaria { Nombre = "Veterinaria Santa Ana Animal Care", Direccion = "4a Avenida Sur, Santa Ana", Departamento = "Santa Ana", Telefono = "2441-2200", Email = "info@vetsa.sv", Horario = "Lun-Sáb 9am-5pm" },
            new VetCareSV.Models.Veterinaria { Nombre = "Clínica Mascotas Soyapango", Direccion = "Bulevar del Ejército, Soyapango", Departamento = "San Salvador", Telefono = "2277-3300", Horario = "Lun-Vie 8am-7pm" },
            new VetCareSV.Models.Veterinaria { Nombre = "VetSalud La Libertad", Direccion = "Carretera El Litoral Km 15, La Libertad", Departamento = "La Libertad", Telefono = "2346-5500", Horario = "Mar-Dom 8am-5pm" },
            new VetCareSV.Models.Veterinaria { Nombre = "Veterinaria San Miguel Norte", Direccion = "Av. Roosevelt, San Miguel", Departamento = "San Miguel", Telefono = "2661-4400", Horario = "Lun-Sáb 8am-6pm" }
        );
        db.SaveChanges();
    }

    if (!db.ComerciosAliados.Any())
    {
        db.ComerciosAliados.AddRange(
            new VetCareSV.Models.ComercioAliado { Nombre = "PetShop El Salvador", Categoria = "Tienda", Direccion = "Centro Comercial Metrocentro, San Salvador", Departamento = "San Salvador", Telefono = "2264-5500", Descripcion = "Tienda de mascotas con alimentos, accesorios y juguetes." },
            new VetCareSV.Models.ComercioAliado { Nombre = "Farmacia Veterinaria Dueñas", Categoria = "Farmacia", Direccion = "Colonia Escalón, San Salvador", Departamento = "San Salvador", Telefono = "2263-4400", Descripcion = "Medicamentos y suplementos para toda especie." },
            new VetCareSV.Models.ComercioAliado { Nombre = "Groomers SV - Peluquería Canina", Categoria = "Grooming", Direccion = "Av. Masferrer, San Salvador", Departamento = "San Salvador", Telefono = "7788-9900", Descripcion = "Servicio de baño, corte y estética para perros y gatos." },
            new VetCareSV.Models.ComercioAliado { Nombre = "Agropecuaria Hermanos López", Categoria = "Agropecuaria", Direccion = "Km 25 Carretera a Santa Ana", Departamento = "La Libertad", Telefono = "2338-1100", Descripcion = "Alimentos para ganado, aves y mascotas." },
            new VetCareSV.Models.ComercioAliado { Nombre = "Casa del Acuario SV", Categoria = "Tienda", Direccion = "1a Calle Poniente, Santa Ana", Departamento = "Santa Ana", Telefono = "2441-6600", Descripcion = "Peces, reptiles y accesorios especializados." },
            new VetCareSV.Models.ComercioAliado { Nombre = "PetFood Premium SV", Categoria = "Tienda", Direccion = "Col. San Benito, San Salvador", Departamento = "San Salvador", Telefono = "2264-7700", Descripcion = "Alimentos premium y orgánicos para mascotas." }
        );
        db.SaveChanges();
    }

    // Demo account with pets and appointments for presentation
    if (!db.Usuarios.Any(u => u.Email == "demo@vetcare.sv"))
    {
        var demoUser = new VetCareSV.Models.Usuario
        {
            Nombre = "María García",
            Email = "demo@vetcare.sv",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo1234"),
            Rol = "Usuario",
            FechaRegistro = DateTime.UtcNow.AddMonths(-3)
        };
        db.Usuarios.Add(demoUser);
        db.SaveChanges();

        var vet1 = db.Veterinarias.First();
        var vet2 = db.Veterinarias.Skip(1).First();

        var mascota1 = new VetCareSV.Models.Mascota { Nombre = "Max", Especie = "Perro", Raza = "Labrador", Edad = 3, Color = "Dorado", UsuarioId = demoUser.Id };
        var mascota2 = new VetCareSV.Models.Mascota { Nombre = "Luna", Especie = "Gato", Raza = "Siamés", Edad = 2, Color = "Crema", UsuarioId = demoUser.Id };
        var mascota3 = new VetCareSV.Models.Mascota { Nombre = "Toby", Especie = "Perro", Raza = "Beagle", Edad = 5, Color = "Tricolor", UsuarioId = demoUser.Id };
        db.Mascotas.AddRange(mascota1, mascota2, mascota3);
        db.SaveChanges();

        db.Citas.AddRange(
            new VetCareSV.Models.Cita { MascotaId = mascota1.Id, VeterinariaId = vet1.Id, Fecha = DateTime.UtcNow.AddDays(-30), Motivo = "Vacunación anual", Estado = "Completada" },
            new VetCareSV.Models.Cita { MascotaId = mascota2.Id, VeterinariaId = vet2.Id, Fecha = DateTime.UtcNow.AddDays(-15), Motivo = "Control de peso y revisión general", Estado = "Completada" },
            new VetCareSV.Models.Cita { MascotaId = mascota1.Id, VeterinariaId = vet1.Id, Fecha = DateTime.UtcNow.AddDays(-7), Motivo = "Desparasitación", Estado = "Completada" },
            new VetCareSV.Models.Cita { MascotaId = mascota3.Id, VeterinariaId = vet2.Id, Fecha = DateTime.UtcNow.AddDays(3), Motivo = "Consulta dermatológica", Estado = "Pendiente" },
            new VetCareSV.Models.Cita { MascotaId = mascota2.Id, VeterinariaId = vet1.Id, Fecha = DateTime.UtcNow.AddDays(10), Motivo = "Vacuna antirrábica", Estado = "Pendiente" }
        );
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// No cachear nada — cada respuesta pide al servidor
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        ctx.Context.Response.Headers["Pragma"] = "no-cache";
        ctx.Context.Response.Headers["Expires"] = "0";
    }
});

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// ── Resolución de connection string ───────────────────────────────
static string ResolveConnectionString()
{
    // 1. DATABASE_URL
    var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (!string.IsNullOrEmpty(rawUrl))
    {
        var uri = new Uri(rawUrl);
        var userInfo = uri.UserInfo.Split(':', 2);
        return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};" +
               $"Username={userInfo[0]};Password={userInfo[1]};" +
               $"SSL Mode=Require;Trust Server Certificate=true";
    }

    // 2. Variables individuales de Railway Postgres plugin
    var pgHost = Environment.GetEnvironmentVariable("PGHOST");
    if (!string.IsNullOrEmpty(pgHost))
    {
        var pgPort     = Environment.GetEnvironmentVariable("PGPORT")     ?? "5432";
        var pgDb       = Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway";
        var pgUser     = Environment.GetEnvironmentVariable("PGUSER")     ?? "postgres";
        var pgPassword = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "";
        return $"Host={pgHost};Port={pgPort};Database={pgDb};" +
               $"Username={pgUser};Password={pgPassword};" +
               $"SSL Mode=Require;Trust Server Certificate=true";
    }

    // 3. Local (appsettings.json)
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

    return config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "No se encontró conexión a base de datos. " +
            "Define DATABASE_URL, PGHOST, o DefaultConnection en appsettings.json.");
}
