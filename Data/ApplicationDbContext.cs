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
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Sector> Sector { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso()
                {
                    idPermiso = 1,
                    tipo = "Crear"
                },
                new Permiso()
                {
                    idPermiso = 2,
                    tipo = "Leer"
                },
                new Permiso()
                {
                    idPermiso = 3,
                    tipo = "Eliminar"
                },
                new Permiso()
                {
                    idPermiso = 4,
                    tipo = "Actualizar"
                }
            );

            modelBuilder.Entity<Roles>().HasData(
                new Roles()
                {
                    idRol = 1,
                    nombre = "Administrador",
                    descripcion = "Soy administrador",
                    idPermiso = 1
                },
                new Roles()
                {
                    idRol = 2,
                    nombre = "Medico",
                    descripcion = "Soy médico",
                    idPermiso = 2
                },
                new Roles()
                {
                    idRol = 3,
                    nombre = "Enfermero",
                    descripcion = "Soy enfermero",
                    idPermiso = 2
                }
            );

            ////// Configuraciones adicionales para relaciones y restricciones
            ////modelBuilder.Entity<Asignacion>()
            ////    .HasOne(a => a.personal)
            ////    .WithMany()
            ////    .HasForeignKey(a => a.idPersonal);

            ////modelBuilder.Entity<Asignacion>()
            ////    .HasOne(a => a.sector)
            ////    .WithMany()
            ////    .HasForeignKey(a => a.idSector);
        }
    }
}
