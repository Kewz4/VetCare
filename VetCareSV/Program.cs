using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Railway provee DATABASE_URL como postgres://user:pass@host:port/db
// Si no existe, se cae con mensaje claro.
var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrEmpty(rawUrl))
{
    // Convertir formato postgres:// a formato Npgsql
    var uri = new Uri(rawUrl);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("No se encontró conexión a base de datos. Define DATABASE_URL o DefaultConnection.");
}

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

// Crear tablas automáticamente al arrancar (no requiere archivos de migración)
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
