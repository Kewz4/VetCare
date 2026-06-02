using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class Cita
{
    public int Id { get; set; }
    [Required] public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Pendiente"; // Pendiente, Confirmada, Completada, Cancelada
    public string? Motivo { get; set; }
    public int MascotaId { get; set; }
    public Mascota? Mascota { get; set; }
    public int VeterinariaId { get; set; }
    public Veterinaria? Veterinaria { get; set; }
    public HistorialMedico? HistorialMedico { get; set; }
}
