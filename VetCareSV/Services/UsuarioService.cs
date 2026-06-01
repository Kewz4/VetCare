using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;
    public UsuarioService(AppDbContext context) { _context = context; }

    public async Task<Usuario?> LoginAsync(string email, string password)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash)) return null;
        return usuario;
    }

    public async Task<Usuario> RegistrarAsync(CrearUsuarioDTO dto)
    {
        if (await ExisteEmailAsync(dto.Email))
            throw new InvalidOperationException("El email ya está registrado");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = "Dueno",
            FechaRegistro = DateTime.Now
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id) =>
        await _context.Usuarios.Include(u => u.Mascotas).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync() =>
        await _context.Usuarios.Include(u => u.Mascotas).OrderBy(u => u.Nombre).ToListAsync();

    public async Task<bool> ExisteEmailAsync(string email) =>
        await _context.Usuarios.AnyAsync(u => u.Email == email);
}
