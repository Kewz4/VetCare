using System.ComponentModel.DataAnnotations;
namespace VetCareSV.DTOs;
public class CrearMascotaDTO
{
    [Required(ErrorMessage = "El nombre es requerido")] public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "La especie es requerida")] public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    public int? Edad { get; set; }
    public string? Color { get; set; }
}
