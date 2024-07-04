using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Data;
using Satizen_Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ApiResponse _response;

        public TurnoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Turno
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetTurnos()
        {
            try
            {

                _response.Resultado = await _context.Turnos
                                              .Where(u => u.estadoTurno == null)
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

        // GET: api/Turno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetTurno(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);

            if (turno == null)
            {
                return NotFound();
            }

            return turno;
        }

        // PUT: api/Turno/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurno(int id, Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return BadRequest();
            }

            _context.Entry(turno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(id))
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

        private bool TurnoExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Turno
        [HttpPost]
        public async Task<ActionResult<Turno>> PostTurno(Turno turno)
        {
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurno", new { id = turno.TurnoId }, turno);
        }

        // DELETE: api/Turno/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> DesactivarPaciente(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var Turno = await _context.Turnos.FirstOrDefaultAsync(p => p.TurnoId == id);

            if (Turno == null)
            {
                return NotFound();
            }

            // Desactivar el turno estableciendo la fecha actual en estadoTurno
            Turno.estadoTurno = DateTime.Now;

            _context.Turnos.Update(Turno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
