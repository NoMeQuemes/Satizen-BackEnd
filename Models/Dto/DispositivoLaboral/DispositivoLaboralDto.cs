namespace Satizen_Api.DTOs
{
    public class DispositivoLaboralDto
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
