using Microsoft.AspNetCore.Mvc;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class ComerciosController : Controller
{
    private readonly IComercioService _service;
    public ComerciosController(IComercioService service) { _service = service; }

    public async Task<IActionResult> Index(string? categoria, string? termino)
    {
        var comercios = await _service.BuscarAsync(categoria, termino);
        ViewBag.Categoria = categoria;
        ViewBag.Termino = termino;
        return View(comercios);
    }
}
