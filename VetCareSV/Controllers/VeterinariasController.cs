using Microsoft.AspNetCore.Mvc;
using VetCareSV.Services;
namespace VetCareSV.Controllers;
public class VeterinariasController : Controller
{
    private readonly IVeterinariaService _service;
    public VeterinariasController(IVeterinariaService service) { _service = service; }

    public async Task<IActionResult> Index(string? departamento, string? termino)
    {
        var vets = await _service.BuscarAsync(departamento, termino);
        ViewBag.Departamento = departamento;
        ViewBag.Termino = termino;
        return View(vets);
    }

    public async Task<IActionResult> Details(int id)
    {
        var vet = await _service.ObtenerPorIdAsync(id);
        if (vet == null) return NotFound();
        return View(vet);
    }
}
