using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Custom;
using Satizen_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Satizen_Api.Data;
using Satizen_Api.Models.Dto.Usuarios;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous] //Esta sentencia define que no es necesario estar autorizado para usar este controlador
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Utilidades _utilidades;
        public AccesoController(ApplicationDbContext applicationDbContext, Utilidades utilidades)
        {
            _applicationDbContext = applicationDbContext;
            _utilidades = utilidades;
        }


        // EndPoint que registra un usuario
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> Registrarse(UsuarioCreateDto objeto)
        {
            var modeloUsuario = new Usuario
            {
                nombreUsuario = objeto.nombreUsuario,
                password = _utilidades.encriptarSHA256(objeto.password),
                idRoles = objeto.idRoles,
            };

            await _applicationDbContext.Usuarios.AddAsync(modeloUsuario);
            await _applicationDbContext.SaveChangesAsync();

            if (modeloUsuario.idUsuario != 0)
                return StatusCode(StatusCodes.Status200OK, new {isSucccess = true});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSucccess = false });

        }

        // EndPoint que loggea al usuario
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto objeto)
        {
            var usuarioEncontrado = await _applicationDbContext.Usuarios
                                    .Where(u =>
                                    u.nombreUsuario == objeto.nombreUsuario &&
                                    u.password == _utilidades.encriptarSHA256(objeto.password)
                                    ).FirstOrDefaultAsync();
            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado)});


        }
    }
}
