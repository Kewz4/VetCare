using System;
using System.Collections.Generic;

// CLASE: Es el molde, el plano para crear objetos
// OBJETO: Es una instancia específica de la clase con valores reales

class Persona
{
    // Propiedades de la clase Persona
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Ciudad { get; set; }
    public string Profesion { get; set; }

    // Constructor
    public Persona(string nombre, int edad, string ciudad, string profesion)
    {
        Nombre = nombre;
        Edad = edad;
        Ciudad = ciudad;
        Profesion = profesion;
    }

    // Método para que la persona se salude
    public void Saludar()
    {
        Console.WriteLine($"¡Hola! Soy {Nombre}");
    }

    // Método para mostrar toda la información
    public void MostrarInfo()
    {
        Console.WriteLine("\n--- Información de la Persona ---");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Edad: {Edad} años");
        Console.WriteLine($"Ciudad: {Ciudad}");
        Console.WriteLine($"Profesión: {Profesion}");
        Console.WriteLine("--------------------------------\n");
    }

    // Método para cambiar edad
    public void CumplirAños()
    {
        Edad++;
        Console.WriteLine($"{Nombre} ha cumplido años y ahora tiene {Edad} años.\n");
    }

    // Método para cambiar profesión
    public void CambiarProfesion(string nuevaProfesion)
    {
        Profesion = nuevaProfesion;
        Console.WriteLine($"{Nombre} ahora trabaja como {Profesion}.\n");
    }
}

class Program
{
    static void Main()
    {
        // OBJETOS: Instancias específicas de la clase Persona
        List<Persona> personas = new List<Persona>();
        
        bool continuar = true;

        Console.WriteLine("=== APLICACIÓN DE GESTIÓN DE PERSONAS ===\n");
        Console.WriteLine("Aprende la diferencia entre CLASE y OBJETO\n");

        while (continuar)
        {
            MostrarMenu();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearPersona(personas);
                    break;
                case "2":
                    MostrarPersonas(personas);
                    break;
                case "3":
                    InteractuarConPersona(personas);
                    break;
                case "4":
                    continuar = false;
                    Console.WriteLine("\n¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.\n");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("--- MENÚ PRINCIPAL ---");
        Console.WriteLine("1. Crear una nueva persona (nuevo OBJETO)");
        Console.WriteLine("2. Mostrar todas las personas");
        Console.WriteLine("3. Interactuar con una persona");
        Console.WriteLine("4. Salir");
        Console.Write("Seleccione una opción: ");
    }

    static void CrearPersona(List<Persona> personas)
    {
        Console.Write("\nIngrese el nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese la edad: ");
        int.TryParse(Console.ReadLine(), out int edad);

        Console.Write("Ingrese la ciudad: ");
        string ciudad = Console.ReadLine();

        Console.Write("Ingrese la profesión: ");
        string profesion = Console.ReadLine();

        // Crear una nueva instancia (OBJETO) de la clase Persona
        Persona nuevaPersona = new Persona(nombre, edad, ciudad, profesion);
        personas.Add(nuevaPersona);

        Console.WriteLine($"\n✓ Persona '{nombre}' creada exitosamente.\n");
    }

    static void MostrarPersonas(List<Persona> personas)
    {
        if (personas.Count == 0)
        {
            Console.WriteLine("\nNo hay personas registradas aún.\n");
            return;
        }

        Console.WriteLine($"\nTotal de OBJETOS (personas) creados: {personas.Count}\n");
        
        for (int i = 0; i < personas.Count; i++)
        {
            Console.WriteLine($"--- Persona #{i + 1} ---");
            personas[i].MostrarInfo();
        }
    }

    static void InteractuarConPersona(List<Persona> personas)
    {
        if (personas.Count == 0)
        {
            Console.WriteLine("\nNo hay personas para interactuar.\n");
            return;
        }

        Console.WriteLine("\nSeleccione una persona:");
        for (int i = 0; i < personas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {personas[i].Nombre}");
        }

        Console.Write("Opción: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= personas.Count)
        {
            Persona personaSeleccionada = personas[opcion - 1];

            bool interactuar = true;
            while (interactuar)
            {
                Console.WriteLine($"\n--- Interactuando con {personaSeleccionada.Nombre} ---");
                Console.WriteLine("1. Saludar");
                Console.WriteLine("2. Ver información");
                Console.WriteLine("3. Cumplir años");
                Console.WriteLine("4. Cambiar profesión");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Opción: ");

                string opcionAccion = Console.ReadLine();

                switch (opcionAccion)
                {
                    case "1":
                        personaSeleccionada.Saludar();
                        break;
                    case "2":
                        personaSeleccionada.MostrarInfo();
                        break;
                    case "3":
                        personaSeleccionada.CumplirAños();
                        break;
                    case "4":
                        Console.Write("Ingrese la nueva profesión: ");
                        string nuevaProfesion = Console.ReadLine();
                        personaSeleccionada.CambiarProfesion(nuevaProfesion);
                        break;
                    case "5":
                        interactuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.\n");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Opción inválida.\n");
        }
    }
}
