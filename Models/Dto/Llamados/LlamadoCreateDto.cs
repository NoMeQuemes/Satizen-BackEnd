using System.ComponentModel.DataAnnotations;

namespace SatizenLlamados.Modelos.Dto
{
    public class LlamadoCreateDto
    {
        public int idPaciente { get; set; } //clave foranea (tabla pacientes)
        public int idPersonal { get; set; } //clave foranea (tabla personal)
        public DateTime fechaHoraLlamado { get; set; }
        public required string estadoLlamado { get; set; }
        public required string prioridadLlamado { get; set; }
        public required string observacionLlamado { get; set; }
    }

}


