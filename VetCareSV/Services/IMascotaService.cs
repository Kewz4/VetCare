using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public interface IMascotaService
{
    Task<IEnumerable<Mascota>> ObtenerPorUsuarioAsync(int usuarioId);
    Task<Mascota?> ObtenerPorIdAsync(int id);
    Task<Mascota> CrearAsync(CrearMascotaDTO dto, int usuarioId);
    Task<Mascota> ActualizarAsync(int id, CrearMascotaDTO dto, int usuarioId);
    Task<bool> EliminarAsync(int id, int usuarioId);
    Task<IEnumerable<Mascota>> ObtenerTodasAsync();
}
