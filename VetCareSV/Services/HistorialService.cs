using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class HistorialService : IHistorialService
{
    private readonly AppDbContext _context;
    public HistorialService(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<HistorialMedico>> ObtenerPorMascotaAsync(int mascotaId) =>
        await _context.HistorialesMedicos
            .Include(h => h.Cita).ThenInclude(c => c!.Veterinaria)
            .Where(h => h.Cita!.MascotaId == mascotaId)
            .OrderByDescending(h => h.FechaRegistro)
            .ToListAsync();

    public async Task<HistorialMedico?> ObtenerPorCitaAsync(int citaId) =>
        await _context.HistorialesMedicos.Include(h => h.Cita).FirstOrDefaultAsync(h => h.CitaId == citaId);

    public async Task<HistorialMedico> CrearAsync(CrearHistorialDTO dto)
    {
        if (await _context.HistorialesMedicos.AnyAsync(h => h.CitaId == dto.CitaId))
            throw new InvalidOperationException("Esta cita ya tiene historial registrado");

        var historial = new HistorialMedico { Diagnostico = dto.Diagnostico, Tratamiento = dto.Tratamiento, Observaciones = dto.Observaciones, CitaId = dto.CitaId, FechaRegistro = DateTime.UtcNow };
        _context.HistorialesMedicos.Add(historial);

        var cita = await _context.Citas.FindAsync(dto.CitaId);
        if (cita != null) cita.Estado = "Completada";

        await _context.SaveChangesAsync();
        return historial;
    }
}
