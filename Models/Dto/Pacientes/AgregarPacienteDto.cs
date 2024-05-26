namespace Satizen_Api.Models.Dto.Pacientes
{
    public class AgregarPacienteDto
    {
        //public int idInstitucion { get; set; }
        public int idUsuario { get; set; }
        public string? nombrePaciente { get; set; }
        public int numeroHabitacionPaciente { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string? observacionPaciente { get; set; }

    }
}
