using Microsoft.EntityFrameworkCore;
using Satizen_Api.Models;
using System.Threading;

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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Personal> Personals { get; set; }


        //Acá se agregan datos a la base de datos

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

            {
                modelBuilder.Entity<Institucion>().HasData(
                    new Institucion()
                    {
                        idInstitucion = 1,
                        nombreInstitucion = "Santa",
                        direccionInstitucion = "Caleee",
                        telefonoInstitucion = "53625362",
                        correoInstitucion = "santaqgmail.com",
                        celularInstitucion = "6473467326"


                    }
                    );
            }
        }
    }
}