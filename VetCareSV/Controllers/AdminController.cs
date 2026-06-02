using Microsoft.AspNetCore.Mvc;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class AdminController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly ICitaService _citaService;
    private readonly IMascotaService _mascotaService;
    public AdminController(IUsuarioService usuarioService, ICitaService citaService, IMascotaService mascotaService)
    { _usuarioService = usuarioService; _citaService = citaService; _mascotaService = mascotaService; }

    private bool EsAdmin() => HttpContext.Session.GetString("UsuarioRol") == "Admin";

    public async Task<IActionResult> Index()
    {
        if (!EsAdmin()) return RedirectToAction("Login", "Usuarios");
        ViewBag.Usuarios = await _usuarioService.ObtenerTodosAsync();
        ViewBag.Citas = await _citaService.ObtenerTodasAsync();
        ViewBag.Mascotas = await _mascotaService.ObtenerTodasAsync();
        ViewBag.Stats = await _citaService.ObtenerEstadisticasAsync();
        return View();
    }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstadoCita(int id, string estado)
    {
        if (!EsAdmin()) return RedirectToAction("Login", "Usuarios");
        try { await _citaService.CambiarEstadoAsync(id, estado); TempData["Exito"] = "Estado actualizado."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction(nameof(Index));
    }
}
