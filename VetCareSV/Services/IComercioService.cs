using VetCareSV.Models;
namespace VetCareSV.Services;
public interface IComercioService
{
    Task<IEnumerable<ComercioAliado>> ObtenerTodosAsync();
    Task<IEnumerable<ComercioAliado>> BuscarAsync(string? categoria, string? termino);
}
