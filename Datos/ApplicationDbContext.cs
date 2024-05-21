using ContactoPacientes.Modelos;
using Microsoft.EntityFrameworkCore;

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
