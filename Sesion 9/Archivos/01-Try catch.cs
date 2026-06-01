try
{
    Console.Write("Edad: ");
    int edad = int.Parse(Console.ReadLine());
    
    if (edad < 0 || edad > 120)
        throw new ArgumentException("Edad fuera de rango");
        
    Console.WriteLine($"Edad válida: {edad}");
}
catch (FormatException ex)
{
    Console.WriteLine($"Error: Debe ingresar un número - {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error de validación: {ex.Message}");
}
catch (Exception ex)  // Catch genérico (SIEMPRE al final)
{
    Console.WriteLine($"Error inesperado: {ex.Message}");
}