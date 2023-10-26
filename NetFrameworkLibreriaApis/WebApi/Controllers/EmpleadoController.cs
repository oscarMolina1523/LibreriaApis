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
    public class EmpleadoController : ApiController
    {
        private readonly IEmpleadoService _EmpleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _EmpleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEmpleado()
        {
            List<Empleado> empleado = await _EmpleadoService.GetAll();

            return Ok(empleado);
        }

        [HttpGet]
        public IHttpActionResult GetEmpleadoById(Guid Id)
        {
            Empleado empleado = _EmpleadoService.GetById(Id);

            return Ok(empleado);
        }

        [HttpPost]
        public IHttpActionResult Create(EmpleadoDTO nuevoEmpleado)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            Empleado newEmpleado = _EmpleadoService.CrearEmpleado(nuevoEmpleado);

            return Ok(newEmpleado);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _EmpleadoService.EliminarEmpleado(Id);

            return Ok("el empleado ha sido eliminado");
        }

        [HttpPut]
        public IHttpActionResult modificarEmpleado(Guid Id, EmpleadoDTO nuevosCampos)
        {
            _EmpleadoService.ModificarEmpleado(Id, nuevosCampos);

            return Ok("El empleado ha sido modificado");
        }

    }
}
