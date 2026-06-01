// 📁 DTOs/ProductoDTO.cs (Para respuestas)
namespace MiAPI.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public bool Disponible { get; set; }
        public int Stock { get; set; }
        // ❌ NO incluimos CostoCompra ni Proveedor
    }
}