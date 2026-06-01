using Microsoft.EntityFrameworkCore;
using VetCareSV.Data;
using VetCareSV.DTOs;
using VetCareSV.Models;
namespace VetCareSV.Services;
public class MascotaService : IMascotaService
{
    private readonly AppDbContext _context;
    public MascotaService(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<Mascota>> ObtenerPorUsuarioAsync(int usuarioId) =>
        await _context.Mascotas.Where(m => m.UsuarioId == usuarioId).OrderBy(m => m.Nombre).ToListAsync();

    public async Task<Mascota?> ObtenerPorIdAsync(int id) =>
        await _context.Mascotas.Include(m => m.Usuario).Include(m => m.Citas).ThenInclude(c => c.Veterinaria).FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Mascota> CrearAsync(CrearMascotaDTO dto, int usuarioId)
    {
        var mascota = new Mascota { Nombre = dto.Nombre, Especie = dto.Especie, Raza = dto.Raza, Edad = dto.Edad, Color = dto.Color, UsuarioId = usuarioId };
        _context.Mascotas.Add(mascota);
        await _context.SaveChangesAsync();
        return mascota;
    }

    public async Task<Mascota> ActualizarAsync(int id, CrearMascotaDTO dto, int usuarioId)
    {
        var mascota = await _context.Mascotas.FirstOrDefaultAsync(m => m.Id == id && m.UsuarioId == usuarioId)
            ?? throw new KeyNotFoundException("Mascota no encontrada");
        mascota.Nombre = dto.Nombre; mascota.Especie = dto.Especie; mascota.Raza = dto.Raza;
        mascota.Edad = dto.Edad; mascota.Color = dto.Color;
        await _context.SaveChangesAsync();
        return mascota;
    }

    public async Task<bool> EliminarAsync(int id, int usuarioId)
    {
        var mascota = await _context.Mascotas.FirstOrDefaultAsync(m => m.Id == id && m.UsuarioId == usuarioId)
            ?? throw new KeyNotFoundException("Mascota no encontrada");
        _context.Mascotas.Remove(mascota);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Mascota>> ObtenerTodasAsync() =>
        await _context.Mascotas.Include(m => m.Usuario).OrderBy(m => m.Nombre).ToListAsync();
}
