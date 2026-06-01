namespace MakeAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object para recibir datos al actualizar un producto existente.
    /// Se usa en el método PUT.
    /// </summary>
    public class ActualizarProductoDTO
    {
        /// <summary>
        /// Nuevo nombre del producto.
        /// Requerido.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nuevo precio unitario del producto.
        /// Debe ser mayor a 0.
        /// Requerido.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Nueva categoría del producto.
        /// Opcional.
        /// </summary>
        public string? Categoria { get; set; }
    }
}
