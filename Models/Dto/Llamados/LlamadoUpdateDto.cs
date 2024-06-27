using System.ComponentModel.DataAnnotations;

namespace SatizenLlamados.Modelos.Dto
{
    public class LlamadoUpdateDto
    {
        [Required]
        public int idLlamado { get; set; }
        [Required]
        public int idPaciente { get; set; } //clave foranea (tabla pacientes)
        [Required]
        public int idPersonal { get; set; }//clave foranea (tabla personal)
        [Required]
        public DateTime fechaHoraLlamado { get; set; }
        [Required]
        public string estadoLlamado { get; set; }
        [Required]
        public string prioridadLlamado { get; set; }
        [Required]
        public string observacionLlamado { get; set; }

    }
<<<<<<< HEAD
}
=======
}
>>>>>>> b6f0028134b2241f764abb666decbb0d86f4db5e
