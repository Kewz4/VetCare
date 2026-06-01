using Utils.CuentaBancaria;

namespace EjemploCuentaBancaria
{   
class Program
{
    static void Main()
    {
        // Crear objetos
        CuentaBancaria cuentaAna = new CuentaBancaria("Ana García", "123-456-789");
        CuentaBancaria cuentaCarlos = new CuentaBancaria("Carlos Ruiz", "987-654-321");
        
        // Operaciones
        cuentaAna.Depositar(1000);
        cuentaAna.Depositar(500);
        cuentaAna.Retirar(200);
        
        cuentaCarlos.Depositar(2500);
        cuentaCarlos.Retirar(3000);  // Intento fallido
        
        // Mostrar información
        cuentaAna.MostrarInfo();
        cuentaCarlos.MostrarInfo();
        
        // Demostrar independencia
        Console.WriteLine($"\n🔍 Saldo Ana: ${cuentaAna.Saldo}");
        Console.WriteLine($"🔍 Saldo Carlos: ${cuentaCarlos.Saldo}");
    }
}
}
