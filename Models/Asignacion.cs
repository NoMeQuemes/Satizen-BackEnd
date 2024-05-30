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
  

        [ForeignKey("Sector")]
        public int idSector { get; set; }

        [Required]
        public DiaSemana diaSemana { get; set; } // Enum DiaSemana

        [Required]
        [StringLength(50)]
        public string turno { get; set; }

        [Required]
        public TimeSpan horaInicio { get; set; }

        [Required]
        public TimeSpan horaFinalizacion { get; set; }
    }

    public enum DiaSemana
    {
        Domingo,
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes,
        Sabado
    }
}
