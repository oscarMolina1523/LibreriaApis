using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetRol()
        {
            List<RolDTO> material = _rolService.GetAll();
            return Ok(material);
        }

        [HttpGet]
        public IHttpActionResult GetRolById(Guid Id)
        {
            RolDTO rol = _rolService.GetById(Id);
            return Ok(rol);
        }

        [HttpPost]
        public IHttpActionResult crearRol(RolDTO nuevoRol)
        {
            RolDTO newRol = _rolService.CrearRol(nuevoRol);
            return Ok(newRol);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _rolService.EliminarRol(Id);
            return Ok();
        }
    }
}
