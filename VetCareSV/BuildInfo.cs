namespace VetCareSV;
public static class BuildInfo
{
    // Se genera una vez al arrancar la app — cambia en cada deploy/restart
    public static readonly string CacheBust = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
}
