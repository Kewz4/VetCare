public class Instrumento
{
    public string Nombre { get; set; }
    
    public virtual void Tocar()
    {
        Console.WriteLine($"{Nombre} está produciendo un sonido");
    }
}

public class Guitarra : Instrumento
{
    public override void Tocar()
    {
        Console.WriteLine($"🎸 {Nombre}: ¡Rasgueo de cuerdas!");
    }
}

public class Piano : Instrumento
{
    public override void Tocar()
    {
        Console.WriteLine($"🎹 {Nombre}: ¡Notas melodiosas!");
    }
}

public class Bateria : Instrumento
{
    public override void Tocar()
    {
        Console.WriteLine($"🥁 {Nombre}: ¡Bum tss tss bum!");
    }
}

class Program
{
    static void Main()
    {
        // CREAR INSTRUMENTOS
        List<Instrumento> orquesta = new List<Instrumento>
        {
            new Guitarra { Nombre = "Fender Stratocaster" },
            new Piano { Nombre = "Yamaha Grand" },
            new Bateria { Nombre = "Pearl Export" },
            new Guitarra { Nombre = "Gibson Les Paul" }
        };
        
        Console.WriteLine("🎵 ¡La orquesta comienza a tocar!\n");
        
        // POLIMORFISMO EN ACCIÓN
        foreach (Instrumento instrumento in orquesta)
        {
            instrumento.Tocar();  // ¡Mismo método, diferentes resultados!
        }
    }
}

// SALIDA:
// 🎸 Fender Stratocaster: ¡Rasgueo de cuerdas!
// 🎹 Yamaha Grand: ¡Notas melodiosas!
// 🥁 Pearl Export: ¡Bum tss tss bum!
// 🎸 Gibson Les Paul: ¡Rasgueo de cuerdas!