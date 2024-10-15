using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

using Satizen_Api.Data;
using Satizen_Api.Models.Dto;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto.Pacientes;

using System.Net;
using System.Threading;


namespace Satizen_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<PacientesController> _logger;
        private readonly ApiResponse _response;

        public PacientesController(ApplicationDbContext dbContext, ILogger<PacientesController> logger)
        {
            _applicationDbContext = dbContext;
            _logger = logger;
            _response = new ApiResponse();
        }

        [Authorize(Policy = "AdminDoctorEnfermero")]
        [HttpGet]
        [Route("ListarPacientes")]
        public async Task<ActionResult<ApiResponse>> GetPaciente()
        {
            try
            {

                _response.Resultado = await _applicationDbContext.Pacientes
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

        [Authorize(Policy = "AdminDoctorEnfermero")]
        [HttpGet]
        [Route("ListarPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PacientesDto>> GetPaciente(int id)
        {


            var paciente = await _applicationDbContext.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        [Authorize(Policy = "AdminDoctorEnfermero")]
        [HttpPost]
        [Route("CrearPaciente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatePacientesDto>> CrearPaciente([FromBody] CreatePacientesDto pacientesDto)
        {
            if (pacientesDto == null)
            {
                return BadRequest();
            }

            Paciente modelo = new()
            {
                idUsuario = pacientesDto.idUsuario,
                idInstitucion = pacientesDto.idInstitucion,
                nombrePaciente = pacientesDto.nombrePaciente,
                apellido = pacientesDto.apellido,
                dni = pacientesDto.dni,
                numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente,
                observacionPaciente = pacientesDto.observacionPaciente,
                fechaIngreso = DateTime.Now
            };

            await _applicationDbContext.Pacientes.AddAsync(modelo);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPaciente", new { id = modelo.idPaciente }, modelo);
        }

        [Authorize(Policy = "AdminDoctorEnfermero")]
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

            var paciente = await _applicationDbContext.Pacientes.FirstOrDefaultAsync(p => p.idPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }

            // Desactivar el paciente estableciendo la fecha actual en estadoPaciente
            paciente.estadoPaciente = DateTime.Now;

            _applicationDbContext.Pacientes.Update(paciente);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Policy = "AdminDoctorEnfermero")]
        [HttpPut]
        [Route("ActualizarPaciente/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] UpdatePacientesDto pacientesDto)
        {


            var paciente = await _applicationDbContext.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            paciente.idInstitucion = pacientesDto.idInstitucion;
            paciente.nombrePaciente = pacientesDto.nombrePaciente;
            paciente.apellido = pacientesDto.apellido;
            paciente.dni = pacientesDto.dni;
            paciente.numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente;
            paciente.observacionPaciente = pacientesDto.observacionPaciente;

            _applicationDbContext.Pacientes.Update(paciente);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
