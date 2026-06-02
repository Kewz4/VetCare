using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public interface ICitaService
{
    Task<IEnumerable<Cita>> ObtenerPorUsuarioAsync(int usuarioId);
    Task<Cita?> ObtenerPorIdAsync(int id);
    Task<Cita> CrearAsync(CrearCitaDTO dto);
    Task<Cita> CambiarEstadoAsync(int id, string estado);
    Task<bool> CancelarAsync(int id, int usuarioId);
    Task<IEnumerable<Cita>> ObtenerTodasAsync();
    Task<Dictionary<string, int>> ObtenerEstadisticasAsync();
}
