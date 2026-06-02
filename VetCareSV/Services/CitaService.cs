using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class CitaService : ICitaService
{
    private readonly AppDbContext _context;
    public CitaService(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<Cita>> ObtenerPorUsuarioAsync(int usuarioId) =>
        await _context.Citas
            .Include(c => c.Mascota)
            .Include(c => c.Veterinaria)
            .Include(c => c.HistorialMedico)
            .Where(c => c.Mascota!.UsuarioId == usuarioId)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();

    public async Task<Cita?> ObtenerPorIdAsync(int id) =>
        await _context.Citas
            .Include(c => c.Mascota).ThenInclude(m => m!.Usuario)
            .Include(c => c.Veterinaria)
            .Include(c => c.HistorialMedico)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Cita> CrearAsync(CrearCitaDTO dto)
    {
        var cita = new Cita { Fecha = dto.Fecha, Motivo = dto.Motivo, MascotaId = dto.MascotaId, VeterinariaId = dto.VeterinariaId, Estado = "Pendiente" };
        _context.Citas.Add(cita);
        await _context.SaveChangesAsync();
        return cita;
    }

    public async Task<Cita> CambiarEstadoAsync(int id, string estado)
    {
        var estados = new[] { "Pendiente", "Confirmada", "Completada", "Cancelada" };
        if (!estados.Contains(estado)) throw new InvalidOperationException("Estado inválido");
        var cita = await _context.Citas.FindAsync(id) ?? throw new KeyNotFoundException("Cita no encontrada");
        cita.Estado = estado;
        await _context.SaveChangesAsync();
        return cita;
    }

    public async Task<bool> CancelarAsync(int id, int usuarioId)
    {
        var cita = await _context.Citas.Include(c => c.Mascota).FirstOrDefaultAsync(c => c.Id == id && c.Mascota!.UsuarioId == usuarioId)
            ?? throw new KeyNotFoundException("Cita no encontrada");
        cita.Estado = "Cancelada";
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Cita>> ObtenerTodasAsync() =>
        await _context.Citas.Include(c => c.Mascota).ThenInclude(m => m!.Usuario).Include(c => c.Veterinaria).OrderByDescending(c => c.Fecha).ToListAsync();

    public async Task<Dictionary<string, int>> ObtenerEstadisticasAsync()
    {
        var stats = await _context.Citas.GroupBy(c => c.Estado).Select(g => new { Estado = g.Key, Cantidad = g.Count() }).ToListAsync();
        return stats.ToDictionary(s => s.Estado, s => s.Cantidad);
    }
}
