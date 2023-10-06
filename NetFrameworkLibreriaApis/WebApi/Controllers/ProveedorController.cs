using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProveedorController : ApiController
    {
        private readonly IProveedorService _ProveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _ProveedorService = proveedorService;
        }

        [HttpGet]
        public IHttpActionResult GetProveedor()
        {
            List<ProveedorDTO> proveedor = _ProveedorService.GetAll();
            return Ok(proveedor);
        }

        [HttpGet]
        public IHttpActionResult GetProveedorById(Guid Id)
        {
            ProveedorDTO proveedor = _ProveedorService.GetById(Id);
            return Ok(proveedor);
        }

        [HttpPost]
        public IHttpActionResult Create(ProveedorDTO nuevoProveedor)
        {
            ProveedorDTO newProveedor = _ProveedorService.CrearProveedor(nuevoProveedor);
            return Ok(newProveedor);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _ProveedorService.EliminarProveedor(Id);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult modificarProveedor(Guid Id, ProveedorDTO nuevosCampos)
        {
            _ProveedorService.ModificarProveedor(Id, nuevosCampos);
            return Ok("El proveedor ha sido modificado");
        }

    }
}
