// CLASE BASE (Padre)
public class Animal
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    
    public Animal(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
        Console.WriteLine($"🦴 Animal creado: {nombre}");
    }
    
    public void Respirar()
    {
        Console.WriteLine($"{Nombre} está respirando");
    }
    
    public virtual void HacerSonido()  // virtual = puede ser sobrescrito
    {
        Console.WriteLine($"{Nombre} hace un sonido genérico");
    }
}

// CLASE DERIVADA (Hija) - Usa : para heredar
public class Perro : Animal  // Perro ES UN Animal
{
    public string Raza { get; set; }
    
    // Constructor llama al constructor base con base()
    public Perro(string nombre, int edad, string raza) : base(nombre, edad)
    {
        Raza = raza;
        Console.WriteLine($"🐕 Perro creado: {nombre} - Raza: {raza}");
    }
    
    // Método específico de Perro
    public void MoverCola()
    {
        Console.WriteLine($"{Nombre} está moviendo la cola 🐕‍🦺");
    }
    
    // Sobrescribir método virtual
    public override void HacerSonido()
    {
        Console.WriteLine($"{Nombre} dice: ¡Guau guau!");
    }
}