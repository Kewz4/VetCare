using VetCareSV.Models;
namespace VetCareSV.Services;
public interface IVeterinariaService
{
    Task<IEnumerable<Veterinaria>> ObtenerTodasAsync();
    Task<IEnumerable<Veterinaria>> BuscarAsync(string? departamento, string? termino);
    Task<Veterinaria?> ObtenerPorIdAsync(int id);
}
