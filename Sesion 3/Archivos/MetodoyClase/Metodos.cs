// Demostración de métodos: Modificadores, tipos de retorno y parámetros
using System;

namespace MetodoyClase
{   

class SistemaCalificaciones
{
    // ====== DEFINICIÓN DE MÉTODOS ======

    // Método 1: public (accesible), double (retorna número), 3 parámetros int
    public static double CalcularPromedio(int nota1, int nota2, int nota3)
    {
        return (nota1 + nota2 + nota3) / 3.0;
    }

    // Método 2: private (privado), string (retorna texto), 1 parámetro double
    private static string ObtenerEstadoAcademico(double promedio)
    {
        if (promedio >= 90)
            return "🌟 Excelente";
        else if (promedio >= 80)
            return "✓ Bueno";
        else if (promedio >= 70)
            return "→ Aceptable";
        else
            return "⚠ Necesita mejorar";
    }

    // Método 3: public (accesible), void (no retorna valor), 3 parámetros
    public static void MostrarReporteEstudiante(string nombre, double promedio, string estado)
    {
        Console.WriteLine("--- REPORTE ACADÉMICO ---");
        Console.WriteLine($"Estudiante: {nombre}");
        Console.WriteLine($"Promedio: {promedio:F2}");
        Console.WriteLine($"Desempeño: {estado}");
    }

    // Método 4: private (privado), bool (retorna verdadero/falso), 1 parámetro double
    private static bool PuedeAdelantarMaterias(double promedio)
    {
        return promedio >= 85;
    }
}
}
