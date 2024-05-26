namespace Satizen_Api.Models.Dto.Usuarios
{
    public class UsuarioDto
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public int idRoles { get; set; }
        public int estadoUsuario { get; set; }

    }
}
