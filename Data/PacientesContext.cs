using Microsoft.EntityFrameworkCore;
using Satizen_Api.models.Dto;

namespace Satizen_Api.models.Dto
{
    public class PacientesContext : DbContext
    {


        public PacientesContext(DbContextOptions<PacientesContext> options)
            : base(options)
        {
        }


        public DbSet<PacientesDto> Pacientes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               
        }
    }
}

