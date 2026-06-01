public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public DateTime FechaRegistro { get; }
    
    // Constructor 1: Por defecto
    public Persona()
    {
        Nombre = "Sin nombre";
        Edad = 0;
        FechaRegistro = DateTime.Now;
        Console.WriteLine("👶 Persona creada (por defecto)");
    }
    
    // Constructor 2: Con parámetros
    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
        FechaRegistro = DateTime.Now;
        Console.WriteLine($"👋 Bienvenido, {nombre}!");
    }
    
    // Constructor 3: Copia
    public Persona(Persona original)
    {
        Nombre = original.Nombre;
        Edad = original.Edad;
        FechaRegistro = original.FechaRegistro;
    }
}

// CREACIÓN
Persona p1 = new Persona();                    // Constructor 1
Persona p2 = new Persona("Ana", 25);           // Constructor 2
Persona p3 = new Persona(p2);                   // Constructor 3