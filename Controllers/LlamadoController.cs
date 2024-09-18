using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Satizen_Api.Data;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto.Llamados;

using System.Net;

namespace Satizen_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LlamadoController : ControllerBase
    {
        private readonly ILogger<LlamadoController> _logger;
        private readonly ApplicationDbContext _llamadoContext;
        protected ApiResponse _response;

        public LlamadoController(ILogger<LlamadoController> logger, ApplicationDbContext llamadoContext)
        {
            _logger = logger;
            _llamadoContext = llamadoContext;
            _response = new();
        }


        [Authorize(Policy = "AdminDoctor")]
        [HttpGet]
        [Route("ListarLlamados")]
        public async Task<ActionResult<ApiResponse>> GetLlamados()
        {
            try
            {
                _logger.LogInformation("Obtener los usuarios"); // Esto solo muestra en consola que se ejecutó este endpoint

                _response.Resultado = await _llamadoContext.Llamados
                                              .Where(u => u.fechaEliminacion == null)
                                              .ToListAsync();
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [Authorize(Policy = "AdminDoctor")]
        [HttpGet("{id:int}", Name = "GetLlamado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetLlamado(int id)
        {
            try
            {
                var llamado = await _llamadoContext.Llamados.FindAsync(id);
                if (llamado == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { "Llamado not found" };
                    return NotFound(_response);
                }

                _response.Resultado = llamado;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _response.statusCode = HttpStatusCode.InternalServerError;
            }
            return StatusCode((int)_response.statusCode, _response);
        }



        [Authorize(Policy = "AdminDoctor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CrearLlamado([FromBody] LlamadoCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid input data" };
                    return BadRequest(_response);
                }

                var llamado = new Llamado
                {
                    idPaciente = createDto.idPaciente,
                    idPersonal = createDto.idPersonal,
                    fechaHoraLlamado = createDto.fechaHoraLlamado,
                    estadoLlamado = createDto.estadoLlamado,
                    prioridadLlamado = createDto.prioridadLlamado,
                    observacionLlamado = createDto.observacionLlamado
                };

                await _llamadoContext.Llamados.AddAsync(llamado);
                await _llamadoContext.SaveChangesAsync();

                _response.Resultado = llamado;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetLlamado", new { id = llamado.idLlamado }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _response.statusCode = HttpStatusCode.InternalServerError;
            }
            return StatusCode((int)_response.statusCode, _response);
        }



        [Authorize(Policy = "AdminDoctor")]
        [HttpPatch]
        [Route("EliminarLlamado/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var llamado = await _llamadoContext.Llamados.FirstOrDefaultAsync(v => v.idLlamado == id);

            if (llamado == null)
            {
                return NotFound();
            }

            llamado.fechaEliminacion = DateTime.Now;

            _llamadoContext.Llamados.Update(llamado);
            await _llamadoContext.SaveChangesAsync();

            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }



        [Authorize(Policy = "AdminDoctor")]
        [HttpPut]
        [Route("ActualizarLlamado/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateLlamado(int id, [FromBody] LlamadoUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var llamadoExistente = await _llamadoContext.Llamados.FirstOrDefaultAsync(e => e.idLlamado == id);

                if (llamadoExistente == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { "El llamado no existe" };
                }

                llamadoExistente.idPaciente = updateDto.idPaciente;
                llamadoExistente.idPersonal = updateDto.idPersonal;
                llamadoExistente.fechaHoraLlamado = updateDto.fechaHoraLlamado;
                llamadoExistente.estadoLlamado = updateDto.estadoLlamado;
                llamadoExistente.prioridadLlamado = updateDto.prioridadLlamado;
                llamadoExistente.observacionLlamado = updateDto.observacionLlamado;

                _llamadoContext.Llamados.Update(llamadoExistente);
                await _llamadoContext.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [Authorize(Policy = "AdminDoctor")]
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialLlamado(int id, JsonPatchDocument<LlamadoUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var llamado = await _llamadoContext.Llamados.FindAsync(id);


            LlamadoUpdateDto llamadoDto = new()
            {
                idLlamado = llamado.idLlamado,
                idPaciente = llamado.idPaciente,
                idPersonal = llamado.idPersonal,
                fechaHoraLlamado = llamado.fechaHoraLlamado,
                estadoLlamado = llamado.estadoLlamado,
                prioridadLlamado = llamado.prioridadLlamado,
                observacionLlamado = llamado.observacionLlamado
            };

            if (llamado == null) return BadRequest();

            patchDto.ApplyTo(llamadoDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Llamado modelo = new()
            {
                idLlamado = llamadoDto.idLlamado,
                idPaciente = llamadoDto.idPaciente,
                idPersonal = llamadoDto.idPersonal,
                fechaHoraLlamado = llamadoDto.fechaHoraLlamado,
                estadoLlamado = llamadoDto.estadoLlamado,
                prioridadLlamado = llamadoDto.prioridadLlamado,
                observacionLlamado = llamadoDto.observacionLlamado
            };

            _llamadoContext.Llamados.Update(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }

}



