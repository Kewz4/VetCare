using Microsoft.AspNetCore.Mvc;
using VetCareSV.Data;
using Microsoft.EntityFrameworkCore;
namespace VetCareSV.Controllers;
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context) { _context = context; }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalMascotas = await _context.Mascotas.CountAsync();
        ViewBag.TotalVeterinarias = await _context.Veterinarias.CountAsync();
        ViewBag.TotalCitas = await _context.Citas.CountAsync();
        ViewBag.TotalComercios = await _context.ComerciosAliados.CountAsync();
        return View();
    }

    public IActionResult Error() => View(new VetCareSV.Models.ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public async Task<IActionResult> Diagnostico()
    {
        var info = new System.Text.StringBuilder();
        try
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            info.AppendLine($"Conexion: {(canConnect ? "OK" : "FALLO")}");
            info.AppendLine($"Usuarios: {await _context.Usuarios.CountAsync()}");
            info.AppendLine($"Mascotas: {await _context.Mascotas.CountAsync()}");
            info.AppendLine($"Veterinarias: {await _context.Veterinarias.CountAsync()}");
            info.AppendLine($"Citas: {await _context.Citas.CountAsync()}");
            info.AppendLine($"Historiales: {await _context.HistorialesMedicos.CountAsync()}");
            info.AppendLine($"Comercios: {await _context.ComerciosAliados.CountAsync()}");
            var ultimosUsuarios = await _context.Usuarios
                .OrderByDescending(u => u.FechaRegistro)
                .Take(5)
                .Select(u => new { u.Id, u.Nombre, u.Email, u.Rol, u.FechaRegistro })
                .ToListAsync();
            info.AppendLine($"\nUltimos registros:");
            foreach (var u in ultimosUsuarios)
                info.AppendLine($"  [{u.Id}] {u.Nombre} | {u.Email} | {u.Rol} | {u.FechaRegistro:yyyy-MM-dd HH:mm}");
        }
        catch (Exception ex)
        {
            info.AppendLine($"ERROR: {ex.Message}");
            if (ex.InnerException != null) info.AppendLine($"INNER: {ex.InnerException.Message}");
        }
        ViewBag.Info = info.ToString();
        return View();
    }
}
