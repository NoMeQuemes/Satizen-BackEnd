using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Custom;
using Satizen_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Satizen_Api.Data;
using Satizen_Api.Models.Dto.Usuarios;
using System.Net;
using Azure;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous] //Esta sentencia define que no es necesario estar autorizado para usar este controlador
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Utilidades _utilidades;
        protected ApiResponse _response;

        public AccesoController(ApplicationDbContext applicationDbContext, Utilidades utilidades)
        {
            _applicationDbContext = applicationDbContext;
            _utilidades = utilidades;
            _response = new();
        }


        // EndPoint que registra un usuario
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<ActionResult<ApiResponse>> Registrarse(UsuarioCreateDto objeto)
        {
            try
            {
                //Acá se valida que no exista el nombre de usuario
                if (await _applicationDbContext.Usuarios.AnyAsync(u => u.nombreUsuario == objeto.nombreUsuario))
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages = new List<string> { "El nombre de usuarios ya existe" };
                    return StatusCode((int)_response.statusCode, _response);
                }


                var modeloUsuario = new Usuario
                {
                    nombreUsuario = objeto.nombreUsuario,
                    password = _utilidades.encriptarSHA256(objeto.password),
                    idRoles = objeto.idRoles,
                };

                await _applicationDbContext.Usuarios.AddAsync(modeloUsuario);
                await _applicationDbContext.SaveChangesAsync();

                if (modeloUsuario.idUsuario != 0)
                {
                    _response.statusCode = HttpStatusCode.OK;
                    _response.IsExitoso = true;
                    _response.Resultado = modeloUsuario;
                }
                else
                {
                    _response.statusCode = HttpStatusCode.InternalServerError;
                    _response.IsExitoso = false;
                    _response.ErrorMessages = new List<string> { "Error al registrar el usuario." };
                }

                return StatusCode((int)_response.statusCode, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return StatusCode((int)_response.statusCode, _response);
            }

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
