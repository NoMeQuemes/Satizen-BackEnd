using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class PacientesModels
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPaciente { get; set; }
        public int idUsuario { get; set; }
        public int idInstitucion { get; set; }
        public string? nombrePaciente { get; set; }
        public int numeroHabitacionPaciente { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string? observacionPaciente { get; set; }
        [ForeignKey("idUsuario")]
        public virtual Usuario usuario { get; set; }
        [ForeignKey("idInstitucion")]
        public virtual Instituciones institucion { get; set; }
    }
}

