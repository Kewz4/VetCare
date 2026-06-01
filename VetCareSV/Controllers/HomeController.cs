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
}
