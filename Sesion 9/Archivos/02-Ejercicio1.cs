using System;
using System.Collections.Generic;

public class CajaRegistradora
{
    private static List<string> productos = new List<string>
    {
        "Manzana", "Pan", "Leche", "Huevos", "Café"
    };
    
    private static List<decimal> precios = new List<decimal>
    {
        0.50m, 1.20m, 2.50m, 3.00m, 4.50m
    };
    
    public static void Main()
    {
        List<string> carrito = new List<string>();
        List<int> cantidades = new List<int>();
        decimal total = 0;
        
        Console.WriteLine("=== CAJA REGISTRADORA ===");
        
        // Mostrar productos
        Console.WriteLine("\nProductos disponibles:");
        for (int i = 0; i < productos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productos[i]} - ${precios[i]:F2}");
        }
        
        bool agregando = true;
        while (agregando)
        {
            Console.Write("\nSeleccione número de producto (0 para terminar): ");
            string input = Console.ReadLine();
            
            // TODO: Agregar try-catch para convertir el input a número
            
            int seleccion = int.Parse(input);
            
            if (seleccion == 0)
            {
                agregando = false;
                break;
            }
            
            // TODO: Agregar try-catch para validar que el índice exista
            
            // TODO: Agregar try-catch para convertir cantidad a número
            
            Console.Write("Cantidad: ");
            string cantidadInput = Console.ReadLine();
            int cantidad = int.Parse(cantidadInput);
            
            // TODO: Agregar try-catch para validar cantidad positiva
            
            string producto = productos[seleccion - 1];
            decimal precio = precios[seleccion - 1];
            decimal subtotal = precio * cantidad;
            
            carrito.Add(producto);
            cantidades.Add(cantidad);
            total += subtotal;
            
            Console.WriteLine($"Agregado: {cantidad}x {producto} - Subtotal: ${subtotal:F2}");
        }
        
        // Mostrar ticket
        Console.WriteLine("\n=== TICKET DE COMPRA ===");
        
        // TODO: Agregar try-catch por si el carrito está vacío
        
        for (int i = 0; i < carrito.Count; i++)
        {
            Console.WriteLine($"{carrito[i]} x{cantidades[i]}");
        }
        
        Console.WriteLine($"\n💰 TOTAL A PAGAR: ${total:F2}");
        
        // Procesar pago
        Console.Write("\nIngrese monto con el que paga: ");
        string pagoInput = Console.ReadLine();
        
        // TODO: Agregar try-catch para convertir el pago
        
        // TODO: Agregar try-catch para validar que el pago sea suficiente
        
        decimal pago = decimal.Parse(pagoInput);
        decimal cambio = pago - total;
        
        Console.WriteLine($"Cambio: ${cambio:F2}");
        Console.WriteLine("¡Gracias por su compra!");
    }
}