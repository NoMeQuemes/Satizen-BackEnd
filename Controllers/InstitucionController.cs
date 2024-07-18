using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using Satizen_Api.Data;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto.Institucion;

namespace Proyec_Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly ILogger<InstitucionController> _logger;
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;

        public InstitucionController(ILogger<InstitucionController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            _response = new ApiResponse();
        }


        [HttpGet]
        [Route("ListarInstituciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetInstituciones()
        {
            try
            {
                _logger.LogInformation("Obtener las Instituciones");

                _response.Resultado = await _db.Instituciones
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

        [HttpGet]
        [Route("ListarPorId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InstitucionDto>> GetInstitucion(int id)
        {
            var institucion = _db.Instituciones.FirstOrDefault(c => c.idInstitucion == id);

            if (institucion == null)
            {
                return NotFound();
            }
            return Ok(institucion);
        }

        [HttpPost]
        [Route("CrearInstitucion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearInstitucion([FromBody] InstitucionCreateDto institucioncreateDto)
        {
            try
            {
                if (institucioncreateDto == null)
                {
                    return BadRequest(institucioncreateDto);
                }

                Institucion modelo = new()
                {
                    nombreInstitucion = institucioncreateDto.nombreInstitucion,
                    direccionInstitucion = institucioncreateDto.direccionInstitucion,
                    telefonoInstitucion = institucioncreateDto.telefonoInstitucion,
                    correoInstitucion = institucioncreateDto.correoInstitucion,
                    celularInstitucion = institucioncreateDto.celularInstitucion,

                };

                await _db.Instituciones.AddAsync(modelo);
                await _db.SaveChangesAsync();
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

        [HttpPut]
        [Route("ActualizarInstitucion/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateInstitucion(int id, [FromBody] InstitucionUpdateDto institucionupdateDto)
        {
            if (institucionupdateDto == null || id != institucionupdateDto.idInstitucion)
            {
                return BadRequest();
            }

            var institucion = _db.Instituciones.FirstOrDefault(v => v.idInstitucion == id);

            if (institucion == null)
            {
                return NotFound(); // Agregar manejo para cuando no se encuentra la institución
            }

            institucion.nombreInstitucion = institucionupdateDto.nombreInstitucion;
            institucion.direccionInstitucion = institucionupdateDto.direccionInstitucion;
            institucion.telefonoInstitucion = institucionupdateDto.telefonoInstitucion;
            institucion.correoInstitucion = institucionupdateDto.correoInstitucion;
            institucion.celularInstitucion = institucionupdateDto.celularInstitucion;

            _db.SaveChanges(); // Guardar los cambios en la base de datos

            return NoContent();
        }


        [HttpPatch]
        [Route("EliminarInstitucion/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarInstitucion(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var institucion = await _db.Instituciones.FirstOrDefaultAsync(v => v.idInstitucion == id);

            if (institucion == null)
            {
                return NotFound();
            }

            institucion.eliminado = DateTime.Now;

            _db.Instituciones.Update(institucion);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
