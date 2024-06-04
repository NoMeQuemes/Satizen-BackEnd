using Satizen_Api.Models;

public class ActualizarAsignacionDto
{
    //  public int idAsignacion { get; set; }
    public int idPersonal { get; set; }
    public int idSector { get; set; }
    public DiaSemana diaSemana { get; set; }
    public string turno { get; set; }
    public TimeSpan horaInicio { get; set; }
    public TimeSpan horaFinalizacion { get; set; }
}
