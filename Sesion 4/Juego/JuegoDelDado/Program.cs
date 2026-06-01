using System;
using System.Collections.Generic;
using System.Linq;

// Clase para representar un jugador
class Jugador
{
    public string Nombre { get; set; }
    public decimal Saldo { get; set; }

    public Jugador(string nombre, decimal saldoInicial = 1000)
    {
        Nombre = nombre;
        Saldo = saldoInicial;
    }

    public override string ToString()
    {
        return $"{Nombre}: ${Saldo}";
    }
}

// Clase para manejar el juego de dados
class JuegoDados
{
    private List<Jugador> jugadores;
    private Random random;
    private const int NUM_PARTIDAS = 3;
    private const int SALDO_INICIAL = 1000;

    public JuegoDados()
    {
        jugadores = new List<Jugador>();
        random = new Random();
    }

    public void MostrarMenu()
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n========== JUEGO DE DADOS ==========");
            Console.WriteLine("1. Iniciar Nuevo Juego");
            Console.WriteLine("2. Ver Instrucciones");
            Console.WriteLine("3. Salir");
            Console.WriteLine("===================================");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    InicializarJugadores();
                    if (jugadores.Count > 0)
                    {
                        JugarPartidas();
                    }
                    break;
                case "2":
                    MostrarInstrucciones();
                    break;
                case "3":
                    salir = true;
                    Console.WriteLine("\n¡Gracias por jugar!");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private void MostrarInstrucciones()
    {
        Console.WriteLine("\n========== INSTRUCCIONES ==========");
        Console.WriteLine("1. Se crean 2 o más jugadores con sus nombres");
        Console.WriteLine("2. Cada jugador comienza con $1000");
        Console.WriteLine("3. Hay 3 partidas en total");
        Console.WriteLine("4. En cada partida:");
        Console.WriteLine("   - Todos apuestan una cantidad (1-500)");
        Console.WriteLine("   - Todos predicen un número (1-6)");
        Console.WriteLine("   - Se lanza un dado");
        Console.WriteLine("   - Quien adivinó gana todo lo apostado");
        Console.WriteLine("5. Gana quien termine con más saldo");
        Console.WriteLine("===================================\n");
    }

    private void InicializarJugadores()
    {
        jugadores.Clear();
        Console.Write("\n¿Cuántos jugadores desea? (mínimo 2): ");

        if (int.TryParse(Console.ReadLine(), out int numJugadores))
        {
            if (numJugadores < 2)
            {
                Console.WriteLine("Se necesitan mínimo 2 jugadores.");
                return;
            }

            for (int i = 0; i < numJugadores; i++)
            {
                Console.Write($"Nombre del jugador {i + 1}: ");
                string nombre = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    nombre = $"Jugador {i + 1}";
                }

                jugadores.Add(new Jugador(nombre, SALDO_INICIAL));
            }

            Console.WriteLine("\n¡Jugadores inicializados correctamente!");
            MostrarSaldos();
        }
        else
        {
            Console.WriteLine("Entrada inválida.");
        }
    }

    private void JugarPartidas()
    {
        for (int partida = 1; partida <= NUM_PARTIDAS; partida++)
        {
            Console.WriteLine($"\n{'='} PARTIDA {partida}/{NUM_PARTIDAS} {'='}");
            JugarPartida(partida);
        }

        MostrarGanador();
    }

    private void JugarPartida(int numeroPartida)
    {
        List<(Jugador jugador, decimal apuesta, int prediccion)> apuestas = 
            new List<(Jugador, decimal, int)>();

        decimal totalApostado = 0;

        // Obtener apuestas y predicciones de todos los jugadores
        foreach (var jugador in jugadores)
        {
            Console.WriteLine($"\n--- Turno de {jugador.Nombre} (Saldo: ${jugador.Saldo}) ---");

            // Obtener apuesta
            decimal apuesta = ObtenerApuesta(jugador);
            if (apuesta > 0)
            {
                jugador.Saldo -= apuesta;
                totalApostado += apuesta;

                // Obtener predicción
                int prediccion = ObtenerPrediccion();

                apuestas.Add((jugador, apuesta, prediccion));
            }
        }

        // Lanzar dado
        int numeroGanador = random.Next(1, 7);
        Console.WriteLine($"\n¡El dado muestra: {numeroGanador}!");

        // Determinar ganadores
        var ganadores = apuestas.Where(a => a.prediccion == numeroGanador).ToList();

        if (ganadores.Count > 0)
        {
            decimal premioIndividual = totalApostado / ganadores.Count;
            Console.WriteLine($"\n¡Tenemos {ganadores.Count} ganador(es)!");

            foreach (var ganador in ganadores)
            {
                ganador.jugador.Saldo += premioIndividual;
                Console.WriteLine($"{ganador.jugador.Nombre} ¡GANÓ! Recibe ${premioIndividual}");
            }
        }
        else
        {
            Console.WriteLine("\n¡Nadie acertó! El bote se acumula para la siguiente partida.");
            // Los saldos reducidos se mantienen, el dinero se pierda
        }

        MostrarSaldos();
    }

    private decimal ObtenerApuesta(Jugador jugador)
    {
        while (true)
        {
            Console.Write($"¿Cuánto desea apostar? (1-500, saldo disponible: ${jugador.Saldo}): ");

            if (decimal.TryParse(Console.ReadLine(), out decimal apuesta))
            {
                if (apuesta > 0 && apuesta <= 500 && apuesta <= jugador.Saldo)
                {
                    return apuesta;
                }
                else
                {
                    Console.WriteLine("Apuesta inválida. Intente de nuevo.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
            }
        }
    }

    private int ObtenerPrediccion()
    {
        while (true)
        {
            Console.Write("¿Qué número predice que saldrá? (1-6): ");

            if (int.TryParse(Console.ReadLine(), out int prediccion))
            {
                if (prediccion >= 1 && prediccion <= 6)
                {
                    return prediccion;
                }
                else
                {
                    Console.WriteLine("Número inválido. Debe ser entre 1 y 6.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
            }
        }
    }

    private void MostrarSaldos()
    {
        Console.WriteLine("\n--- Saldos Actuales ---");
        foreach (var jugador in jugadores)
        {
            Console.WriteLine(jugador.ToString());
        }
    }

    private void MostrarGanador()
    {
        Console.WriteLine("\n\n{'='} FIN DEL JUEGO {'='}");
        Console.WriteLine("\n--- RESULTADOS FINALES ---");

        var jugadoresOrdenados = jugadores.OrderByDescending(j => j.Saldo).ToList();

        for (int i = 0; i < jugadoresOrdenados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {jugadoresOrdenados[i].Nombre}: ${jugadoresOrdenados[i].Saldo}");
        }

        var ganador = jugadoresOrdenados.First();
        Console.WriteLine($"\n🎉 ¡GANADOR: {ganador.Nombre} con ${ganador.Saldo}! 🎉");
    }
}

// Programa principal
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        JuegoDados juego = new JuegoDados();
        juego.MostrarMenu();
    }
}
