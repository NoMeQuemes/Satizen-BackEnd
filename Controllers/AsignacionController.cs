using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;      // Para trabajar con solicitudes y respuestas HTTP
using Microsoft.AspNetCore.Mvc;       // Base para controladores MVC
using Microsoft.EntityFrameworkCore;  // Interacciones con la base de datos (Entity Framework)
using Microsoft.AspNetCore.JsonPatch; // Para actualizaciones parciales (no se usa en este controlador, pero lo mantenemos para consistencia)
using Microsoft.Extensions.Logging;   // Para registrar eventos en la aplicación (logs)
using System.Net;                     // Para códigos de estado HTTP (OK, NotFound, etc.)
using System.Threading.Tasks;        // Para operaciones asíncronas
using System.Collections.Generic;    // Para colecciones (listas, etc.)

using Satizen_Api.Data;            // Tu contexto de base de datos (ApplicationDbContext)
using Satizen_Api.Models;           // Tus modelos de datos (Asignacion, etc.)
using Satizen_Api.Models.Dto;       // Tus Data Transfer Objects (DTOs) (AsignacionDto)

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Esta sentencia determina que a esta API solo pueden entrar usuarios autorizados
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected ApiResponse _response;

        public AsignacionesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _response = new();
        }

        //--------------- EndPoint que trae la lista completa de asignaciones -------------------
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAsignaciones()
        {
            try
            {
                var asignaciones = await _applicationDbContext.Asignaciones
                                          //.Include(a => a.personal)  
                                          //.Include(a => a.sector)
                                          .ToListAsync();

                _response.Resultado = asignaciones;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //------------- EndPoint que trae una asignacion a través de la id --------------
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetAsignacion(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var asignacion = await _applicationDbContext.Asignaciones
                                            //.Include(a => a.personal)
                                            //.Include(a => a.sector)
                                            .FirstOrDefaultAsync(a => a.idAsignacion == id);

                if (asignacion == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = asignacion;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //--------------- EndPoint que crea una nueva asignacion ------------
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> PostAsignacion(AsignacionDto asignacionDto)
        {
            try
            {
                var asignacion = new Asignacion
                {
                    idPersonal = asignacionDto.idPersonal,
                    idSector = asignacionDto.idSector,
                    diaSemana = asignacionDto.diaSemana,
                    turno = asignacionDto.turno,
                    horaInicio = asignacionDto.horaInicio,
                    horaFinalizacion = asignacionDto.horaFinalizacion
                };

                _applicationDbContext.Asignaciones.Add(asignacion);
                await _applicationDbContext.SaveChangesAsync();

                _response.Resultado = asignacion;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtAction("GetAsignacion", new { id = asignacion.idAsignacion }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //--------------- EndPoint que actualiza una asignacion existente ------------
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutAsignacion(int id, AsignacionDto asignacionDto)
        {
            try
            {
                if (id != asignacionDto.idAsignacion)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var asignacion = await _applicationDbContext.Asignaciones.FindAsync(id);
                if (asignacion == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                asignacion.idPersonal = asignacionDto.idPersonal;
                asignacion.idSector = asignacionDto.idSector;
                asignacion.diaSemana = asignacionDto.diaSemana;
                asignacion.turno = asignacionDto.turno;
                asignacion.horaInicio = asignacionDto.horaInicio;
                asignacion.horaFinalizacion = asignacionDto.horaFinalizacion;

                _applicationDbContext.Entry(asignacion).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //--------------- EndPoint que elimina una asignacion ------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAsignacion(int id)
        {
            try
            {
                var asignacion = await _applicationDbContext.Asignaciones.FindAsync(id);
                if (asignacion == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _applicationDbContext.Asignaciones.Remove(asignacion);
                await _applicationDbContext.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
        }

        private bool AsignacionExists(int id)
        {
            return _applicationDbContext.Asignaciones.Any(e => e.idAsignacion == id);
        }
    }
}
