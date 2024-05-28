using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Data;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto.Pacientes;
using System.Net;
using System.Threading;


namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Esta sentencia determina que a esta api solo pueden entrar usuarios autorizados
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly PacientesContext _dbContext;
        private readonly ILogger<PacienteController> _logger;
        private readonly ApiResponse _response;

        public PacientesController(PacientesContext dbContext, ILogger<PacientesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _response = new ApiResponse();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route("ListarPacientes")]
        public async Task<ActionResult<ApiResponse>> GetPaciente()
        {
            try
            {
                _logger.LogInformation("Obtener los Pacientes");

                _response.Resultado = await _dbContext.Pacientes
                                              .Where(u => u.estadoPaciente == null)
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

        [HttpGet("{id:int}", Name = "GetPaciente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PacientesDto>> GetPaciente(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error al traer paciente con Id " + id);
                return BadRequest();
            }

            var paciente = await _dbContext.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PacientesDto>> CrearPaciente([FromBody] PacientesDto pacientesDto)
        {
            if (pacientesDto == null)
            {
                return BadRequest();
            }

            var paciente = new PacientesDto
            {
                idPaciente = pacientesDto.idPaciente,
                idUsuario = pacientesDto.idUsuario,
                idInstitucion = pacientesDto.idInstitucion,
                nombrePaciente = pacientesDto.nombrePaciente,
                numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente,
                fechaIngreso = pacientesDto.fechaIngreso,
                observacionPaciente = pacientesDto.observacionPaciente
            };

            await _dbContext.Pacientes.AddAsync(paciente);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPaciente", new { id = paciente.idPaciente }, paciente);
        }

        [HttpPatch]
        [Route("DesactivarPaciente/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DesactivarPaciente(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(p => p.idPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }

            // Desactivar el paciente estableciendo la fecha actual en estadoPaciente
            paciente.estadoPaciente = DateTime.Now;

            _dbContext.Pacientes.Update(paciente);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] PacientesDto pacientesDto)
        {
            if (pacientesDto == null || id != pacientesDto.idPaciente)
            {
                return BadRequest();
            }

            var paciente = await _dbContext.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            paciente.idUsuario = pacientesDto.idUsuario;
            paciente.idInstitucion = pacientesDto.idInstitucion;
            paciente.nombrePaciente = pacientesDto.nombrePaciente;
            paciente.numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente;
            paciente.fechaIngreso = pacientesDto.fechaIngreso;
            paciente.observacionPaciente = pacientesDto.observacionPaciente;

            _dbContext.Pacientes.Update(paciente);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPaciente(int id, [FromBody] JsonPatchDocument<PacientesDto> patchDto)
        {
            if (patchDto == null || id <= 0)
            {
                return BadRequest();
            }

            var paciente = await _dbContext.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            var pacienteDto = new PacientesDto
            {
                idPaciente = paciente.idPaciente,
                idUsuario = paciente.idUsuario,
                idInstitucion = paciente.idInstitucion,
                nombrePaciente = paciente.nombrePaciente,
                numeroHabitacionPaciente = paciente.numeroHabitacionPaciente,
                fechaIngreso = paciente.fechaIngreso,
                observacionPaciente = paciente.observacionPaciente
            };

            patchDto.ApplyTo(pacienteDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            paciente.idUsuario = pacienteDto.idUsuario;
            paciente.idInstitucion = pacienteDto.idInstitucion;
            paciente.nombrePaciente = pacienteDto.nombrePaciente;
            paciente.numeroHabitacionPaciente = pacienteDto.numeroHabitacionPaciente;
            paciente.fechaIngreso = pacienteDto.fechaIngreso;
            paciente.observacionPaciente = pacienteDto.observacionPaciente;

            _dbContext.Pacientes.Update(paciente);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
