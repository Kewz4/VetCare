using MakeAPI.Models;

namespace MakeAPI.Services
{
    /// <summary>
    /// Implementación del servicio de productos.
    /// Utiliza una lista en memoria para almacenar los productos.
    /// En un proyecto real, esta clase interactuaría con una base de datos.
    /// </summary>
    public class ProductoService : IProductoService
    {
        // Lista privada que actúa como "base de datos" en memoria
        private readonly List<Producto> _productos;

        // Contador para generar IDs únicos
        private int _nextId = 1;

        public ProductoService()
        {
            _productos = new List<Producto>();
            // Se inicializa con algunos productos de prueba
            InicializarProductosPrueba();
        }

        /// <summary>
        /// Inicializa la lista con productos de prueba para demostración.
        /// </summary>
        private void InicializarProductosPrueba()
        {
            try
            {
                // Se crean algunos productos de ejemplo
                var producto1 = new Producto("Laptop Dell", 899.99m, "Electrónica", 5);
                producto1.Id = _nextId++;
                _productos.Add(producto1);

                var producto2 = new Producto("Mouse Logitech", 29.99m, "Accesorios", 15);
                producto2.Id = _nextId++;
                _productos.Add(producto2);

                var producto3 = new Producto("Teclado Mecánico", 149.99m, "Accesorios", 8);
                producto3.Id = _nextId++;
                _productos.Add(producto3);

                var producto4 = new Producto("Monitor LG 27\"", 299.99m, "Electrónica", 3);
                producto4.Id = _nextId++;
                _productos.Add(producto4);
            }
            catch (Exception ex)
            {
                // En caso de error, podría registrarse en un log
                Console.WriteLine($"Error al inicializar productos de prueba: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los productos sin modificarlos.
        /// </summary>
        public Task<List<Producto>> ObtenerTodosAsync()
        {
            // Se retorna una copia de la lista para evitar modificaciones externas
            return Task.FromResult(new List<Producto>(_productos));
        }

        /// <summary>
        /// Busca un producto por su ID.
        /// </summary>
        public Task<Producto?> ObtenerPorIdAsync(int id)
        {
            // Se busca el producto en la lista
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(producto);
        }

        /// <summary>
        /// Crea un nuevo producto y lo agrega a la lista.
        /// </summary>
        public Task<Producto> CrearAsync(string nombre, decimal precio, string categoria, int stock)
        {
            try
            {
                // Se valida que el nombre no sea duplicado (ejemplo de lógica de negocio adicional)
                if (_productos.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Ya existe un producto con el nombre '{nombre}'", nameof(nombre));
                }

                // Se crea el nuevo producto
                var nuevoProducto = new Producto(nombre, precio, categoria, stock);
                
                // Se asigna un ID único
                nuevoProducto.Id = _nextId++;
                
                // Se agrega a la lista
                _productos.Add(nuevoProducto);

                return Task.FromResult(nuevoProducto);
            }
            catch (ArgumentException)
            {
                // Se relanza la excepción de validación
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear el producto", ex);
            }
        }

        /// <summary>
        /// Actualiza los datos de un producto existente.
        /// </summary>
        public Task<Producto?> ActualizarAsync(int id, string nombre, decimal precio, string categoria)
        {
            // Se busca el producto
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            
            if (producto == null)
                return Task.FromResult<Producto?>(null);

            try
            {
                // Se valida que el nuevo nombre no sea duplicado (si cambió)
                if (!producto.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    if (_productos.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new ArgumentException($"Ya existe un producto con el nombre '{nombre}'", nameof(nombre));
                    }
                }

                // Se actualizan los datos
                producto.Nombre = nombre;
                producto.Precio = precio;
                producto.Categoria = categoria;

                return Task.FromResult<Producto?>(producto);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el producto", ex);
            }
        }

        /// <summary>
        /// Elimina un producto de la lista.
        /// </summary>
        public Task<bool> EliminarAsync(int id)
        {
            // Se busca el producto
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            
            if (producto == null)
                return Task.FromResult(false);

            // Se elimina
            _productos.Remove(producto);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Actualiza el stock de un producto.
        /// </summary>
        public Task<Producto?> ActualizarStockAsync(int id, int cantidad)
        {
            // Se busca el producto
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            
            if (producto == null)
                return Task.FromResult<Producto?>(null);

            try
            {
                // Se actualiza el stock (esto puede lanzar excepciones si la validación falla)
                producto.ActualizarStock(cantidad);
                return Task.FromResult<Producto?>(producto);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el stock", ex);
            }
        }
    }
}
