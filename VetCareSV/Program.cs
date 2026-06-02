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

// Crear tablas automáticamente al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// ── Resolución de connection string ───────────────────────────────
// Orden de prioridad:
//   1. DATABASE_URL  (postgres://user:pass@host:port/db)
//   2. PGHOST + PGPORT + PGDATABASE + PGUSER + PGPASSWORD  (Railway plugin vars)
//   3. appsettings.json DefaultConnection  (desarrollo local)
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
