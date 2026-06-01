using Microsoft.EntityFrameworkCore;
using WebAppEscuela.Data;
using WebAppEscuela.DTOs;
using WebAppEscuela.Models;

namespace WebAppEscuela.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly AppDbContext _context;

        public EstudianteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EstudianteResponseDto>> ObtenerTodosAsync()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return estudiantes.Select(MapToResponseDto).ToList();
        }

        public async Task<EstudianteResponseDto> ObtenerPorIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new KeyNotFoundException($"Estudiante con ID {id} no encontrado");
            
            return MapToResponseDto(estudiante);
        }

        public async Task<EstudianteResponseDto> CrearAsync(CreateEstudianteDto dto)
        {
            // Validar email único
            var emailExiste = await _context.Estudiantes
                .AnyAsync(e => e.Email == dto.Email);
            
            if (emailExiste)
            {
                throw new InvalidOperationException($"El email '{dto.Email}' ya está registrado");
            }

            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                FechaNacimiento = dto.FechaNacimiento,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Activo = dto.Activo,
                FechaRegistro = DateTime.Now
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return MapToResponseDto(estudiante);
        }

        public async Task<EstudianteResponseDto> ActualizarAsync(int id, UpdateEstudianteDto dto)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new KeyNotFoundException($"Estudiante con ID {id} no encontrado");

            // Validar email único si cambió
            if (estudiante.Email != dto.Email)
            {
                var emailExiste = await _context.Estudiantes
                    .AnyAsync(e => e.Email == dto.Email && e.Id != id);
                
                if (emailExiste)
                {
                    throw new InvalidOperationException($"El email '{dto.Email}' ya está registrado");
                }
            }

            estudiante.Nombre = dto.Nombre;
            estudiante.Apellido = dto.Apellido;
            estudiante.Email = dto.Email;
            estudiante.FechaNacimiento = dto.FechaNacimiento;
            estudiante.Telefono = dto.Telefono;
            estudiante.Direccion = dto.Direccion;
            estudiante.Activo = dto.Activo;

            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();

            return MapToResponseDto(estudiante);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new KeyNotFoundException($"Estudiante con ID {id} no encontrado");

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Estudiantes.AnyAsync(e => e.Id == id);
        }

        private static EstudianteResponseDto MapToResponseDto(Estudiante estudiante)
        {
            return new EstudianteResponseDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email = estudiante.Email,
                FechaNacimiento = estudiante.FechaNacimiento,
                Telefono = estudiante.Telefono,
                Direccion = estudiante.Direccion,
                FechaRegistro = estudiante.FechaRegistro,
                Activo = estudiante.Activo
            };
        }
    }
}
