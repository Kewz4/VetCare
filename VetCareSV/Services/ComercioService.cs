using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class ComercioService : IComercioService
{
    private readonly AppDbContext _context;
    public ComercioService(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<ComercioAliado>> ObtenerTodosAsync() =>
        await _context.ComerciosAliados.OrderBy(c => c.Categoria).ThenBy(c => c.Nombre).ToListAsync();

    public async Task<IEnumerable<ComercioAliado>> BuscarAsync(string? categoria, string? termino)
    {
        var query = _context.ComerciosAliados.AsQueryable();
        if (!string.IsNullOrEmpty(categoria))
            query = query.Where(c => c.Categoria == categoria);
        if (!string.IsNullOrEmpty(termino))
            query = query.Where(c => c.Nombre.Contains(termino) || (c.Descripcion != null && c.Descripcion.Contains(termino)));
        return await query.OrderBy(c => c.Nombre).ToListAsync();
    }
}
