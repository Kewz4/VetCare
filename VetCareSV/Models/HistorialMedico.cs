using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class HistorialMedico
{
    public int Id { get; set; }
    [Required] public string Diagnostico { get; set; } = string.Empty;
    [Required] public string Tratamiento { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public int CitaId { get; set; }
    public Cita? Cita { get; set; }
}
