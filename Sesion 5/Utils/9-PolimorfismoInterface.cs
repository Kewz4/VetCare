// INTERFAZ = Contrato que define QUÉ hacer, no CÓMO
public interface IEncendible
{
    void Encender();
    void Apagar();
    bool EstaEncendido { get; }
}

// DIFERENTES clases implementan la MISMA interfaz
public class Computadora : IEncendible
{
    public bool EstaEncendido { get; private set; }
    
    public void Encender()
    {
        EstaEncendido = true;
        Console.WriteLine("💻 Computadora: Iniciando Windows... beep beep");
    }
    
    public void Apagar()
    {
        EstaEncendido = false;
        Console.WriteLine("💻 Computadora: Cerrando sesión... apagando");
    }
}

public class Lampara : IEncendible
{
    public bool EstaEncendido { get; private set; }
    
    public void Encender()
    {
        EstaEncendido = true;
        Console.WriteLine("💡 Lámpara: Click! Luz prendida");
    }
    
    public void Apagar()
    {
        EstaEncendido = false;
        Console.WriteLine("💡 Lámpara: Click! Luz apagada");
    }
}

public class Auto : IEncendible
{
    public bool EstaEncendido { get; private set; }
    
    public void Encender()
    {
        EstaEncendido = true;
        Console.WriteLine("🚗 Auto: Rrrr rrrr... ¡Motor encendido!");
    }
    
    public void Apagar()
    {
        EstaEncendido = false;
        Console.WriteLine("🚗 Auto: Motor apagado");
    }
}

// POLIMORFISMO CON INTERFACES
class CasaInteligente
{
    private List<IEncendible> _dispositivos = new List<IEncendible>();
    
    public void AgregarDispositivo(IEncendible dispositivo)
    {
        _dispositivos.Add(dispositivo);
    }
    
    public void EncenderTodo()
    {
        Console.WriteLine("\n🔌 ENCENDIENDO TODOS LOS DISPOSITIVOS:");
        foreach (var dispositivo in _dispositivos)
        {
            dispositivo.Encender();  // Polimorfismo
        }
    }
    
    public void ApagarTodo()
    {
        Console.WriteLine("\n🔌 APAGANDO TODOS LOS DISPOSITIVOS:");
        foreach (var dispositivo in _dispositivos)
        {
            dispositivo.Apagar();  // Polimorfismo
        }
    }
}