// Imagina tener que mantener ESTO:
public class Perro
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Color { get; set; }
    
    public void Comer() { Console.WriteLine($"{Nombre} está comiendo"); }
    public void Dormir() { Console.WriteLine($"{Nombre} está durmiendo"); }
    public void Ladrar() { Console.WriteLine($"{Nombre} dice: ¡Guau!"); }
}

public class Gato
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Color { get; set; }
    
    public void Comer() { Console.WriteLine($"{Nombre} está comiendo"); }
    public void Dormir() { Console.WriteLine($"{Nombre} está durmiendo"); }
    public void Maullar() { Console.WriteLine($"{Nombre} dice: ¡Miau!"); }
}

public class Vaca
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Color { get; set; }
    
    public void Comer() { Console.WriteLine($"{Nombre} está comiendo"); }
    public void Dormir() { Console.WriteLine($"{Nombre} está durmiendo"); }
    public void Mugir() { Console.WriteLine($"{Nombre} dice: ¡Muuu!"); }
}

// ... 20 animales más, TODO repetido 😫