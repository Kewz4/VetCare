// 📁 Services/IProductoService.cs (CONTRATO)
namespace MiAPI.Services
{
    public interface IProductoService
    {
        List<Producto> ObtenerTodos();
        Producto ObtenerPorId(int id);
        Producto Crear(Producto producto);
        bool Actualizar(int id, Producto producto);
        bool Eliminar(int id);
    }
}