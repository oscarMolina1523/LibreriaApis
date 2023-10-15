using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetProveedor()
        {
            List<Proveedor> proveedor = await _ProveedorService.GetAll();
            return Ok(proveedor);
        }

        [HttpGet]
        public IHttpActionResult GetProveedorById(Guid Id)
        {
            Proveedor proveedor = _ProveedorService.GetById(Id);
            return Ok(proveedor);
        }

        [HttpPost]
        public IHttpActionResult Create(ProveedorDTO nuevoProveedor)
        {
            Proveedor newProveedor = _ProveedorService.CrearProveedor(nuevoProveedor);
            return Ok(newProveedor);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(Guid Id)
        {
            _ProveedorService.EliminarProveedor(Id);
            return Ok("proveedor eliminado");
        }

        [HttpPut]
        public IHttpActionResult modificarProveedor(Guid Id, ProveedorDTO nuevosCampos)
        {
            _ProveedorService.ModificarProveedor(Id, nuevosCampos);
            return Ok("El proveedor ha sido modificado");
        }

    }
}
