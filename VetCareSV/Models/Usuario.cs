using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class Usuario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100)] public string Nombre { get; set; } = string.Empty;
    [Required][EmailAddress][MaxLength(150)] public string Email { get; set; } = string.Empty;
    [Required] public string PasswordHash { get; set; } = string.Empty;
    [Required] public string Rol { get; set; } = "Dueno"; // Dueno, Veterinario, Admin
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public int? ComercioId { get; set; }
    public ComercioAliado? Comercio { get; set; }
    public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
}
