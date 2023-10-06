using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class EmpleadoController : ApiController
    {
        private readonly IEmpleadoService _EmpleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _EmpleadoService = empleadoService;
        }

        [HttpGet]
        public IHttpActionResult GetEmpleado()
        {
            List<EmpleadoDTO> empleado = _EmpleadoService.GetAll();
            return Ok(empleado);
        }

        [HttpGet]
        public IHttpActionResult GetEmpleadoById(Guid Id)
        {
            EmpleadoDTO empleado = _EmpleadoService.GetById(Id);
            return Ok(empleado);
        }

        [HttpPost]
        public IHttpActionResult Create(EmpleadoDTO nuevoEmpleado)
        {
            EmpleadoDTO newEmpleado = _EmpleadoService.CrearEmpleado(nuevoEmpleado);
            return Ok(newEmpleado);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _EmpleadoService.EliminarEmpleado(Id);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult modificarEmpleado(Guid Id, EmpleadoDTO nuevosCampos)
        {
            _EmpleadoService.ModificarEmpleado(Id, nuevosCampos);
            return Ok("El empleado ha sido modificado");
        }

    }
}
