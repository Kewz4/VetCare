// 📁 Models/Producto.cs
namespace MiAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        
        // Propiedad privada con acceso controlado
        private int _stock;
        public int Stock 
        { 
            get => _stock;
            private set => _stock = value; 
        }
        
        // Propiedad calculada
        public bool Disponible => Stock > 0;
        
        // Método de negocio
        public void ActualizarStock(int cantidad)
        {
            if (Stock + cantidad < 0)
                throw new InvalidOperationException("Stock insuficiente");
            _stock += cantidad;
        }
    }
}