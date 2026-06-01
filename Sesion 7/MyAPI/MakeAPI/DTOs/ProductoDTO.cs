namespace MakeAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object para transferir información de un producto en las respuestas.
    /// Se usa en las respuestas GET para evitar exponer la lógica interna.
    /// </summary>
    public class ProductoDTO
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Precio unitario del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Categoría del producto.
        /// </summary>
        public string Categoria { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad en stock del producto.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Indica si el producto está disponible (tiene stock > 0).
        /// </summary>
        public bool Disponible { get; set; }

        /// <summary>
        /// Fecha de creación o modificación (puede agregarse en versiones futuras).
        /// </summary>
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
