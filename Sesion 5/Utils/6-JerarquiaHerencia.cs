// NIVEL 1: Lo más general
public class Persona
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public DateTime FechaNacimiento { get; set; }
    
    public int CalcularEdad() => DateTime.Now.Year - FechaNacimiento.Year;
}

// NIVEL 2: Especialización por rol
public class Estudiante : Persona
{
    public string Matricula { get; set; }
    public List<string> Cursos { get; set; }
    public double Promedio { get; set; }
    
    public bool EstaAprobado() => Promedio >= 6.0;
}

public class Profesor : Persona
{
    public string Departamento { get; set; }
    public DateTime FechaContratacion { get; set; }
    public decimal Salario { get; set; }
}

// NIVEL 3: Sub-especialización
public class EstudianteBecado : Estudiante
{
    public decimal MontoBeca { get; set; }
    public string TipoBeca { get; set; }
    public DateTime FechaRenovacion { get; set; }
    
    public bool RenovarBeca(double promedioRequerido)
    {
        return Promedio >= promedioRequerido;
    }
}