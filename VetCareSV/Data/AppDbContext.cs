using Microsoft.EntityFrameworkCore;
using VetCareSV.Models;
namespace VetCareSV.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Veterinaria> Veterinarias { get; set; }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
    public DbSet<ComercioAliado> ComerciosAliados { get; set; }
    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(e => {
            e.ToTable("usuarios");
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
            e.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(150);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
            e.Property(u => u.Rol).HasColumnName("rol").HasDefaultValue("Dueno");
            e.Property(u => u.FechaRegistro).HasColumnName("fecha_registro").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Mascota>(e => {
            e.ToTable("mascotas");
            e.HasKey(m => m.Id);
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
            e.Property(m => m.Especie).HasColumnName("especie").IsRequired();
            e.Property(m => m.Raza).HasColumnName("raza").HasMaxLength(100);
            e.Property(m => m.Edad).HasColumnName("edad");
            e.Property(m => m.Color).HasColumnName("color");
            e.Property(m => m.UsuarioId).HasColumnName("usuario_id");
            e.HasOne(m => m.Usuario).WithMany(u => u.Mascotas).HasForeignKey(m => m.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Veterinaria>(e => {
            e.ToTable("veterinarias");
            e.HasKey(v => v.Id);
            e.Property(v => v.Id).HasColumnName("id");
            e.Property(v => v.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
            e.Property(v => v.Direccion).HasColumnName("direccion").IsRequired();
            e.Property(v => v.Departamento).HasColumnName("departamento").HasMaxLength(100);
            e.Property(v => v.Telefono).HasColumnName("telefono").HasMaxLength(20);
            e.Property(v => v.Email).HasColumnName("email");
            e.Property(v => v.Horario).HasColumnName("horario");
            e.HasData(
                new Veterinaria { Id = 1, Nombre = "Clínica Veterinaria San Salvador Centro", Direccion = "Calle Arce, San Salvador", Departamento = "San Salvador", Telefono = "2222-1111", Email = "info@vetsancen.sv", Horario = "Lun-Vie 8am-6pm" },
                new Veterinaria { Id = 2, Nombre = "Veterinaria Santa Ana Animal Care", Direccion = "4a Avenida Sur, Santa Ana", Departamento = "Santa Ana", Telefono = "2441-2200", Email = "info@vetsa.sv", Horario = "Lun-Sáb 9am-5pm" },
                new Veterinaria { Id = 3, Nombre = "Clínica Mascotas Soyapango", Direccion = "Bulevar del Ejército, Soyapango", Departamento = "San Salvador", Telefono = "2277-3300", Horario = "Lun-Vie 8am-7pm" },
                new Veterinaria { Id = 4, Nombre = "VetSalud La Libertad", Direccion = "Carretera El Litoral Km 15, La Libertad", Departamento = "La Libertad", Telefono = "2346-5500", Horario = "Mar-Dom 8am-5pm" },
                new Veterinaria { Id = 5, Nombre = "Veterinaria San Miguel Norte", Direccion = "Av. Roosevelt, San Miguel", Departamento = "San Miguel", Telefono = "2661-4400", Horario = "Lun-Sáb 8am-6pm" }
            );
        });

        modelBuilder.Entity<Cita>(e => {
            e.ToTable("citas");
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).HasColumnName("id");
            e.Property(c => c.Fecha).HasColumnName("fecha").HasColumnType("timestamp");
            e.Property(c => c.Estado).HasColumnName("estado").HasDefaultValue("Pendiente");
            e.Property(c => c.Motivo).HasColumnName("motivo");
            e.Property(c => c.MascotaId).HasColumnName("mascota_id");
            e.Property(c => c.VeterinariaId).HasColumnName("veterinaria_id");
            e.HasOne(c => c.Mascota).WithMany(m => m.Citas).HasForeignKey(c => c.MascotaId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(c => c.Veterinaria).WithMany(v => v.Citas).HasForeignKey(c => c.VeterinariaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HistorialMedico>(e => {
            e.ToTable("historial_medico");
            e.HasKey(h => h.Id);
            e.Property(h => h.Id).HasColumnName("id");
            e.Property(h => h.Diagnostico).HasColumnName("diagnostico").IsRequired();
            e.Property(h => h.Tratamiento).HasColumnName("tratamiento").IsRequired();
            e.Property(h => h.Observaciones).HasColumnName("observaciones");
            e.Property(h => h.FechaRegistro).HasColumnName("fecha_registro").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(h => h.CitaId).HasColumnName("cita_id");
            e.HasOne(h => h.Cita).WithOne(c => c.HistorialMedico).HasForeignKey<HistorialMedico>(h => h.CitaId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ComercioAliado>(e => {
            e.ToTable("comercios_aliados");
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).HasColumnName("id");
            e.Property(c => c.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
            e.Property(c => c.Categoria).HasColumnName("categoria").IsRequired();
            e.Property(c => c.Direccion).HasColumnName("direccion").IsRequired();
            e.Property(c => c.Departamento).HasColumnName("departamento");
            e.Property(c => c.Telefono).HasColumnName("telefono");
            e.Property(c => c.Descripcion).HasColumnName("descripcion");
            e.HasData(
                new ComercioAliado { Id = 1, Nombre = "PetShop El Salvador", Categoria = "Tienda", Direccion = "Centro Comercial Metrocentro, San Salvador", Departamento = "San Salvador", Telefono = "2264-5500", Descripcion = "Tienda de mascotas con alimentos, accesorios y juguetes." },
                new ComercioAliado { Id = 2, Nombre = "Farmacia Veterinaria Dueñas", Categoria = "Farmacia", Direccion = "Colonia Escalón, San Salvador", Departamento = "San Salvador", Telefono = "2263-4400", Descripcion = "Medicamentos y suplementos para toda especie." },
                new ComercioAliado { Id = 3, Nombre = "Groomers SV - Peluquería Canina", Categoria = "Grooming", Direccion = "Av. Masferrer, San Salvador", Departamento = "San Salvador", Telefono = "7788-9900", Descripcion = "Servicio de baño, corte y estética para perros y gatos." },
                new ComercioAliado { Id = 4, Nombre = "Agropecuaria Hermanos López", Categoria = "Agropecuaria", Direccion = "Km 25 Carretera a Santa Ana", Departamento = "La Libertad", Telefono = "2338-1100", Descripcion = "Alimentos para ganado, aves y mascotas." },
                new ComercioAliado { Id = 5, Nombre = "Casa del Acuario SV", Categoria = "Tienda", Direccion = "1a Calle Poniente, Santa Ana", Departamento = "Santa Ana", Telefono = "2441-6600", Descripcion = "Peces, reptiles y accesorios especializados." },
                new ComercioAliado { Id = 6, Nombre = "PetFood Premium SV", Categoria = "Tienda", Direccion = "Col. San Benito, San Salvador", Departamento = "San Salvador", Telefono = "2264-7700", Descripcion = "Alimentos premium y orgánicos para mascotas." }
            );
        });

        modelBuilder.Entity<Producto>(e => {
            e.ToTable("productos");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
            e.Property(p => p.Descripcion).HasColumnName("descripcion");
            e.Property(p => p.Precio).HasColumnName("precio").HasColumnType("decimal(10,2)");
            e.Property(p => p.ComercioId).HasColumnName("comercio_id");
            e.HasOne(p => p.Comercio).WithMany(c => c.Productos).HasForeignKey(p => p.ComercioId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
