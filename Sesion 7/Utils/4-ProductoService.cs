// 📁 Services/ProductoService.cs (IMPLEMENTACIÓN)
namespace MiAPI.Services
{
    public class ProductoService : IProductoService
    {
        // Simulación de base de datos en memoria
        private List<Producto> _productos = new List<Producto>();
        private int _nextId = 1;
        
        public List<Producto> ObtenerTodos()
        {
            return _productos.ToList();  // Copia para seguridad
        }
        
        public Producto ObtenerPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }
        
        public Producto Crear(Producto producto)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre es requerido");
                
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser positivo");
            
            producto.Id = _nextId++;
            _productos.Add(producto);
            return producto;
        }
        
        public bool Actualizar(int id, Producto productoActualizado)
        {
            var producto = ObtenerPorId(id);
            if (producto == null) return false;
            
            producto.Nombre = productoActualizado.Nombre;
            producto.Precio = productoActualizado.Precio;
            producto.Categoria = productoActualizado.Categoria;
            // Stock se maneja por separado
            
            return true;
        }
        
        public bool Eliminar(int id)
        {
            var producto = ObtenerPorId(id);
            if (producto == null) return false;
            
            return _productos.Remove(producto);
        }
    }
}