using MakeAPI.Models;

namespace MakeAPI.Services
{
    /// <summary>
    /// Interfaz que define el contrato para las operaciones de gestión de productos.
    /// Implementa el patrón de inyección de dependencias y el Principio de Segregación de Interfaz (ISP).
    /// </summary>
    public interface IProductoService
    {
        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de todos los productos disponibles</returns>
        Task<List<Producto>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene un producto específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>
        /// El producto si existe, null si no se encuentra.
        /// </returns>
        Task<Producto?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="nombre">Nombre del producto</param>
        /// <param name="precio">Precio unitario del producto</param>
        /// <param name="categoria">Categoría del producto</param>
        /// <param name="stock">Stock inicial</param>
        /// <returns>El producto creado con su ID asignado</returns>
        /// <exception cref="ArgumentException">Si algún parámetro no cumple las validaciones</exception>
        Task<Producto> CrearAsync(string nombre, decimal precio, string categoria, int stock);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">Identificador del producto a actualizar</param>
        /// <param name="nombre">Nuevo nombre del producto</param>
        /// <param name="precio">Nuevo precio del producto</param>
        /// <param name="categoria">Nueva categoría del producto</param>
        /// <returns>El producto actualizado, null si no existe</returns>
        /// <exception cref="ArgumentException">Si algún parámetro no cumple las validaciones</exception>
        Task<Producto?> ActualizarAsync(int id, string nombre, decimal precio, string categoria);

        /// <summary>
        /// Elimina un producto del sistema.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar</param>
        /// <returns>true si se eliminó exitosamente, false si el producto no existe</returns>
        Task<bool> EliminarAsync(int id);

        /// <summary>
        /// Actualiza el stock de un producto.
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <param name="cantidad">Cantidad a sumar o restar del stock</param>
        /// <returns>El producto con el stock actualizado, null si no existe</returns>
        /// <exception cref="ArgumentException">Si la operación no es válida</exception>
        Task<Producto?> ActualizarStockAsync(int id, int cantidad);
    }
}
