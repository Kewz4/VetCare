using System;
using System.Collections.Generic;

// Definición de la clase Estudiante
class Estudiante
{
    // Propiedades
    public string Nombre { get; set; }
    public int Matricula { get; set; }
    public List<double> Calificaciones { get; private set; }

    // Constructor
    public Estudiante(string nombre, int matricula)
    {
        Nombre = nombre;
        Matricula = matricula;
        Calificaciones = new List<double>();
    }

    // Método para agregar una calificación
    public void AgregarCalificacion(double calificacion)
    {
        if (calificacion >= 0 && calificacion <= 100)
        {
            Calificaciones.Add(calificacion);
            Console.WriteLine($"✓ Calificación {calificacion} agregada a {Nombre}");
        }
        else
        {
            Console.WriteLine("✗ Error: La calificación debe estar entre 0 y 100");
        }
    }

    // Método para calcular el promedio
    public double ObtenerPromedio()
    {
        if (Calificaciones.Count == 0)
            return 0;

        double suma = 0;
        foreach (double calificacion in Calificaciones)
        {
            suma += calificacion;
        }
        return suma / Calificaciones.Count;
    }

    // Método para mostrar información del estudiante
    public void MostrarInformacion()
    {
        Console.WriteLine("\n" + new string('-', 50));
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Matrícula: {Matricula}");
        Console.WriteLine($"Calificaciones: {string.Join(", ", Calificaciones)}");
        Console.WriteLine($"Promedio: {ObtenerPromedio():F2}");
        Console.WriteLine(new string('-', 50));
    }
}

class Program
{
    static void Main()
    {
        // Programa principal
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   SISTEMA DE GESTIÓN DE ESTUDIANTES   ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Crear objetos estudiantes
        Estudiante est1 = new Estudiante("Juan Pérez", 2024001);
        Estudiante est2 = new Estudiante("María García", 2024002);
        Estudiante est3 = new Estudiante("Carlos López", 2024003);

        // Agregar calificaciones a los estudiantes
        Console.WriteLine("📝 Agregando calificaciones...\n");

        est1.AgregarCalificacion(85);
        est1.AgregarCalificacion(90);
        est1.AgregarCalificacion(88);

        est2.AgregarCalificacion(92);
        est2.AgregarCalificacion(95);
        est2.AgregarCalificacion(89);

        est3.AgregarCalificacion(78);
        est3.AgregarCalificacion(82);
        est3.AgregarCalificacion(79);

        // Mostrar información de cada estudiante
        Console.WriteLine("\n📊 INFORMACIÓN DE ESTUDIANTES:\n");
        est1.MostrarInformacion();
        est2.MostrarInformacion();
        est3.MostrarInformacion();

        // Interacción interactiva
        Console.WriteLine("\n🎓 MODO INTERACTIVO\n");
        Console.WriteLine("¿Deseas agregar más calificaciones? (s/n): ");
        string respuesta = Console.ReadLine();

        if (respuesta?.ToLower() == "s")
        {
            Console.WriteLine("\nSelecciona un estudiante:");
            Console.WriteLine("1. Juan Pérez");
            Console.WriteLine("2. María García");
            Console.WriteLine("3. Carlos López");
            Console.Write("Opción: ");
            
            string opcion = Console.ReadLine();
            
            Console.Write("Ingresa la calificación: ");
            if (double.TryParse(Console.ReadLine(), out double nota))
            {
                switch (opcion)
                {
                    case "1":
                        est1.AgregarCalificacion(nota);
                        est1.MostrarInformacion();
                        break;
                    case "2":
                        est2.AgregarCalificacion(nota);
                        est2.MostrarInformacion();
                        break;
                    case "3":
                        est3.AgregarCalificacion(nota);
                        est3.MostrarInformacion();
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Calificación inválida");
            }
        }

        Console.WriteLine("\n✓ Programa finalizado");
    }
}

