using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class ComercioAliado
{
    public int Id { get; set; }
    [Required][MaxLength(200)] public string Nombre { get; set; } = string.Empty;
    [Required] public string Categoria { get; set; } = string.Empty; // Veterinaria, Tienda, Farmacia
    [Required] public string Direccion { get; set; } = string.Empty;
    public string? Departamento { get; set; }
    public string? Telefono { get; set; }
    public string? Descripcion { get; set; }
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
