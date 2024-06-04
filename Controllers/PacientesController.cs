using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Satizen_Api.Models;
using System.Net;
using System.Threading;
using Satizen_Api.models.Dto;
using Satizen_Api.Data;
using Satizen_Api.Models.Dto.Pacientes;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected ApiResponse _response;


        public PacientesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _response = new();
        }


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

        [HttpGet("{id:int}", Name = "GetPaciente")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatePacientesDto>> CrearPaciente([FromBody] CreatePacientesDto pacientesDto)
        {
            if (pacientesDto == null)
            {
                return BadRequest();
            }

            var paciente = new PacientesDto
            {
                idUsuario = pacientesDto.idUsuario,
                idInstitucion = pacientesDto.idInstitucion,
                nombrePaciente = pacientesDto.nombrePaciente,
                numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente,
                observacionPaciente = pacientesDto.observacionPaciente
            };

            await _applicationDbContext.Pacientes.AddAsync(paciente);
            await _applicationDbContext.SaveChangesAsync();

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

        [HttpPut("{id:int}")]
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
            paciente.numeroHabitacionPaciente = pacientesDto.numeroHabitacionPaciente;
            paciente.observacionPaciente = pacientesDto.observacionPaciente;

            _applicationDbContext.Pacientes.Update(paciente);
            await _applicationDbContext.SaveChangesAsync();

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

            var pacientes = await _applicationDbContext.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }

            var pacienteDto = new PacientesDto
            {
                idPaciente = pacientes.idPaciente,
                idUsuario = pacientes.idUsuario,
                idInstitucion = pacientes.idInstitucion,
                nombrePaciente = pacientes.nombrePaciente,
                numeroHabitacionPaciente = pacientes.numeroHabitacionPaciente,
                fechaIngreso = pacientes.fechaIngreso,
                observacionPaciente = pacientes.observacionPaciente
            };

            patchDto.ApplyTo(pacienteDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pacientes.idUsuario = pacienteDto.idUsuario;
            pacientes.idInstitucion = pacienteDto.idInstitucion;
            pacientes.nombrePaciente = pacienteDto.nombrePaciente;
            pacientes.numeroHabitacionPaciente = pacienteDto.numeroHabitacionPaciente;
            pacientes.fechaIngreso = pacienteDto.fechaIngreso;
            pacientes.observacionPaciente = pacienteDto.observacionPaciente;

            _applicationDbContext.Pacientes.Update(pacientes);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
