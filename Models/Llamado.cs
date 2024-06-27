using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satizen_Api.Modelos
{
    public class Llamado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
<<<<<<< HEAD
        public int idLlamado { get; set; }
        public int idPaciente { get; set; } //clave foranea (tabla pacientes)
        public int idPersonal { get; set; } //clave foranea (tabla personal)
        public DateTime fechaHoraLlamado { get; set; }
        public string estadoLlamado { get; set; }
=======
        public int idLlamado {  get; set; }
        public int idPaciente { get; set; } //clave foranea (tabla pacientes)
        public int idPersonal { get; set; } //clave foranea (tabla personal)
        public DateTime fechaHoraLlamado { get; set; }
        public string estadoLlamado { get;set; }
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
        public string prioridadLlamado { get; set; }
        public string observacionLlamado { get; set; }

    }
<<<<<<< HEAD
}
=======
}
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
