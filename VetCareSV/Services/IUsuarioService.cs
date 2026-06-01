using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public interface IUsuarioService
{
    Task<Usuario?> LoginAsync(string email, string password);
    Task<Usuario> RegistrarAsync(CrearUsuarioDTO dto);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    Task<bool> ExisteEmailAsync(string email);
}
