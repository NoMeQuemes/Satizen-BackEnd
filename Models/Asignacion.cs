using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Asignacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAsignacion { get; set; }

        public int idPersonal { get; set; }
        [ForeignKey("idPersonal")]
        public virtual Personal Personal { get; set; }
  

        public int idSector { get; set; }
        [ForeignKey("idSector")]
        public virtual Sector Sector { get; set; }

        public DateTime diaSemana { get; set; }

        [Required]
        [StringLength(50)]
        public string turno { get; set; } //Esto tiene que ser una tabla aparte

        [Required]
        public TimeSpan horaInicio { get; set; }

        [Required]
        public TimeSpan horaFinalizacion { get; set; }
    }
}
