// El modelo completo (NO enviar directamente)
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public decimal CostoCompra { get; set; }  // 👈 Información sensible
    public string Proveedor { get; set; }      // 👈 Información interna
    public int Stock { get; set; }
    // ... más propiedades internas
}