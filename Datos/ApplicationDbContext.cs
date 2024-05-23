using Microsoft.EntityFrameworkCore;
using Satizen_Api.models.Dto;

namespace Satizen_Api.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Contacto> Contactos { get; set; }

     
      
    }
}
