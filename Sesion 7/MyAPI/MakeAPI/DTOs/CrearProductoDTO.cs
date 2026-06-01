namespace MakeAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object para recibir datos al crear un nuevo producto.
    /// Se usa en el método POST.
    /// </summary>
    public class CrearProductoDTO
    {
        /// <summary>
        /// Nombre del producto que se va a crear.
        /// Requerido.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Precio unitario del producto.
        /// Debe ser mayor a 0.
        /// Requerido.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Categoría a la que pertenece el producto.
        /// Opcional.
        /// </summary>
        public string? Categoria { get; set; }

        /// <summary>
        /// Cantidad inicial de stock.
        /// Debe ser >= 0.
        /// Requerido.
        /// </summary>
        public int Stock { get; set; }
    }
}
