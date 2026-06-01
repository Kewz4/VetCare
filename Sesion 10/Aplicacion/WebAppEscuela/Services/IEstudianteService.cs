using WebAppEscuela.DTOs;
using WebAppEscuela.Models;

namespace WebAppEscuela.Services
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteResponseDto>> ObtenerTodosAsync();
        Task<EstudianteResponseDto> ObtenerPorIdAsync(int id);
        Task<EstudianteResponseDto> CrearAsync(CreateEstudianteDto dto);
        Task<EstudianteResponseDto> ActualizarAsync(int id, UpdateEstudianteDto dto);
        Task<bool> EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
