using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppEscuela.Data;
using WebAppEscuela.DTOs;
using WebAppEscuela.Models;
using WebAppEscuela.Services;

namespace WebAppEscuela.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteService _service;

        public EstudiantesController(IEstudianteService service)
        {
            _service = service;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            try
            {
                var estudiantes = await _service.ObtenerTodosAsync();
                return View(estudiantes.Select(e => new Estudiante
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Email = e.Email,
                    FechaNacimiento = e.FechaNacimiento,
                    Telefono = e.Telefono,
                    Direccion = e.Direccion,
                    FechaRegistro = e.FechaRegistro,
                    Activo = e.Activo
                }).ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener estudiantes: {ex.Message}";
                return View(new List<Estudiante>());
            }
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var dto = await _service.ObtenerPorIdAsync(id.Value);
                var estudiante = new Estudiante
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    FechaNacimiento = dto.FechaNacimiento,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    FechaRegistro = dto.FechaRegistro,
                    Activo = dto.Activo
                };
                return View(estudiante);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,FechaNacimiento,Telefono,Direccion,Activo")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CreateEstudianteDto
                    {
                        Nombre = estudiante.Nombre,
                        Apellido = estudiante.Apellido,
                        Email = estudiante.Email,
                        FechaNacimiento = estudiante.FechaNacimiento,
                        Telefono = estudiante.Telefono,
                        Direccion = estudiante.Direccion,
                        Activo = estudiante.Activo
                    };

                    await _service.CrearAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var dto = await _service.ObtenerPorIdAsync(id.Value);
                var estudiante = new Estudiante
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    FechaNacimiento = dto.FechaNacimiento,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    FechaRegistro = dto.FechaRegistro,
                    Activo = dto.Activo
                };
                return View(estudiante);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Estudiantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,FechaNacimiento,Telefono,Direccion,Activo")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new UpdateEstudianteDto
                    {
                        Id = estudiante.Id,
                        Nombre = estudiante.Nombre,
                        Apellido = estudiante.Apellido,
                        Email = estudiante.Email,
                        FechaNacimiento = estudiante.FechaNacimiento,
                        Telefono = estudiante.Telefono,
                        Direccion = estudiante.Direccion,
                        Activo = estudiante.Activo
                    };

                    await _service.ActualizarAsync(id, dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var dto = await _service.ObtenerPorIdAsync(id.Value);
                var estudiante = new Estudiante
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    FechaNacimiento = dto.FechaNacimiento,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    FechaRegistro = dto.FechaRegistro,
                    Activo = dto.Activo
                };
                return View(estudiante);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al eliminar: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
