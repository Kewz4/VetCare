// ¡SOLUCIÓN! Una sola plantilla ( "CLASE" ) para TODOS

public class Cliente
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public decimal Saldo { get; set; }
    public bool Activo { get; set; }
    
    public void MostrarResumen()
    {
        Console.WriteLine($"{Nombre} - Saldo: ${Saldo}");
    }
}



// Crear 10,000 clientes con 4 líneas
List<Cliente> clientes = new List<Cliente>();
for (int i = 0; i < 10000; i++)
{
    clientes.Add(new Cliente { /* datos */ });
}