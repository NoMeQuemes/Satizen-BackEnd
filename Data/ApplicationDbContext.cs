using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Api.Models;
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
        public DbSet<Usuario> Usuarios { get; set; } //Nando
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Paciente> Pacientes { get; set; } //Amilcar
        public DbSet<Sectores> Sectores { get; set; } //Albano
        public DbSet<Personal> Personals { get; set; } //Franco
        public DbSet<Institucion> Instituciones { get; set; } // Karen
        public DbSet<DispositivoLaboral> DispositivosLaborales { get; set; } // Baraco


        //Acá se agregan datos a la base de datos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            //Estas lineas de código agregan el valor a la columna computada "esActivo"
            modelBuilder.Entity<RefreshToken>()
                .Property(o => o.esActivo)
                .HasComputedColumnSql("IIF(fechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1))");
        }
    }
}