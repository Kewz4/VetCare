            Console.WriteLine("\n=== CALCULADORA BÁSICA ===");
            Console.Write("Ingrese el primer número: ");
            string input1 = Console.ReadLine();
            double numero1 = Convert.ToDouble(input1);
            Console.Write("Ingrese el segundo número: ");
            string input2 = Console.ReadLine();
            double numero2 = Convert.ToDouble(input2);
            Console.WriteLine("Seleccione la operación (+, -, *, /): ");
            string operacion = Console.ReadLine();
            double resultado = 0;


////////////
            Console.WriteLine("Ejemplo 1: Imprimir números del 1 al 10");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Ejemplo 2: Sumar los números del 1 al 10 usando for");
            int suma = 0;
            for (int i = 1; i <= 10; i++)
            {
                suma += i;
            }
            Console.WriteLine($"Suma = {suma}");

            Console.WriteLine();
            Console.WriteLine("Ejemplo 3: Recorrer un arreglo de nombres");
            string[] nombres = { "Ana", "Luis", "María", "José" };
            for (int i = 0; i < nombres.Length; i++)
            {
                Console.WriteLine($"Índice {i}: {nombres[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("Ejemplo 4: Tabla de multiplicar (1 a 5)");
            for (int a = 1; a <= 5; a++)
            {
                for (int b = 1; b <= 10; b++)
                {
                    Console.Write($"{a}x{b}={a * b}\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Presiona Enter para salir...");
            Console.ReadLine();