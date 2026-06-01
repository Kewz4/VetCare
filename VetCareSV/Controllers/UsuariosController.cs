using Microsoft.AspNetCore.Mvc;
using VetCareSV.DTOs;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class UsuariosController : Controller
{
    private readonly IUsuarioService _service;
    public UsuariosController(IUsuarioService service) { _service = service; }

    public IActionResult Login() { if (HttpContext.Session.GetInt32("UsuarioId") != null) return RedirectToAction("Index", "Home"); return View(); }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            var usuario = await _service.LoginAsync(dto.Email, dto.Password);
            if (usuario == null) { ModelState.AddModelError("", "Email o contraseña incorrectos"); return View(dto); }
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); return View(dto); }
    }

    public IActionResult Registro() { if (HttpContext.Session.GetInt32("UsuarioId") != null) return RedirectToAction("Index", "Home"); return View(); }

    [HttpPost][ValidateAntiForgeryToken]
    public async Task<IActionResult> Registro(CrearUsuarioDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            var usuario = await _service.RegistrarAsync(dto);
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);
            TempData["Exito"] = "¡Bienvenido a VetCare SV, " + usuario.Nombre + "!";
            return RedirectToAction("Index", "Home");
        }
        catch (InvalidOperationException ex) { ModelState.AddModelError("", ex.Message); return View(dto); }
        catch (Exception ex) { ModelState.AddModelError("", "Error al registrar: " + ex.Message); return View(dto); }
    }

    [HttpPost][ValidateAntiForgeryToken]
    public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Index", "Home"); }

    public async Task<IActionResult> Perfil()
    {
        var id = HttpContext.Session.GetInt32("UsuarioId");
        if (id == null) return RedirectToAction("Login");
        var usuario = await _service.ObtenerPorIdAsync(id.Value);
        if (usuario == null) return RedirectToAction("Login");
        return View(usuario);
    }
}
