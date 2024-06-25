namespace Satizen_Api.Models.Dto.DispositivoLaboral
{
    public class ActualizarDispositivoLaboralDto
    {
        public int idTelefonoEmpresa { get; set; }
        public int idPersonal { get; set; }
        public required string numeroEmpresa { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public string? almacenamiento { get; set; }
        public string? color { get; set; }
    }
}
