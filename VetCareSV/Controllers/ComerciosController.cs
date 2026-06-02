using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Models;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class ComerciosController : Controller
{
    private readonly IComercioService _service;
    private readonly AppDbContext _context;
    public ComerciosController(IComercioService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    public async Task<IActionResult> Index(string? categoria, string? termino)
    {
        var comercios = await _service.BuscarAsync(categoria, termino);
        ViewBag.Categoria = categoria;
        ViewBag.Termino = termino;
        return View(comercios);
    }

    public async Task<IActionResult> Details(int id)
    {
        var comercio = await _context.ComerciosAliados.FindAsync(id);
        if (comercio == null) return NotFound();
        return View(comercio);
    }

    [HttpGet]
    public IActionResult RegistroComercio() => View();

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistroComercio(RegistrarComercioDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);

        if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
        {
            ModelState.AddModelError("Email", "Este email ya está registrado.");
            return View(dto);
        }

        var usuario = new Usuario
        {
            Nombre = dto.NombreContacto,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = "Comercio"
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var comercio = new ComercioAliado
        {
            Nombre = dto.NombreComercio,
            Categoria = dto.Categoria,
            Direccion = dto.Direccion,
            Departamento = dto.Departamento,
            Telefono = dto.Telefono,
            Descripcion = dto.Descripcion
        };
        _context.ComerciosAliados.Add(comercio);
        await _context.SaveChangesAsync();

        usuario.ComercioId = comercio.Id;
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
        HttpContext.Session.SetString("UsuarioRol", "Comercio");
        HttpContext.Session.SetInt32("ComercioId", comercio.Id);

        TempData["Exito"] = "¡Comercio registrado! Ya aparece en el directorio.";
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpGet]
    public IActionResult LoginComercio() => RedirectToAction("Login", "Usuarios");

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var uid = HttpContext.Session.GetInt32("UsuarioId");
        if (uid == null) return RedirectToAction("Login", "Usuarios");

        var comercioId = HttpContext.Session.GetInt32("ComercioId");
        if (comercioId == null) return RedirectToAction(nameof(Index));

        var comercio = await _context.ComerciosAliados.FindAsync(comercioId.Value);
        if (comercio == null) return RedirectToAction(nameof(Index));

        var productos = await _context.Productos.Where(p => p.ComercioId == comercioId.Value).OrderBy(p => p.Nombre).ToListAsync();
        ViewBag.Productos = productos;

        // Clients: users whose prescriptions reference any of this comercio's products
        var nombreProductos = productos.Select(p => p.Nombre.ToLower()).ToList();
        var clientes = new List<(VetCareSV.Models.Usuario Usuario, string Mascota, string Producto, DateTime Fecha)>();
        if (nombreProductos.Any())
        {
            var historiales = await _context.HistorialesMedicos
                .Include(h => h.Cita).ThenInclude(c => c!.Mascota).ThenInclude(m => m!.Usuario)
                .Where(h => h.Cita != null && h.Cita.Mascota != null)
                .ToListAsync();
            foreach (var h in historiales)
            {
                var trat = h.Tratamiento.ToLower();
                var match = nombreProductos.FirstOrDefault(n => trat.Contains(n.Split(' ')[0]));
                if (match != null && h.Cita?.Mascota?.Usuario != null)
                    clientes.Add((h.Cita.Mascota.Usuario, h.Cita.Mascota.Nombre, productos.First(p => p.Nombre.ToLower() == match).Nombre, h.Cita.Fecha));
            }
        }
        ViewBag.Clientes = clientes.DistinctBy(c => c.Usuario.Id).OrderByDescending(c => c.Fecha).ToList();

        return View(comercio);
    }

    [HttpGet]
    public async Task<IActionResult> EditarComercio()
    {
        var uid = HttpContext.Session.GetInt32("UsuarioId");
        if (uid == null) return RedirectToAction("Login", "Usuarios");
        var comercioId = HttpContext.Session.GetInt32("ComercioId");
        if (comercioId == null) return RedirectToAction(nameof(Index));
        var comercio = await _context.ComerciosAliados.FindAsync(comercioId.Value);
        if (comercio == null) return RedirectToAction(nameof(Index));
        return View(comercio);
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarComercio(int id, string Nombre, string Direccion, string? Departamento, string? Telefono, string Categoria, string? Descripcion)
    {
        var uid = HttpContext.Session.GetInt32("UsuarioId");
        if (uid == null) return RedirectToAction("Login", "Usuarios");
        var comercio = await _context.ComerciosAliados.FindAsync(id);
        if (comercio == null) return NotFound();
        comercio.Nombre = Nombre.Trim();
        comercio.Direccion = Direccion.Trim();
        comercio.Departamento = Departamento?.Trim();
        comercio.Telefono = Telefono?.Trim();
        comercio.Categoria = Categoria;
        comercio.Descripcion = Descripcion?.Trim();
        await _context.SaveChangesAsync();
        TempData["Exito"] = "Información del comercio actualizada.";
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> AgregarProducto(string Nombre, string? Descripcion, decimal? Precio, string? ImageUrl, int ComercioId)
    {
        if (!string.IsNullOrWhiteSpace(Nombre))
        {
            _context.Productos.Add(new Producto { Nombre = Nombre.Trim(), Descripcion = Descripcion?.Trim(), Precio = Precio, ImageUrl = ImageUrl?.Trim(), ComercioId = ComercioId });
            await _context.SaveChangesAsync();
            TempData["Exito"] = "Producto agregado.";
        }
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> SubirCSV(Microsoft.AspNetCore.Http.IFormFile? archivo, int ComercioId)
    {
        if (archivo == null || archivo.Length == 0) { TempData["Error"] = "Selecciona un archivo CSV."; return RedirectToAction(nameof(Dashboard)); }
        int count = 0;
        using var reader = new System.IO.StreamReader(archivo.OpenReadStream());
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            if (parts.Length < 1 || string.IsNullOrWhiteSpace(parts[0])) continue;
            var nombre = parts[0].Trim();
            var desc = parts.Length > 1 ? parts[1].Trim() : null;
            decimal? precio = null;
            if (parts.Length > 2 && decimal.TryParse(parts[2].Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var p)) precio = p;
            var imgUrl = parts.Length > 3 ? parts[3].Trim() : null;
            _context.Productos.Add(new Producto { Nombre = nombre, Descripcion = string.IsNullOrEmpty(desc) ? null : desc, Precio = precio, ImageUrl = string.IsNullOrEmpty(imgUrl) ? null : imgUrl, ComercioId = ComercioId });
            count++;
        }
        await _context.SaveChangesAsync();
        TempData["Exito"] = $"{count} productos importados desde CSV.";
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var prod = await _context.Productos.FindAsync(id);
        if (prod != null) { _context.Productos.Remove(prod); await _context.SaveChangesAsync(); TempData["Exito"] = "Producto eliminado."; }
        return RedirectToAction(nameof(Dashboard));
    }
}
