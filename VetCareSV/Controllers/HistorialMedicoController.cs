using Microsoft.AspNetCore.Mvc;
using VetCareSV.DTOs;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class HistorialMedicoController : Controller
{
    private readonly IHistorialService _historialService;
    private readonly IMascotaService _mascotaService;
    private readonly ICitaService _citaService;
    public HistorialMedicoController(IHistorialService historialService, IMascotaService mascotaService, ICitaService citaService)
    { _historialService = historialService; _mascotaService = mascotaService; _citaService = citaService; }

    private int? GetUserId() => HttpContext.Session.GetInt32("UsuarioId");

    public async Task<IActionResult> Mascota(int mascotaId)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var mascota = await _mascotaService.ObtenerPorIdAsync(mascotaId);
        if (mascota == null || mascota.UsuarioId != uid.Value) return NotFound();
        var historial = await _historialService.ObtenerPorMascotaAsync(mascotaId);
        ViewBag.Mascota = mascota;
        return View(historial);
    }

    public async Task<IActionResult> Create(int citaId)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var rol = HttpContext.Session.GetString("UsuarioRol");
        if (rol != "Veterinario" && rol != "Admin") return Forbid();
        var cita = await _citaService.ObtenerPorIdAsync(citaId);
        if (cita == null) return NotFound();
        ViewBag.Cita = cita;
        return View(new CrearHistorialDTO { CitaId = citaId });
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearHistorialDTO dto)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var rol = HttpContext.Session.GetString("UsuarioRol");
        if (rol != "Veterinario" && rol != "Admin") return Forbid();
        if (!ModelState.IsValid) { var cita2 = await _citaService.ObtenerPorIdAsync(dto.CitaId); ViewBag.Cita = cita2; return View(dto); }
        try { await _historialService.CrearAsync(dto); TempData["Exito"] = "Historial médico registrado."; return RedirectToAction("Index", "Admin"); }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); var cita2 = await _citaService.ObtenerPorIdAsync(dto.CitaId); ViewBag.Cita = cita2; return View(dto); }
    }
}
