public class Familia
{
    public string TodosSabemos;      // Todo el mundo
    internal string MismoBarrio;     // Misma comunidad
    protected string SoloFamilia;    // Solo parientes
    private string MiSecreto;        // Solo yo
    
    public void Metodo()
    {
        // ✅ Puede acceder a TODOS (está dentro)
    }
}

public class Hijo : Familia
{
    public void MetodoHijo()
    {
        // ✅ Puede acceder a: public, internal, protected
        // ❌ NO puede acceder a: private
    }
}

public class Vecino
{
    Familia f = new Familia();
    // ✅ Puede acceder a: public, internal
    // ❌ NO puede acceder a: protected, private
}