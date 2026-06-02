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

        return View(comercio);
    }
}
