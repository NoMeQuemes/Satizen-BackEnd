using Satizen_Api.Models;

public class CrearAsigcionDto
{
    public int idPersonal { get; set; }
    public int idSector { get; set; }
    public DiaSemana diaSemana { get; set; }
    public int TurnoId { get; set; }
    public TimeSpan horaInicio { get; set; }
    public TimeSpan horaFinalizacion { get; set; }
}