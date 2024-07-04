using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Satizen_Api.Data;
using Satizen_Api.Models;
using Satizen_Api.Models.Dto.Personal;

using System.Net;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected ApiResponse _response;

        public PersonalController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _response = new();
        }

        [HttpGet]
        [Route("ListarPersonal")]
        //[Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Personal>))]
        public async Task<ActionResult<IEnumerable<Personal>>> GetPersonals()
        {
            return await _applicationDbContext.Personals.ToListAsync();
        }

        [HttpGet]
        [Route("ListarPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Personal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Personal>> GetPersonals(int id)
        {
            var personal = await _applicationDbContext.Personals.FindAsync(id);

            if (personal == null)
            {
                return NotFound();
            }

            return personal;
        }

        [HttpPost]
        [Route("CrearPersonal")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Personal))]
        public async Task<ActionResult<ApiResponse>> PostPersonals(AddPersonalDto Personal)
        {
            try
            {
                if (Personal == null)
                {
                    return BadRequest(Personal);
                }


                Personal modelo = new()
                {
                    idInstitucion = Personal.idInstitucion,
                    idUsuario = Personal.idUsuario,
                    nombrePersonal = Personal.nombrePersonal,
                    rolPersonal = Personal.rolPersonal,
                    celularPersonal = Personal.celularPersonal,
                    telefonoPersonal = Personal.telefonoPersonal,
                    correoPersonal = Personal.correoPersonal

                };

                await _applicationDbContext.Personals.AddAsync(modelo);
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

        [HttpPut]
        [Route("ActualizarPersonal/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Personal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPersonals(int id, Personal personal)
        {
            if (id != personal.idPersonal)
            {
                return BadRequest();
            }

            _applicationDbContext.Entry(personal).State = EntityState.Modified;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalExists(id))
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

        /*private bool PersonalExists(int id)
        {
            throw new NotImplementedException();
        }*/

        [HttpDelete]
        [Route("EliminarPersonal/{id:int}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal = await _applicationDbContext.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }

            _applicationDbContext.Personals.Remove(personal);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalExists(int id)
        {
            return _applicationDbContext.Personals.Any(e => e.idPersonal == id);
        }
    }
}
