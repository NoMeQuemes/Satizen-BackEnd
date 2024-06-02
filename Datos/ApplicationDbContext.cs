using Microsoft.EntityFrameworkCore;
using Proyec_Satizen_Api.Models;

namespace Proyec_Satizen_Api.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }


        public DbSet<InstitucionModels>Institucions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstitucionModels>().HasData(
                new InstitucionModels()
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
