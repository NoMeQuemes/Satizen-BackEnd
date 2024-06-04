using Microsoft.EntityFrameworkCore;
using Satizen_Api.models.Dto;
using Satizen_Api.Models;
using System.Threading;

namespace Satizen_Api.Data
{
    public class ApplicationDbContext :DbContext
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
        public DbSet<PacientesDto> Pacientes { get; set; }

        //Acá se agregan datos a la base de datos

    }
}
