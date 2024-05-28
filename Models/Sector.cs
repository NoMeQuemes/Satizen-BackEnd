using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Sector
    {
        public Sector()
        {
            nombreSector = "Nombre predeterminado";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSector { get; set; }

        public int idInstitucion { get; set; }
        public string nombreSector { get; set; }
    }
}
