using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class CitasController : Controller
{
    private readonly ICitaService _citaService;
    private readonly IMascotaService _mascotaService;
    private readonly IVeterinariaService _vetService;
    private readonly AppDbContext _context;
    public CitasController(ICitaService citaService, IMascotaService mascotaService, IVeterinariaService vetService, AppDbContext context)
    { _citaService = citaService; _mascotaService = mascotaService; _vetService = vetService; _context = context; }

    private int? GetUserId() => HttpContext.Session.GetInt32("UsuarioId");

    public async Task<IActionResult> Index()
    {
        var id = GetUserId(); if (id == null) return RedirectToAction("Login", "Usuarios");
        var citas = await _citaService.ObtenerPorUsuarioAsync(id.Value);
        return View(citas);
    }

    public async Task<IActionResult> Details(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var cita = await _citaService.ObtenerPorIdAsync(id);
        if (cita == null || cita.Mascota?.UsuarioId != uid.Value) return NotFound();

        // Product matching from prescription
        if (cita.HistorialMedico != null)
        {
            var texto = (cita.HistorialMedico.Tratamiento + " " + cita.HistorialMedico.Diagnostico).ToLower();
            var palabras = texto.Split(new[]{' ', ',', '.', ';', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length >= 4).Distinct().ToList();
            var productos = await _context.Productos
                .Include(p => p.Comercio)
                .Where(p => palabras.Any(w => p.Nombre.ToLower().Contains(w)))
                .OrderBy(p => p.ComercioId)
                .ToListAsync();
            ViewBag.ProductosReceta = productos;
        }

        return View(cita);
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancelar(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        try { await _citaService.CancelarAsync(id, uid.Value); TempData["Exito"] = "Cita cancelada."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Create(int? vetId)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var mascotas = await _mascotaService.ObtenerPorUsuarioAsync(uid.Value);
        var vets = await _vetService.ObtenerTodasAsync();
        ViewBag.Mascotas = new SelectList(mascotas, "Id", "Nombre");
        ViewBag.Veterinarias = new SelectList(vets, "Id", "Nombre", vetId);
        ViewBag.NoPets = !mascotas.Any();
        return View();
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearCitaDTO dto)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        if (!ModelState.IsValid)
        {
            var mascotas = await _mascotaService.ObtenerPorUsuarioAsync(uid.Value);
            var vets = await _vetService.ObtenerTodasAsync();
            ViewBag.Mascotas = new SelectList(mascotas, "Id", "Nombre");
            ViewBag.Veterinarias = new SelectList(vets, "Id", "Nombre");
            return View(dto);
        }
        try { await _citaService.CrearAsync(dto); TempData["Exito"] = "Cita agendada exitosamente."; return RedirectToAction(nameof(Index)); }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); return View(dto); }
    }
}
