using Microsoft.EntityFrameworkCore;

namespace Satizen_Api.Models
{
    public class PacientesContext : DbContext
    {


        public PacientesContext(DbContextOptions<PacientesContext> options)
            : base(options)
        {
        }


        public DbSet<PacientesModels> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}

