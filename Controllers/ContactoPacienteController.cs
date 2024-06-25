using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Satizen_Api.Data;
using Satizen_Api.Models.Dto.Contacto;
using Satizen_Api.Models;

using System.Net;
namespace Satizen_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoPacienteController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ContactoPacienteController> _logger;
        private readonly ApiResponse _response;

        public ContactoPacienteController(ApplicationDbContext dbContext, ILogger<ContactoPacienteController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _response = new ApiResponse();
        }


        [HttpGet]
        [Route("ListarContactos")]
        public async Task<ActionResult<ApiResponse>> GetContactos()
        {
            try
            {
                _logger.LogInformation("Obtener los Contactos");

                _response.Resultado = await _dbContext.Contactos
                                              .Where(u => u.eliminado == null)
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


        [HttpGet("{id}", Name = "GetContacto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContactoDto> GetContacto(int id)
        {
            var contacto = _dbContext.Contactos.FirstOrDefault(c => c.idContacto == id);
            if (contacto == null)
            {
                return NotFound();
            }
            return Ok(contacto);
        }

        [HttpPost]
        [Route("CrearContacto")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearContacto([FromBody] ContactoDto contactoDto)
        {
            try
            {
                if (contactoDto == null)
                {
                    return BadRequest(contactoDto);
                }
                else if (contactoDto.idContacto > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                Contacto modelo = new()
                {
                    idContacto = contactoDto.idContacto,
                    idPaciente= contactoDto.idPaciente,
                    celularPaciente= contactoDto.celularPaciente,
                    celularAcompananteP = contactoDto.celularAcompananteP,
                    FechaInicioValidez = contactoDto.FechaInicioValidez,
                    estadoContacto = contactoDto.estadoContacto,
                   
                };

                await _dbContext.Contactos.AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return (_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPatch]
        [Route("EliminarUsuario/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DesactivarContacto(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var contacto = await _dbContext.Contactos.FirstOrDefaultAsync(v => v.idContacto == id);

            if (contacto == null)
            {
                return NotFound();
            }

            // Desactivar el usuario estableciendo la fecha actual en estadoUsuario
            contacto.eliminado = DateTime.Now;

            _dbContext.Contactos.Update(contacto);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateContacto(int id, [FromBody] ContactoDto contactoDto)
        {
            if (contactoDto == null || id != contactoDto.idContacto)
            {
                return BadRequest();
            }
            var contacto = _dbContext.Contactos.FirstOrDefault(v => v.idContacto == id);

            contacto.idPaciente = contactoDto.idPaciente;
            contacto.celularPaciente = contactoDto.celularPaciente;
            contacto.celularAcompananteP = contactoDto.celularAcompananteP;
            contacto.estadoContacto = contactoDto.estadoContacto;

            return NoContent();
        }

        //[HttpPatch("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialContacto(int id, JsonPatchDocument<ContactoDto> patchDto)
        //{
        //    if (patchDto == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var contacto = _dbContext.Contactos.FirstOrDefault(v => v.idContacto == id);

        //    patchDto.ApplyTo(contacto, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return NoContent();
        //}
    }
}
