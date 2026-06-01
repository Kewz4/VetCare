public class Animal  // CLASE BASE (general)
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Color { get; set; }
    
    public void Comer() { Console.WriteLine($"{Nombre} está comiendo"); }
    public void Dormir() { Console.WriteLine($"{Nombre} está durmiendo"); }
    public virtual void HacerSonido() { }  // Polimorfismo
}

public class Perro : Animal  // HERENCIA
{
    public override void HacerSonido() { Console.WriteLine($"{Nombre} dice: ¡Guau!"); }
}
// ¡20 líneas vs 200 líneas! 🎉