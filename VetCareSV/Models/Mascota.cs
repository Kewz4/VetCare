using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class Mascota
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100)] public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "La especie es requerida")]
    public string Especie { get; set; } = string.Empty; // Perro, Gato, Ave
    [MaxLength(100)] public string Raza { get; set; } = string.Empty;
    public int? Edad { get; set; }
    public string? Color { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
