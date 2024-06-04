namespace Satizen_Api.Models.Dto.Pacientes
{
    public class CreatePacientesDto
    {
        public int idUsuario { get; set; }
        public int idInstitucion { get; set; }
        public string? nombrePaciente { get; set; }
        public int numeroHabitacionPaciente { get; set; }
        public string? observacionPaciente { get; set; }
    }
}
