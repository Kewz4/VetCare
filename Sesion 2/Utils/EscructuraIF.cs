// Ejemplo real en backend
if (usuario.Autenticado)
{
    if (usuario.TienePermiso("admin"))
    {
        // Procesar operación administrativa
    }
    else
    {
        // Denegar acceso
    }
}
else
{
    // Redirigir a login
}