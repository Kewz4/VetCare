// CLASE = RECETA
public class Galleta
{
    public string Sabor { get; set; }
    public string Forma { get; set; }
    public bool TieneChispas { get; set; }
}

// OBJETOS = COOKIES
Galleta cookie1 = new Galleta 
{ 
    Sabor = "Chocolate", 
    Forma = "Redonda", 
    TieneChispas = true 
};

Galleta cookie2 = new Galleta 
{ 
    Sabor = "Vainilla", 
    Forma = "Estrella", 
    TieneChispas = false 
};