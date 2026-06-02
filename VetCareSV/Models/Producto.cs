using System.ComponentModel.DataAnnotations;
namespace VetCareSV.Models;
public class Producto
{
    public int Id { get; set; }
    [Required][MaxLength(200)] public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal? Precio { get; set; }
    public int ComercioId { get; set; }
    public ComercioAliado? Comercio { get; set; }
}
