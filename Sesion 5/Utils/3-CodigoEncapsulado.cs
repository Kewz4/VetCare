public class CuentaBancariaSegura
{
    // Campos PRIVADOS - nadie fuera puede acceder directamente
    private decimal _saldo;
    private string _contraseña;
    private List<string> _historialTransacciones;
    
    // PROPIEDADES PÚBLICAS - acceso controlado
    public string Titular { get; set; }
    
    // Solo lectura - no se puede modificar externamente
    public decimal Saldo 
    { 
        get { return _saldo; }
        private set { _saldo = value; }  // Solo modificable dentro de la clase
    }
    
    // Propiedad calculada (solo get)
    public decimal SaldoEnDolares => _saldo / 20;  // Suponiendo tipo de cambio
    
    // MÉTODOS PÚBLICOS - interfaz controlada
    public void Depositar(decimal monto)
    {
        if (monto > 0)
        {
            _saldo += monto;
            _historialTransacciones.Add($"Depósito: +{monto:C}");
            this.RegistrarTransaccion("Depósito", monto);  // Llamada al método privado
            Console.WriteLine($"💰 Depósito exitoso. Nuevo saldo: {_saldo:C}");
        }
    }
    
    public bool Retirar(decimal monto, string contraseña)
    {
        if (contraseña != _contraseña)
        {
            this.RegistrarTransaccion("Intento de retiro fallido - contraseña incorrecta", monto);  // Llamada al método privado
            Console.WriteLine("❌ Contraseña incorrecta");
            return false;
        }
        
        if (monto <= _saldo && monto > 0)
        {
            _saldo -= monto;
            _historialTransacciones.Add($"Retiro: -{monto:C}");
            this.RegistrarTransaccion("Retiro", monto);  // Llamada al método privado
            Console.WriteLine($"💸 Retiro exitoso. Nuevo saldo: {_saldo:C}");
            return true;
        }
        this.RegistrarTransaccion("Intento de retiro fallido", monto);  // Llamada al método privado
        Console.WriteLine("❌ Fondos insuficientes");
        return false;
    }
    
    // Método privado - solo uso interno
    private void RegistrarTransaccion(string tipo, decimal monto)
    {
        // Lógica de registro
        Console.WriteLine($"Registro de transacción: {tipo}, Monto: {monto:C}");
    }
}