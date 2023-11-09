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
    public class DetalleController : ApiController
    {
        private readonly IDetalleService _DetalleService;

        public DetalleController(IDetalleService detalleService)
        {
            _DetalleService = detalleService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDetalle()
        {
            List<DetalleProducto> detalle =await _DetalleService.GetAll();
            return Ok(detalle);
        }

        [HttpGet]
        public async Task<IHttpActionResult>  GetDetalleById(Guid Id)
        {
            DetalleProducto detalle = await _DetalleService.GetById(Id);
            return Ok(detalle);
        }

        [HttpPost]
        public IHttpActionResult Create(DetalleProductoDTO nuevoDetalle)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(string.Join(" ", errors));
            }

            DetalleProducto newDetalle = _DetalleService.CrearDetalle(nuevoDetalle);
            return Ok(newDetalle);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Eliminar(Guid Id)
        {
            await _DetalleService.EliminarDetalle(Id);
            return Ok("El detalle de producto fue eliminado");
        }

        [HttpPut]
        public async Task<IHttpActionResult> modificarDetalle(Guid Id, DetalleProductoDTO nuevosCampos)
        {
            await _DetalleService.ModificarDetalle(Id, nuevosCampos);
            return Ok("El detalle de producto ha sido modificado");
        }


    }
}
