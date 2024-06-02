using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyec_Satizen_Api.Datos;
using Proyec_Satizen_Api.Models;
using Proyec_Satizen_Api.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Proyec_Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly ILogger<InstitucionController> _logger;
        private readonly ApplicationDbContext _db;

        public InstitucionController (ILogger<InstitucionController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<InstitucionDto>> GetInstitucionModels()
        {
            _logger.LogInformation("Obtener las Instituciones");
            return Ok(_db.Institucions.ToList());
        }

        [HttpGet("id:int", Name ="GetInstitucion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InstitucionDto> GetInstitucion(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Institucion con Id" + id);
                return BadRequest();
            }
            //var institucion = InstitucionStore.institucionList.FirstOrDefault(v => v.idInstitucion == id);
            var institucion = _db.Institucions.FirstOrDefault(v => v.idInstitucion == id);

            if (institucion == null)
            {
                return NotFound();
            }

            return Ok(institucion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InstitucionDto> CrearInstitucion([FromBody] InstitucionDto institucionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Institucions.FirstOrDefault(v => v.nombreInstitucion.ToLower() == institucionDto.nombreInstitucion.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La institucion ya existe");
                return BadRequest(ModelState);
            }
            if (institucionDto == null)
            {
                return BadRequest(institucionDto);
            }
            if (institucionDto.idInstitucion > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            InstitucionModels models = new()
            {
                nombreInstitucion = institucionDto.nombreInstitucion,
                direccionInstitucion = institucionDto.direccionInstitucion,
                telefonoInstitucion = institucionDto.telefonoInstitucion,
                correoInstitucion = institucionDto.correoInstitucion,
                celularInstitucion = institucionDto.celularInstitucion
            };
            _db.Institucions.Add(models);
            _db.SaveChanges();

            return CreatedAtRoute("GetInstitucion", new { idInstucion = institucionDto.idInstitucion }, institucionDto);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteInstitucion(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var institucion = _db.Institucions.FirstOrDefault(v =>v.idInstitucion==id);
            if (institucion == null)
            {
                return NotFound();
            }
            _db.Institucions.Remove(institucion);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateInstitucion(int id, [FromBody] InstitucionDto institucionDto)
        {
            if (institucionDto==null || id != institucionDto.idInstitucion)
            {
                return BadRequest();
            }
            //var institucion = InstitucionStore.institucionList.FirstOrDefault(v => v.idInstitucion == id);
            //institucion.nombreInstitucion = institucionDto.nombreInstitucion;
            //institucion.direccionInstitucion = institucionDto.direccionInstitucion;
            //institucion.telefonoInstitucion = institucionDto.telefonoInstitucion;
            //institucion.correoInstitucion = institucionDto.correoInstitucion;
            //institucion.celularInstitucion = institucionDto.celularInstitucion;

            InstitucionModels models = new()
            {
                idInstitucion = institucionDto.idInstitucion,
                nombreInstitucion = institucionDto.nombreInstitucion,
                direccionInstitucion = institucionDto.direccionInstitucion,
                telefonoInstitucion = institucionDto.telefonoInstitucion,
                correoInstitucion = institucionDto.correoInstitucion,
                celularInstitucion = institucionDto.celularInstitucion
            };

            _db.Institucions.Update(models);
            _db.SaveChanges();

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialInstitucion(int id, JsonPatchDocument<InstitucionDto> patchDto)
        {
            if (patchDto == null || id != 0)
            {
                return BadRequest();
            }
            var institucion = _db.Institucions.AsNoTracking().FirstOrDefault(v => v.idInstitucion == id);

            InstitucionDto institucionDto = new()
            {
                idInstitucion = institucion.idInstitucion,
                nombreInstitucion = institucion.nombreInstitucion,
                direccionInstitucion = institucion.direccionInstitucion,
                telefonoInstitucion = institucion.telefonoInstitucion,
                correoInstitucion = institucion.correoInstitucion,
                celularInstitucion = institucion.celularInstitucion
            };

            if (institucion == null)
                return BadRequest();

            patchDto.ApplyTo(institucionDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            InstitucionModels models = new()
            {
                idInstitucion = institucionDto.idInstitucion,
                nombreInstitucion = institucionDto.nombreInstitucion,
                direccionInstitucion = institucionDto.direccionInstitucion,
                telefonoInstitucion = institucionDto.telefonoInstitucion,
                correoInstitucion = institucionDto.correoInstitucion,
                celularInstitucion = institucionDto.celularInstitucion
            };
            _db.Institucions.Update(models);
            _db.SaveChanges();
            return NoContent();
        }


    }
}

