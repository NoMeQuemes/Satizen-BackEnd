using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Satizen_Api.Data;
using Satizen_Api.DTOs;
using Satizen_Api.Models.DispositivoLaboral;
using Satizen_Api.Models.Dto.DispositivoLaboral;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satizen_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispositivoLaboralController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DispositivoLaboralController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        [Route("DispositivoLaboral/Listar")]
        public async Task<ActionResult<IEnumerable<DispositivoLaboralDto>>> GetDispositivosLaborales()
        {
            var dispositivosLaborales = await _context.DispositivosLaborales
                .Include(d => d.Personals) // Incluimos la entidad relacionada Personal
                .ToListAsync();

            var dispositivosLaboralesDto = dispositivosLaborales.Select(d => new DispositivoLaboralDto
            {
                idTelefonoEmpresa = d.idTelefonoEmpresa,
                idPersonal = d.idPersonal ?? 0, // ?? 0 para manejar el valor nullable
                numeroEmpresa = d.numeroEmpresa,
                marca = d.marca,
                modelo = d.modelo,
                almacenamiento = d.almacenamiento,
                color = d.color
            }).ToList();

            return Ok(dispositivosLaboralesDto);
        }

        
        [HttpGet]
        [Route("DispositivoLaboral/Listar/{id}")]
        public async Task<ActionResult<DispositivoLaboralDto>> GetDispositivoLaboral(int id)
        {
            var dispositivoLaboral = await _context.DispositivosLaborales
                .Include(d => d.Personals) // Incluimos la entidad relacionada Personal
                .FirstOrDefaultAsync(d => d.idTelefonoEmpresa == id);

            if (dispositivoLaboral == null)
            {
                return NotFound();
            }

            var dispositivoLaboralDto = new DispositivoLaboralDto
            {
                idTelefonoEmpresa = dispositivoLaboral.idTelefonoEmpresa,
                idPersonal = dispositivoLaboral.idPersonal ?? 0, // ?? 0 para manejar el valor nullable
                numeroEmpresa = dispositivoLaboral.numeroEmpresa,
                marca = dispositivoLaboral.marca,
                modelo = dispositivoLaboral.modelo,
                almacenamiento = dispositivoLaboral.almacenamiento,
                color = dispositivoLaboral.color
            };

            return Ok(dispositivoLaboralDto);
        }

   
        [HttpPost]
        [Route("DispositivoLaboral/Crear")]
        public async Task<ActionResult<DispositivoLaboralDto>> PostDispositivoLaboral(CrearDispositivoLaboralDto crearDispositivoLaboralDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dispositivoLaboral = new DispositivoLaboral
            {
                idPersonal = crearDispositivoLaboralDto.idPersonal,
                numeroEmpresa = crearDispositivoLaboralDto.numeroEmpresa,
                marca = crearDispositivoLaboralDto.marca,
                modelo = crearDispositivoLaboralDto.modelo,
                almacenamiento = crearDispositivoLaboralDto.almacenamiento,
                color = crearDispositivoLaboralDto.color
            };

            _context.DispositivosLaborales.Add(dispositivoLaboral);
            await _context.SaveChangesAsync();

            dispositivoLaboral.idTelefonoEmpresa = dispositivoLaboral.idTelefonoEmpresa;

            return CreatedAtAction(nameof(GetDispositivoLaboral), new { id = dispositivoLaboral.idTelefonoEmpresa }, crearDispositivoLaboralDto);
        }

  
        [HttpPut]
        [Route("DispositivoLaboral/Actualizar/{id}")]
        public async Task<IActionResult> PutDispositivoLaboral(int id, ActualizarDispositivoLaboralDto actualizarDispositivoLaboralDto)
        {
            if (id != actualizarDispositivoLaboralDto.idTelefonoEmpresa)
            {
                return BadRequest();
            }

            var dispositivoLaboral = await _context.DispositivosLaborales.FindAsync(id);

            if (dispositivoLaboral == null)
            {
                return NotFound();
            }

            dispositivoLaboral.idPersonal = actualizarDispositivoLaboralDto.idPersonal;
            dispositivoLaboral.numeroEmpresa = actualizarDispositivoLaboralDto.numeroEmpresa;
            dispositivoLaboral.marca = actualizarDispositivoLaboralDto.marca;
            dispositivoLaboral.modelo = actualizarDispositivoLaboralDto.modelo;
            dispositivoLaboral.almacenamiento = actualizarDispositivoLaboralDto.almacenamiento;
            dispositivoLaboral.color = actualizarDispositivoLaboralDto.color;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoLaboralExists(id))
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

  
        [HttpDelete]
        [Route("DispositivoLaboral/Eliminar/{id}")]
        public async Task<IActionResult> DeleteDispositivoLaboral(int id)
        {
            var dispositivoLaboral = await _context.DispositivosLaborales.FindAsync(id);

            if (dispositivoLaboral == null)
            {
                return NotFound();
            }

            _context.DispositivosLaborales.Remove(dispositivoLaboral);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispositivoLaboralExists(int id)
        {
            return _context.DispositivosLaborales.Any(e => e.idTelefonoEmpresa == id);
        }
    }
}
