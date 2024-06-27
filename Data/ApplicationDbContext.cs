using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Satizen_Api.Modelos;
using Satizen_Api.models.Dto;
=======
using Satizen_Api.Models.Dto;
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
using Satizen_Api.Models;
using System.Numerics;
using System.Threading;
using SatizenLlamados.Modelos;
using Satizen_Api.Modelos;

namespace Satizen_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        /* Con esta función usamos los datos que estan en el modelo para que 
         * cuando ejecutemos la migración
         * se agreguen o actualicen en la base de datos
         */
        public DbSet<Usuario> Usuarios { get; set; } //Nando
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Paciente> Pacientes { get; set; } //Amilcar
        public DbSet<Sector> Sectores { get; set; } //Albano
        public DbSet<Personal> Personals { get; set; } //Franco
        public DbSet<Institucion> Instituciones { get; set; }  //Karen
        public DbSet<DispositivoLaboral> DispositivosLaborales { get; set; } // Baraco
        public DbSet<Asignacion> Asignaciones { get; set; } // Alexander
<<<<<<< HEAD
=======
        public DbSet<Contacto> Contactos { get; set; } //Agustin
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
        public DbSet<Llamado> Llamados { get; set; } //Luis


        //Acá se agregan datos a la base de datos
<<<<<<< HEAD


=======
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
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

            //Estas lineas de código agregan el valor a la columna computada "esActivo"

            modelBuilder.Entity<RefreshToken>()
                .Property(o => o.esActivo)
                .HasComputedColumnSql("IIF(fechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1))");


            base.OnModelCreating(modelBuilder);

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