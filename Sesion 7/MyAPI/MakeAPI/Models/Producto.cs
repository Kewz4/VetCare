namespace MakeAPI.Models
{
    /// <summary>
    /// Clase que representa un producto en el sistema.
    /// Implementa las propiedades y métodos necesarios para gestionar un producto.
    /// </summary>
    public class Producto
    {
        // Campo privado para almacenar el stock
        private int _stock;

        /// <summary>
        /// Constructor para crear un nuevo producto.
        /// </summary>
        /// <param name="nombre">Nombre del producto</param>
        /// <param name="precio">Precio unitario del producto</param>
        /// <param name="categoria">Categoría a la que pertenece el producto</param>
        /// <param name="stock">Cantidad inicial en stock</param>
        public Producto(string nombre, decimal precio, string categoria, int stock)
        {
            // Se validan los parámetros antes de asignarlos
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío", nameof(nombre));

            if (precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0", nameof(precio));

            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo", nameof(stock));

            Nombre = nombre;
            Precio = precio;
            Categoria = categoria ?? string.Empty;
            _stock = stock;
        }

        /// <summary>
        /// Identificador único del producto (solo lectura después de la creación).
        /// Se asigna automáticamente por la base de datos.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto (requerido).
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Precio unitario del producto (debe ser mayor a 0).
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Categoría a la que pertenece el producto.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Cantidad de unidades disponibles en stock.
        /// Es una propiedad privada que solo se puede modificar mediante el método ActualizarStock().
        /// </summary>
        public int Stock
        {
            get { return _stock; }
            private set { _stock = value; }
        }

        /// <summary>
        /// Propiedad calculada que indica si el producto está disponible.
        /// Retorna true si hay stock disponible (Stock > 0).
        /// </summary>
        public bool Disponible
        {
            get { return _stock > 0; }
        }

        /// <summary>
        /// Actualiza el stock del producto con validaciones.
        /// </summary>
        /// <param name="cantidad">
        /// Cantidad a añadir o restar del stock.
        /// - Valores positivos: aumentan el stock
        /// - Valores negativos: disminuyen el stock
        /// </param>
        /// <exception cref="ArgumentException">
        /// Se lanza si la cantidad es 0 o si el stock resultante sería negativo.
        /// </exception>
        public void ActualizarStock(int cantidad)
        {
            // Validación: No se permite cantidad 0
            if (cantidad == 0)
                throw new ArgumentException("La cantidad debe ser diferente de 0", nameof(cantidad));

            // Validación: El stock no puede quedar negativo
            int nuevoStock = _stock + cantidad;
            if (nuevoStock < 0)
                throw new ArgumentException(
                    $"No hay suficiente stock. Stock actual: {_stock}, intento de cambio: {cantidad}",
                    nameof(cantidad));

            // Si todas las validaciones pasan, se actualiza el stock
            Stock = nuevoStock;
        }

        /// <summary>
        /// Representación textual del producto.
        /// </summary>
        public override string ToString()
        {
            return $"[{Id}] {Nombre} - ${Precio:F2} ({Categoria}) - Stock: {Stock} - Disponible: {Disponible}";
        }
    }
}
