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
        private readonly ApplicationDbContext _applicationDbContext;
        protected ApiResponse _response;


        public PacientesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _response = new();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _applicationDbContext.Pacientes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _applicationDbContext.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        //End point para crear nuevos pacientes
        [HttpPost]
        [Route("CrearPaciente")]
        public async Task<ActionResult<ApiResponse>> PostPaciente(AgregarPacienteDto Paciente)
        {
            try
            {
                if (Paciente == null)
                {
                    return BadRequest(Paciente);
                }
               

                Paciente modelo = new()
                {
                    idUsuario = Paciente.idUsuario,
                    //idInstitucion = Paciente.idInstitucion,
                    nombrePaciente = Paciente.nombrePaciente,
                    numeroHabitacionPaciente = Paciente.numeroHabitacionPaciente,
                    fechaIngreso = Paciente.fechaIngreso,
                    observacionPaciente = Paciente.observacionPaciente

                };

                await _applicationDbContext.Pacientes.AddAsync(modelo);
                await _applicationDbContext.SaveChangesAsync();
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.idPaciente)
            {
                return BadRequest();
            }

            _applicationDbContext.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _applicationDbContext.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _applicationDbContext.Pacientes.Remove(paciente);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _applicationDbContext.Pacientes.Any(e => e.idPaciente == id);
        }
    }
}