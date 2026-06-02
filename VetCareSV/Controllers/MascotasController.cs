using Microsoft.AspNetCore.Mvc;
using VetCareSV.DTOs;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class MascotasController : Controller
{
    private readonly IMascotaService _service;
    public MascotasController(IMascotaService service) { _service = service; }

    private int? GetUserId() => HttpContext.Session.GetInt32("UsuarioId");

    public async Task<IActionResult> Index()
    {
        var id = GetUserId(); if (id == null) return RedirectToAction("Login", "Usuarios");
        var mascotas = await _service.ObtenerPorUsuarioAsync(id.Value);
        return View(mascotas);
    }

    public async Task<IActionResult> Details(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var mascota = await _service.ObtenerPorIdAsync(id);
        if (mascota == null || mascota.UsuarioId != uid.Value) return NotFound();
        return View(mascota);
    }

    public IActionResult Create() { if (GetUserId() == null) return RedirectToAction("Login", "Usuarios"); return View(); }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearMascotaDTO dto)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        if (!ModelState.IsValid) return View(dto);
        try { await _service.CrearAsync(dto, uid.Value); TempData["Exito"] = "Mascota registrada exitosamente."; return RedirectToAction(nameof(Index)); }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); return View(dto); }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var mascota = await _service.ObtenerPorIdAsync(id);
        if (mascota == null || mascota.UsuarioId != uid.Value) return NotFound();
        var dto = new CrearMascotaDTO { Nombre = mascota.Nombre, Especie = mascota.Especie, Raza = mascota.Raza, Edad = mascota.Edad, Color = mascota.Color };
        ViewBag.MascotaId = id;
        return View(dto);
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CrearMascotaDTO dto)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        if (!ModelState.IsValid) { ViewBag.MascotaId = id; return View(dto); }
        try { await _service.ActualizarAsync(id, dto, uid.Value); TempData["Exito"] = "Mascota actualizada."; return RedirectToAction(nameof(Index)); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); ViewBag.MascotaId = id; return View(dto); }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        var mascota = await _service.ObtenerPorIdAsync(id);
        if (mascota == null || mascota.UsuarioId != uid.Value) return NotFound();
        return View(mascota);
    }

    [HttpPost, ActionName("Delete")][ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var uid = GetUserId(); if (uid == null) return RedirectToAction("Login", "Usuarios");
        try { await _service.EliminarAsync(id, uid.Value); TempData["Exito"] = "Mascota eliminada."; return RedirectToAction(nameof(Index)); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (Exception ex) { TempData["Error"] = ex.Message; return RedirectToAction(nameof(Delete), new { id }); }
    }
}
