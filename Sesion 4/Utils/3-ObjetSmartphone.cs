public class Smartphone  // ← NOMBRE (siempre PascalCase)
{
    // 1. ATRIBUTOS - Características
    public string Marca { get; set; }      // Propiedad auto-implementada
    public string Modelo { get; set; }
    private double _bateria;                // Campo privado
    
    // 2. CONSTRUCTOR - Nace el objeto
    public Smartphone(string marca, string modelo)
    {
        Marca = marca;
        Modelo = modelo;
        _bateria = 100;  // Batería nueva al 100%
    }
    
    // 3. MÉTODOS - Comportamientos
    public void Llamar(string numero)
    {
        if (_bateria > 0)
        {
            Console.WriteLine($"📞 Llamando a {numero}...");
            _bateria -= 5;
        }
    }
    
    public double NivelBateria => _bateria;  // Propiedad de solo lectura
}