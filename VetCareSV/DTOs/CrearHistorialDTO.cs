using System.ComponentModel.DataAnnotations;
namespace VetCareSV.DTOs;
public class CrearHistorialDTO
{
    [Required] public string Diagnostico { get; set; } = string.Empty;
    [Required] public string Tratamiento { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    [Required] public int CitaId { get; set; }
}
