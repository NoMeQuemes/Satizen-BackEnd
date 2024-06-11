using Microsoft.EntityFrameworkCore;
using Satizen_Api.Models;

namespace Satizen_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Turno> Turnos { get; set; }  // Agregar el DbSet para la nueva tabla Turnos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Datos iniciales para Permiso
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { idPermiso = 1, tipo = "Crear" },
                new Permiso { idPermiso = 2, tipo = "Leer" },
                new Permiso { idPermiso = 3, tipo = "Eliminar" },
                new Permiso { idPermiso = 4, tipo = "Actualizar" }
            );

            // Datos iniciales para Roles
            modelBuilder.Entity<Roles>().HasData(
                new Roles { idRol = 1, nombre = "Administrador", descripcion = "Soy administrador", idPermiso = 1 },
                new Roles { idRol = 2, nombre = "Medico", descripcion = "Soy médico", idPermiso = 2 },
                new Roles { idRol = 3, nombre = "Enfermero", descripcion = "Soy enfermero", idPermiso = 2 }
            );

            // Datos iniciales para Turnos
            modelBuilder.Entity<Turno>().HasData(
                new Turno { TurnoId = 1, Nombre = "Mañana" },
                new Turno { TurnoId = 2, Nombre = "Tarde" },
                new Turno { TurnoId = 3, Nombre = "Noche" }
            );

            // Configuraciones adicionales para relaciones y restricciones
            // Descomenta y ajusta según tus necesidades específicas
            ////modelBuilder.Entity<Asignacion>()
            ////    .HasOne(a => a.Personal) // Asegúrate de tener una entidad Personal
            ////    .WithMany()
            ////    .HasForeignKey(a => a.idPersonal);

            ////modelBuilder.Entity<Asignacion>()
            ////    .HasOne(a => a.Sector) // Asegúrate de tener una entidad Sector
            ////    .WithMany()
            ////    .HasForeignKey(a => a.idSector);

            ////modelBuilder.Entity<Asignacion>()
            ////    .HasOne(a => a.Turno) // Relación con Turno
            ////    .WithMany()
            ////    .HasForeignKey(a => a.TurnoId)
            ////    .OnDelete(DeleteBehavior.Restrict); // Considera cómo quieres manejar la eliminación
        }
    }
}
