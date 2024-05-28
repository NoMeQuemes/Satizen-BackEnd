
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.models.Dto
{
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idContacto { get; set; }
        public int idPaciente {  get; set; }
        public int celularPaciente { get; set; }
        public int celularAcompananteP { get; set; }
        public DateTime FechaInicioValidez { get; set; }
        public string estadoContacto { get; set; }
        public DateTime? eliminado { get; set; }

    }
}
