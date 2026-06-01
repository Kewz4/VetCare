using Microsoft.EntityFrameworkCore;
using WebAppEscuela.Models;

namespace WebAppEscuela.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la tabla alumnos
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("alumnos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
                
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("apellido");
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("email");
                
                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");
                
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");
                
                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion");
                
                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("fecha_registro");
                
                entity.Property(e => e.Activo)
                    .HasDefaultValue(true)
                    .HasColumnName("activo");
            });
        }
    }
}
