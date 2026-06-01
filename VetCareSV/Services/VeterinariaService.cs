using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class VeterinariaService : IVeterinariaService
{
    private readonly AppDbContext _context;
    public VeterinariaService(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<Veterinaria>> ObtenerTodasAsync() =>
        await _context.Veterinarias.OrderBy(v => v.Nombre).ToListAsync();

    public async Task<IEnumerable<Veterinaria>> BuscarAsync(string? departamento, string? termino)
    {
        var query = _context.Veterinarias.AsQueryable();
        if (!string.IsNullOrEmpty(departamento))
            query = query.Where(v => v.Departamento == departamento);
        if (!string.IsNullOrEmpty(termino))
            query = query.Where(v => v.Nombre.Contains(termino) || v.Direccion.Contains(termino));
        return await query.OrderBy(v => v.Nombre).ToListAsync();
    }

    public async Task<Veterinaria?> ObtenerPorIdAsync(int id) =>
        await _context.Veterinarias.Include(v => v.Citas).FirstOrDefaultAsync(v => v.Id == id);
}
