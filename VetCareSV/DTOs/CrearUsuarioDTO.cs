using System.ComponentModel.DataAnnotations;
namespace VetCareSV.DTOs;
public class CrearUsuarioDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El email es requerido")][EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "La contraseña es requerida")][MinLength(6)]
    public string Password { get; set; } = string.Empty;
    [Required][Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmarPassword { get; set; } = string.Empty;
}
