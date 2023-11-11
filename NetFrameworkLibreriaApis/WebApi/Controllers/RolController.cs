using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class RolController : ApiController
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRol()
        {
            List<Rol> material = await _rolService.GetAll();
            return Ok(material);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRolById(Guid Id)
        {
            Rol rol = await _rolService.GetById(Id);
            return Ok(rol);
        }

        [HttpPost]
        public IHttpActionResult crearRol(RolDTO nuevoRol)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Rol newRol = _rolService.CrearRol(nuevoRol);
            return Ok(newRol);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _rolService.EliminarRol(Id);
            return Ok();
        }
    }
}
