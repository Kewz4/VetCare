using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class Veterinaria
{
    public int Id { get; set; }
    [Required][MaxLength(200)] public string Nombre { get; set; } = string.Empty;
    [Required] public string Direccion { get; set; } = string.Empty;
    [Required][MaxLength(100)] public string Departamento { get; set; } = string.Empty;
    [MaxLength(20)] public string Telefono { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Horario { get; set; }
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
