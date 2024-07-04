using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Models
{
    public class Asignacion
    {
        [Key]
        public int idAsignacion { get; set; }
        public int idPersonal { get; set; }
        [ForeignKey("idPersonal")]
        public Personal Personal { get; set; }

        public int idSector { get; set; }
        [ForeignKey("idSector")]
        public Sector Sector { get; set; }

        public DiaSemana diaSemana { get; set; }

        public int TurnoId { get; set; } // Clave foránea
        [ForeignKey("TurnoId")]
        public Turno Turno { get; set; } // Navegación

        public TimeSpan horaInicio { get; set; }
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
