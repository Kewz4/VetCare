using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public interface IHistorialService
{
    Task<IEnumerable<HistorialMedico>> ObtenerPorMascotaAsync(int mascotaId);
    Task<HistorialMedico?> ObtenerPorCitaAsync(int citaId);
    Task<HistorialMedico> CrearAsync(CrearHistorialDTO dto);
}
