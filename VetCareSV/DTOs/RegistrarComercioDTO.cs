using System.ComponentModel.DataAnnotations;
namespace VetCareSV.DTOs;
public class RegistrarComercioDTO
{
    [Required(ErrorMessage = "Nombre requerido")]
    [MaxLength(200)] public string NombreComercio { get; set; } = string.Empty;

    [Required(ErrorMessage = "Categoría requerida")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dirección requerida")]
    public string Direccion { get; set; } = string.Empty;

    public string? Departamento { get; set; }
    public string? Telefono { get; set; }
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Nombre de contacto requerido")]
    [MaxLength(100)] public string NombreContacto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email requerido")]
    [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contraseña requerida")]
    [MinLength(6)] public string Password { get; set; } = string.Empty;
}
