using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Data;
using Satizen_Api.Models;


namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Esta sentencia determina que a esta api solo pueden entrar usuarios autorizados
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignaciones()
        {
            return await _applicationDbContext.Asignaciones
                                 .Include(a => a.personal)
                                 .Include(a => a.sector)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        {
            var asignacion = await _applicationDbContext.Asignaciones
                                          .Include(a => a.personal)
                                          .Include(a => a.sector)
                                          .FirstOrDefaultAsync(a => a.idAsignacion == id);

            if (asignacion == null)
            {
                return NotFound();
            }

            return asignacion;
        }

        [HttpPost]
        public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
        {
            _applicationDbContext.Asignaciones.Add(asignacion);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion", new { id = asignacion.idAsignacion }, asignacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
        {
            if (id != asignacion.idAsignacion)
            {
                return BadRequest();
            }

            _applicationDbContext.Entry(asignacion).State = EntityState.Modified;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
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
        public async Task<IActionResult> DeleteAsignacion(int id)
        {
            var asignacion = await _applicationDbContext.Asignaciones.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }

            _applicationDbContext.Asignaciones.Remove(asignacion);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionExists(int id)
        {
            return _applicationDbContext.Asignaciones.Any(e => e.idAsignacion == id);
        }
    }
}
