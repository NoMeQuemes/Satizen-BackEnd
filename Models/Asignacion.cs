using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Asignacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAsignacion { get; set; }

        [ForeignKey("Personal")]
        public int idPersonal { get; set; }
        public virtual Personal personal { get; set; }

        [ForeignKey("Sectores")]
        public int idSector { get; set; }
        public virtual Sector sector { get; set; }
    }
}
