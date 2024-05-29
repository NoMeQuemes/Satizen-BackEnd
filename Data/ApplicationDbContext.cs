using Microsoft.EntityFrameworkCore;
using Satizen_Api.Models;
using Satizen_Api.Modelos;
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
        
        public DbSet<Llamado> Llamados { get; set; }

        //Acá se agregan datos a la base de datos

        

    }
}
