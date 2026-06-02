using System.ComponentModel.DataAnnotations;
namespace VetCareSV.DTOs;
public class CrearCitaDTO
{
    [Required(ErrorMessage = "La fecha es requerida")] public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }
    [Required(ErrorMessage = "Selecciona una mascota")] public int MascotaId { get; set; }
    [Required(ErrorMessage = "Selecciona una veterinaria")] public int VeterinariaId { get; set; }
}
