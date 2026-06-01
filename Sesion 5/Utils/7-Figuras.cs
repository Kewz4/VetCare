public class Figura
{
    public string Color { get; set; }
    
    // Método virtual - puede ser sobrescrito
    public virtual double CalcularArea()
    {
        Console.WriteLine("⚠️ Usando método genérico de Figura");
        return 0;
    }
    
    // Método virtual con implementación por defecto
    public virtual void Dibujar()
    {
        Console.WriteLine($"Dibujando una figura {Color}");
    }
}

public class Rectangulo : Figura
{
    public double Ancho { get; set; }
    public double Alto { get; set; }
    
    // Sobrescribe el método
    public override double CalcularArea()
    {
        double area = Ancho * Alto;
        Console.WriteLine($"📐 Rectángulo área: {area}");
        return area;
    }
    
    // Puede o no sobrescribir Dibujar
}

public class Circulo : Figura
{
    public double Radio { get; set; }
    
    public override double CalcularArea()
    {
        double area = Math.PI * Radio * Radio;
        Console.WriteLine($"⚪ Círculo área: {area:F2}");
        return area;
    }
    
    public override void Dibujar()
    {
        base.Dibujar();  // Llama a la versión del padre
        Console.WriteLine($"   Radio: {Radio}");
    }
}