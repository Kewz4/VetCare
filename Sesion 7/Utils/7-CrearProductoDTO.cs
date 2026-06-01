// 📁 DTOs/CrearProductoDTO.cs (Para crear)
public class CrearProductoDTO
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public string Categoria { get; set; }
    public int? StockInicial { get; set; }  // Opcional
}