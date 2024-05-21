using Satizen_Api.Datos;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoPacienteController : ControllerBase
    {
        private readonly ILogger<ContactoPacienteController> _logger;
        public ContactoPacienteController(ILogger<ContactoPacienteController>logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ContactoDto>> GetContactoPacientes()
        {
            _logger.LogInformation("Obtener los ContactoPaciente");
            return Ok(ContactoStore.contactoList);
        }

        [HttpGet("{id:int}", Name = "GetContacto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContactoDto> GetContactos(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer ContactoPaciente con Id" + id);
                return BadRequest();
            }
            var contacto = ContactoStore.contactoList.FirstOrDefault(v => v.idContacto == id);

            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ContactoDto> CrearContacto([FromBody] ContactoDto contactoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contactoDto == null)
            {
                return BadRequest(contactoDto);
            }
            if (contactoDto.idContacto > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            contactoDto.idContacto = ContactoStore.contactoList.OrderByDescending(v => v.idContacto).
                FirstOrDefault().idContacto + 1;

            ContactoStore.contactoList.Add(contactoDto);

            return CreatedAtRoute("GetContacto", new { id = contactoDto.idContacto }, contactoDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var contacto = ContactoStore.contactoList.FirstOrDefault(v => v.idContacto == id);
            if (contacto == null)
            {
                return NotFound();
            }
            ContactoStore.contactoList.Remove(contacto);

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
            var contacto = ContactoStore.contactoList.FirstOrDefault(v => v.idContacto == id);
            contacto.idPaciente = contactoDto.idPaciente;
            contacto.celularPaciente = contactoDto.celularPaciente;
            contacto.celularAcompananteP = contactoDto.celularAcompananteP;
            contacto.estadoContacto = contactoDto.estadoContacto;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialContacto(int id, JsonPatchDocument<ContactoDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var contacto = ContactoStore.contactoList.FirstOrDefault(v => v.idContacto == id);

            patchDto.ApplyTo(contacto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
